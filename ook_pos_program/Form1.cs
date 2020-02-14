using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.Common;
using MySql.Data.MySqlClient;
using MetroFramework.Forms;
using System.Media;
using System.IO;


namespace ook_pos_program
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        SoundPlayer sp = new SoundPlayer();
        string file = (string)@"C:\효과음5.wav";
        public delegate int delMessage(object sender, string strResult);
        public event delMessage eventdelMessage;
        string strconn = string.Format(@"server=localhost;database=kiosk;user=root;password=1234");
        //string strconn = string.Format(@"server=mydb.cmkvwrcpwpyc.ap-northeast-2.rds.amazonaws.com;database=ook;user=admin;password=young1700!");
        order order = new order();
        adapter ad = new adapter();
        order_small small = new order_small();
        order_medium medium = new order_medium();
        order_large large = new order_large();
        DataTable dt = new DataTable();
        Thread th=null;
        int new_row = 0;

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
            timer1.Interval = 1000;
            //small.eventClick += Order_small_eventClick;
            //panel8.Controls.Add(small);

            //시계
            //timer1.Tick += Timer1_Tick;
            //label24.Text = DateTime.Now.ToString();
            //label24.TextAlign = ContentAlignment.MiddleCenter;

            
            dt.Columns.Add("이름", typeof(string));
            dt.Columns.Add("개수", typeof(string));
            dt.Columns.Add("당도", typeof(string));
            dt.Columns.Add("휘핑크림", typeof(string));
            dt.Columns.Add("주문시간", typeof(string));
            dataGridView1.DataSource = dt;

            MySqlConnection conn = new MySqlConnection(strconn);
            try
            {
                conn.Open();
                //MetroFramework.MetroMessageBox.Show(this, "주문이 들어왔습니다", "주문정보", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn.Close();
            }
            catch
            {
                conn.Close();
                MessageBox.Show("MySql 연결 실패");
            }
            th = new Thread(new ThreadStart(watch));
            th.Start();
        }

        private void watch()
        {
            int i = 0, j = 0;
            int count1 = 0;
            int count2 = 0;
            string app_or_pos = string.Empty;
            MySqlConnection conn = new MySqlConnection(strconn);
            string watch = "select max(idorderMenu) from orderMenu";
            string app_pos = "select confirmed from orderMenu where idorderMenu='#idorderMenu'"; //주문이 완료 되었는지 여부를 알려주는 데이터 뽑기
            string rowNum = "select idorderMenu from orderMenu where idorderMenu='#idorderMenu'"; //현재 행을 알아내기 위한 쿼리문
            //string infoQuery = "select * from oneOrder where idorderMenu=#idorderMenu";
            MySqlCommand cmd = new MySqlCommand(watch, conn);
            MySqlCommand cmd_app_pos;
            MySqlCommand cmd_newRow;
            int row_number = 0;


            while (true)
            {
                app_pos = "select confirmed from orderMenu where idorderMenu='#idorderMenu'";
                conn.Open();
                count1 = Convert.ToInt32(cmd.ExecuteScalar());
                //MessageBox.Show(count1.ToString()); //디버깅용
                app_pos = app_pos.Replace("#idorderMenu", count1.ToString());
                cmd_app_pos = new MySqlCommand(app_pos, conn);
                app_or_pos =(string)cmd_app_pos.ExecuteScalar();
                //infoQuery = infoQuery.Replace("#idorderMenu", count1.ToString());

                if (i == 0)
                {
                    count2 = count1;
                }
                if (count1 > count2)
                {
                    if (app_or_pos == "F")
                    {
                        if (this.InvokeRequired)
                        {
                            rowNum = rowNum.Replace("#idorderMenu", count1.ToString()); //ios에서 추가한 주문의 행번호
                            cmd_newRow = new MySqlCommand(rowNum, conn);
                            row_number = Convert.ToInt32(cmd_newRow.ExecuteScalar());
                            this.Invoke(new Action(delegate ()
                            {
                                if (order.button_click == 0) //3개중 맨 아래의 팝업창
                                {
                                    order.button_click++;
                                    new_row = row_number; //전역변수 new_row에 쿼리문을 통해 얻은 변수값을 대입
                                    sp.SoundLocation = file; 
                                    sp.Play();
                                    button8.Visible = true;
                                }
                                else if(order.button_click == 1) //두번째 팝업창
                                {
                                    order.button_click++;
                                    new_row = row_number;
                                    sp.SoundLocation = file;
                                    sp.Play();
                                    button18.Visible = true;
                                }
                                else if(order.button_click == 2) //세번째 팝업창
                                {
                                    sp.SoundLocation = file;
                                    sp.Play();
                                    new_row = row_number;
                                    button23.Visible = true;
                                }
                                this.Refresh();
                            }));
                        }
                    }
                }
                i++;
                count2 = count1;
                conn.Close();

                
                Thread.Sleep(1000);
                
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label24.Text = DateTime.Now.ToString();
        }

        private void button40_Click(object sender, EventArgs e)
        {
            string[,] strData = new string[10, 5];
            int i = 0, j = 0;

            for (i = 0; i < order.HowManyInsert; i++)
            {
                for (j = 0; j < 5; j++)
                {
                    strData[i, j] = dataGridView2.Rows[i].Cells[j].Value.ToString();
                }
            }

            Form_PayCash cashForm = new Form_PayCash();

            cashForm.getArray(strData,order.HowManyInsert,order.price);
            cashForm.ShowDialog();
            
        } //현금결제 버튼

        private int Cash_eventcash()
        {
            throw new NotImplementedException();
        }

        #region event

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            string size = "";
            if (cb.SelectedIndex > -1)
            {
                size = cb.SelectedItem.ToString();
            }
            if (size.Equals("Small"))
            {
               // MetroFramework.MetroMessageBox.Show(this, "주문이 들어왔습니다", "주문정보", MessageBoxButtons.OK, MessageBoxIcon.Information);

                small.eventClick += Order_small_eventClick;
                
                panel8.Controls.Clear();
                panel8.Controls.Add(small);
                small.dt_exchange(dt); //small에 선언한 함수에 데이터 전달
            }
            else if (size.Equals("Medium"))
            {
                order_medium medium = new order_medium();
                medium.eventClick_m += Medium_eventClick_m;
                panel8.Controls.Clear();
                panel8.Controls.Add(medium);
            }
            else if (size.Equals("Large"))
            {
                order_large large = new order_large();
                large.eventClick_l += Large_eventClick_l;
                panel8.Controls.Clear();
                panel8.Controls.Add(large);
            }
        }

        #region evnetClick
        private int Order_small_eventClick(int price, DataTable dt, int HowManyka)
        {
            order.price += price;
            textBoxget.Text = order.price.ToString();  //내야할 비용 int로 받아서 string으로 출력
            textBoxsum.Text = order.price.ToString();
            dataGridView1.DataSource = dt;
            dataGridView2.DataSource = dt;
            order.HowManyInsert += HowManyka;
            return 0;
        }

        private int Large_eventClick_l(int price, DataTable dt, int HowManyka)
        {
            order.price += price;
            textBoxget.Text = order.price.ToString();  //내야할 비용 int로 받아서 string으로 출력
            textBoxsum.Text = order.price.ToString();
            dataGridView1.DataSource = dt;
            dataGridView2.DataSource = dt;
            order.HowManyInsert += HowManyka;
            return 0;
        }

        private int Medium_eventClick_m(int price, DataTable dt, int HowManyka)
        {
            order.price += price;
            textBoxget.Text = order.price.ToString();  //내야할 비용 int로 받아서 string으로 출력
            textBoxsum.Text = order.price.ToString();
            dataGridView1.DataSource = dt;
            dataGridView2.DataSource = dt;
            order.HowManyInsert += HowManyka;
            return 0;
        }

        #endregion

        #endregion

        private void button5_Click(object sender, EventArgs e)
        {
            //MySqlConnection conn = new MySqlConnection(strconn);
            //conn.Open();
            //string delete_query = "truncate orderMenu";
            //MySqlCommand cmd = new MySqlCommand(delete_query, conn);
            //cmd.ExecuteNonQuery();
            //conn.Close();
            int i = 0;

            //order 클래스에 개수 초기화
            for (i = 0; i < 3; i++)
            {
                //콜드브루
                order.coldbrew_1[i] = 0;
                order.coldbrew_2[i] = 0;
                order.coldbrew_3[i] = 0;

                //커피&에스프레소
                order.coffee_capu[i] = 0;
                order.coffee_dolche[i] = 0;
                order.coffee_maggi[i] = 0;
                order.coffee_moka[i] = 0;
                order.coffee_rate[i] = 0;
                order.coffee_americano[i] = 0;
                order.coffee_icecoffee[i] = 0;

                //블렌디드
                order.blendid_green[i] = 0;
                order.blendid_mango[i] = 0;
                order.blendid_choba[i] = 0;
                order.blendid_yo[i] = 0;

                //스타벅스 피지오
                order.pgio_pink[i] = 0;
                order.pgio_raim[i] = 0;
                order.pgio_blacktea[i] = 0;
                order.pgio_greentea[i] = 0;
                order.pgio_passion[i] = 0;

                //프라푸치노
                order.pra_java[i] = 0;
                order.pra_white[i] = 0;
                order.pra_moka[i] = 0;
                order.pra_esp[i] = 0;
                order.pra_greentea[i] = 0;
                order.pra_choco[i] = 0;
                order.pra_brerry[i] = 0;
                order.pra_chococream[i] = 0;

                //초콜릿
                order.choco_sig[i] = 0;
                order.choco_banila[i] = 0;
                order.choco_apo[i] = 0;

                //스무디
                order.smudi_taro[i] = 0;
                order.smudi_mango[i] = 0;
                order.smudi_choco[i] = 0;
                order.smudi_moca[i] = 0;
            }

            //DataTable dt = new DataTable();
            //dt = ad.ConnectDB();
            //dataGridView1.DataSource = dt;

            ((DataTable)dataGridView2.DataSource).Rows.Clear();
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            int temp = 0;
            string countVal = string.Empty;

            foreach (DataGridViewRow dgr in dataGridView2.SelectedRows)
            {
                countVal=dgr.Cells[1].Value.ToString();
                temp = int.Parse(countVal);
                temp=temp-1;

                if (temp <= 0)
                {
                    dataGridView2.Rows.Remove(dgr);
                }

                dgr.Cells[1].Value = temp.ToString();
            }
        }

       

        private void button14_Click(object sender, EventArgs e)
        {
            string noLogin = "noLogin";
            this.Visible = false; //현재 폼 안보이게 하기
            Form2 frm = new Form2(noLogin);
            frm.Owner = this; //form1의 오너를 현재 폼으로
            frm.Show(); //form1 보여주기
        } //뒤로가기

        private void button6_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgr in dataGridView2.SelectedRows)
            {
                dataGridView2.Rows.Remove(dgr);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int temp = 0;
            string countVal = string.Empty;

            foreach (DataGridViewRow dgr in dataGridView2.SelectedRows)
            {
                countVal = dgr.Cells[1].Value.ToString();
                temp = int.Parse(countVal);
                temp++;
                dgr.Cells[1].Value = temp.ToString();
            }
        }


        private void button8_Click(object sender, EventArgs e)
        {
            comed_order();

        } //주문이 들어왔습니다.

        private void button18_Click(object sender, EventArgs e)
        {
            comed_order();


        } //주문이 들어왔습니다.

        private void button23_Click(object sender, EventArgs e)
        {
            comed_order();

        } //주문이 들어왔습니다.

        public void comed_order()
        {
            order.button_click = 0;
            button8.Visible = false; //주문이 들어왔습니다 버튼 안보이게 하기
            button18.Visible = false; //주문이 들어왔습니다 버튼 안보이게 하기
            button23.Visible = false; //주문이 들어왔습니다 버튼 안보이게 하기

            string[,] strData = new string[10, 5];

            for (int i = 0; i < order.HowManyInsert; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    strData[i, j] = dataGridView2.Rows[i].Cells[j].Value.ToString();
                }
            }

            confirm_order co = new confirm_order(strData, order.HowManyInsert, order.price);
            co.Show();
            MessageBox.Show(new_row.ToString());

        }

        private void button24_Click(object sender, EventArgs e)
        {
            th.Abort();
            timer1.Stop();
            Application.Exit();
        }

        private void button25_MouseMove(object sender, MouseEventArgs e)
        {
            //button25.BackColor = Color.FromArgb(128, 128, 255);
        }

        private void button26_MouseMove(object sender, MouseEventArgs e)
        {
           // button26.BackColor = Color.FromArgb(128, 128, 255);
        }

        private void button27_MouseMove(object sender, MouseEventArgs e)
        {
            //button27.BackColor = Color.FromArgb(128, 128, 255);
        }

        private void button25_MouseLeave(object sender, EventArgs e)
        {
            //button25.BackColor = Color.FromArgb(0, 0, 64);
        }

        private void button26_MouseLeave(object sender, EventArgs e)
        {
            //button26.BackColor = Color.FromArgb(0, 0, 64);
        }

        private void button27_MouseLeave(object sender, EventArgs e)
        {
            //button27.BackColor = Color.FromArgb(0, 0, 64);
        }


        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            small.eventClick += Order_small_eventClick;

            panel8.Controls.Clear();
            panel8.Controls.Add(small);
            small.dt_exchange(dt); //small에 선언한 함수에 데이터 전달
        } //스몰

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            medium.eventClick_m += Medium_eventClick_m;
            panel8.Controls.Clear();
            panel8.Controls.Add(medium);
            medium.dt_exchange(dt); //medium에 선언한 함수에 데이터 전달
        } //미디움

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            large.eventClick_l += Large_eventClick_l;
            panel8.Controls.Clear();
            panel8.Controls.Add(large);
            large.dt_exchange(dt); //large에 선언한 함수에 데이터 전달

        } //라지

        private void button25_Click(object sender, EventArgs e)
        {
            small.eventClick += Order_small_eventClick;

            panel8.Controls.Clear();
            panel8.Controls.Add(small);
            small.dt_exchange(dt); //small에 선언한 함수에 데이터 전달
        } //스몰버튼

        private void button26_Click(object sender, EventArgs e)
        {
            medium.eventClick_m += Medium_eventClick_m;
            panel8.Controls.Clear();
            panel8.Controls.Add(medium);
            medium.dt_exchange(dt); //medium에 선언한 함수에 데이터 전달
        } //미디움버튼

        private void button27_Click(object sender, EventArgs e)
        {
            large.eventClick_l += Large_eventClick_l;
            panel8.Controls.Clear();
            panel8.Controls.Add(large);
            large.dt_exchange(dt); //large에 선언한 함수에 데이터 전달
        } //라지버튼

        public void IOToPrint()
        {
            string fst_line = string.Empty;
            string snd_line = string.Empty;
            string thrd_line = string.Empty;
            string four_line = string.Empty;
            string five_line = string.Empty;
            string[] lines = { fst_line, snd_line, thrd_line,four_line,five_line};



            using (StreamWriter outputFile = new StreamWriter(@"C:\Users\eoaud\source\repos\FileIO_test\FileIO_test\test.txt"))
            {
                foreach (string line in lines)
                {
                    outputFile.WriteLine(line);
                }
            }
        }
    }
}
