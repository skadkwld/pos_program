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

namespace ook_pos_program
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public delegate int delMessage(object sender, string strResult);
        public event delMessage eventdelMessage;
        string strconn = string.Format(@"server=localhost;database=kiosk;user=root;password=1234");
        //string strconn = string.Format(@"server=mydb.cmkvwrcpwpyc.ap-northeast-2.rds.amazonaws.com;database=ook;user=admin;password=young1700!");
        order order = new order();
        adapter ad = new adapter();
        order_small small = new order_small();
        DataTable dt = new DataTable();
        Thread th=null;

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
            timer1.Interval = 1000;
            small.eventClick += Order_small_eventClick;
            panel8.Controls.Add(small);
            timer1.Tick += Timer1_Tick;
            label24.Text = DateTime.Now.ToString();
            label24.TextAlign = ContentAlignment.MiddleCenter;

            
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
            string watch = "select count(*) from orderMenu";
            string app_pos = "select confirmed from orderMenu";
            //string infoQuery = "select * from oneOrder where idorderMenu=#idorderMenu";
            MySqlCommand cmd = new MySqlCommand(watch, conn);
            MySqlCommand cmd_app_pos = new MySqlCommand(app_pos, conn);


            while (true)
            {
                conn.Open();
                count1 = Convert.ToInt32(cmd.ExecuteScalar());
                app_pos = app_pos.Replace("#idorderMenu", count1.ToString());
                app_or_pos = Convert.ToString(cmd_app_pos.ExecuteScalar());
                //infoQuery = infoQuery.Replace("#idorderMenu", count1.ToString());
                IOS_Order goPanel = new IOS_Order();
                App_order go_panel = new App_order();

                if (i == 0)
                {
                    count2 = count1;
                }
                if (count1 != count2)
                {
                    if (app_or_pos == "F")
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke(new Action(delegate ()
                            {
                                if (j > 0)
                                {
                                    //panel3.BringToFront();
                                    //panel3.Visible = true;
                                    //panel3.Controls.Add(goPanel);
                                    //go_panel.Location = new Point(836, 645);
                                    go_panel.Show();
                                }
                                //MessageBox.Show(string.Format("앱에서 주문이 들어왔습니다."));
                                else 
                                {
                                    //go_panel.Location = new Point(1502, 869);
                                    go_panel.Show();
                                    //panel1.Visible = true;
                                    //panel1.Controls.Clear();
                                    //panel1.Controls.Add(goPanel);
                                    //j++;
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

            cashForm.getArray(strData,order.HowManyInsert);
            cashForm.ShowDialog();
            
        }

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

        private int Large_eventClick_l(int price, DataTable dt)
        {
            textBoxget.Text = price.ToString();  //내야할 비용 int로 받아서 string으로 출력
            textBoxsum.Text = price.ToString();
            dataGridView1.DataSource = dt;
            return 0;
        }

        private int Medium_eventClick_m(int price, DataTable dt)
        {
            textBoxget.Text = price.ToString();  //내야할 비용 int로 받아서 string으로 출력
            textBoxsum.Text = price.ToString();
            dataGridView1.DataSource = dt;
            return 0;
        }

        private int Order_small_eventClick(int price, DataTable dt,int HowManyka)
        {
            textBoxget.Text = price.ToString();  //내야할 비용 int로 받아서 string으로 출력
            textBoxsum.Text = price.ToString();
            dataGridView1.DataSource = dt;
            dataGridView2.DataSource = dt;
            order.HowManyInsert = HowManyka;
            return 0;
        }

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

        private void timer1_Tick_1(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            th.Abort();
            timer1.Stop();
            Application.Exit();
        } //종료

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
    }
}
