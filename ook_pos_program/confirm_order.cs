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
using System.Net.Http;

namespace ook_pos_program
{
    public partial class confirm_order : Form
    {
        SoundPlayer sp = new SoundPlayer();
        string file = (string)@"C:\주문이 접수되었습니다.wav";
        string strconn = string.Format(@"server=localhost;database=kiosk;user=root;password=1234");
        string ing = "ING..";
        string t = "T";
        string f = "F";
        int rowNum = 0;
       // string strconn = string.Format(@"server=mydb.cmkvwrcpwpyc.ap-northeast-2.rds.amazonaws.com;database=ook;user=admin;password=young1700!");
        order order = new order();
        adapter ad = new adapter();
        DataTable dt=null;
        MySqlConnection conn;
        string[,] arr_for_insert = new string[10, 5];
        int rowCount = 0;
        string docName = "test.txt";
        string docPath = @"C:\Users\eoaud\source\repos\ook_pos_program\ook_pos_program\";
        string stringToPrint;
        string documentContents;


        public confirm_order()
        {
            InitializeComponent();
            dataGridView_binding();
            fillPanel();
        }

        public confirm_order(string[,] strData, int HowManyInsert,int price)
        {
            InitializeComponent();
            dataGridView_binding();
            getArray(strData,HowManyInsert,price);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            sp.SoundLocation = file;
            sp.Play();
            MySqlConnection conn = new MySqlConnection(strconn);
            int id = 0;
            string f_to_ing = string.Empty;

           

            dataGridView_binding(); //업데이트 된 DB를 새로고침 하는 기능
        } //주문 접수 버튼

        public void dataGridView_binding()
        {
            int panelCount = 0;
            MySqlConnection conn = new MySqlConnection(strconn);

            conn.Open();

            string binding = "select o.idorderMenu as '주문번호',o.currentDate as '주문날짜',o.name as '메뉴',m.price as '가격',o.count as '개수' from orderMenu o,menu m where o.name=m.name and o.confirmed='#confirmed' order by o.currentDate desc,o.idorderMenu desc";


            //데이터그리드뷰 코드-----------------------------------

            binding = binding.Replace("#confirmed", f.ToString());
            MySqlDataAdapter adapter = new MySqlDataAdapter(binding, conn);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;

            conn.Close();

            button16.BackColor = Color.White;
            button16.ForeColor = Color.Black;
        }

        public void dataGridView_binding_ING()
        {
            int panelCount = 0;
            MySqlConnection conn = new MySqlConnection(strconn);

            conn.Open();

            string binding = "select o.idorderMenu as '주문번호',o.currentDate as '주문날짜',o.name as '메뉴',m.price as '가격',o.count as '개수' from orderMenu o,menu m where o.name=m.name and o.confirmed='#confirmed' order by o.currentDate desc,o.idorderMenu desc";


            //데이터그리드뷰 코드-----------------------------------

            binding = binding.Replace("#confirmed", "ING");
            MySqlDataAdapter adapter = new MySqlDataAdapter(binding, conn);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;

            conn.Close();
        }



        private void button6_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(strconn);
            int id = 0;
            string f_to_ing = string.Empty;

            string name = string.Empty;
            string count = string.Empty;
            string price = string.Empty;


            //영주증 부분 코드
            WriteFile(name,count,price);
            ReadFile();
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();

        } //주문표 인쇄

 
        public void getArray(string[,] DBarr, int count, int price)
        {
            //DBarr = new string[10, 5];
            arr_for_insert = DBarr;
            rowCount = count;

        }

        public void WriteFile(string name, string count,string price)
        {
            int j = 0;
            //int price = 0;
            int sumprice = 0;
            int length = 22;
            //string name = string.Empty;

            using (StreamWriter outputFile = new StreamWriter(@"C:\Users\eoaud\source\repos\ook_pos_program\ook_pos_program\test.txt"))
            {
                outputFile.WriteLine("-----------------------------------------------------------");
                outputFile.WriteLine("메뉴                                      수량          금액");
                outputFile.WriteLine("-----------------------------------------------------------");


                outputFile.WriteLine(string.Format("{0}                    {1}        {2}", name,count,price));

                sumprice = Convert.ToInt32(price);
                outputFile.WriteLine("-----------------------------------------------------------");
                outputFile.WriteLine(string.Format("총 결제금액 : {0}", sumprice));
                //outputFile.Close();
            }
        }

        public void ReadFile()
        {
            printDocument1.DocumentName = docName;
            using (FileStream stream = new FileStream(docPath + docName, FileMode.Open))
            using (StreamReader reader = new StreamReader(stream))
            {
                documentContents = reader.ReadToEnd();
            }
            stringToPrint = documentContents;
            MessageBox.Show(stringToPrint);
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int charactersOnPage = 0;
            int linesPerPage = 0;

            // Sets the value of charactersOnPage to the number of characters 
            // of stringToPrint that will fit within the bounds of the page.
            e.Graphics.MeasureString(stringToPrint, this.Font,
                e.MarginBounds.Size, StringFormat.GenericTypographic,
                out charactersOnPage, out linesPerPage);

            // Draws the string within the bounds of the page.
            e.Graphics.DrawString(stringToPrint, this.Font, Brushes.Black,
            e.MarginBounds, StringFormat.GenericTypographic);

            // Remove the portion of the string that has been printed.
            stringToPrint = stringToPrint.Substring(charactersOnPage);

            // Check to see if more pages are to be printed.
            e.HasMorePages = (stringToPrint.Length > 0);

            // If there are no more pages, reset the string to be printed.
            if (!e.HasMorePages)
                stringToPrint = documentContents;
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_MouseMove(object sender, MouseEventArgs e)
        {
           // button7.BackColor = Color.White;
        }

        private void button8_MouseMove(object sender, MouseEventArgs e)
        {
            //button8.BackColor = Color.White;
        }

        private void button6_MouseMove(object sender, MouseEventArgs e)
        {
            //button6.BackColor = Color.White;
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            //button6.BackColor = Color.LightGray;
        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {
            //button7.BackColor = Color.LightGray;
        }

        private void button8_MouseLeave(object sender, EventArgs e)
        {
            //button8.BackColor = Color.LightGray;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            orderPanel op = new orderPanel();
            
            Panel panel = new Panel();
            Panel panel2 = new Panel();
            panel.Size= new System.Drawing.Size(717, 100);
            panel2.Size = new System.Drawing.Size(717, 100);
            
            panel.Controls.Add(op);
            panel2.Controls.Add(op);
        }


        public void fillPanel()
        {
            int panelNum = 0;
            int exit = 0;

            while (true)
            {

                switch (panelNum)
                {
                    case 0:
                        panel4.Visible = true;
                        getDatagridview(0);
                        break;
                    case 1:
                        panel5_1.Visible = true;
                        getDatagridview(1);
                        //exit = 1;
                        break;
                    case 2:
                        panel12.Visible = true;
                        getDatagridview(2);
                        //exit = 1;
                        break;
                    case 3:
                        panel11.Visible = true;
                        getDatagridview(3);
                        //exit = 1;
                        break;
                    case 4:
                        panel10.Visible = true;
                        getDatagridview(4);
                        break;
                    case 5:
                        panel9.Visible = true;
                        getDatagridview(5);
                        break;
                    case 6:
                        panel8.Visible = true;
                        getDatagridview(6);
                        break;
                    case 7:
                        panel6.Visible = true;
                        getDatagridview(7);
                        break;
                    case 8:
                        panel7.Visible = true;
                        getDatagridview(8);
                        break;

                }

                if (dataGridView1.Rows.Count == 1) break;
                panelNum++;
            }
        }

        public void getDatagridview(int flag)
        {
            int i = 0, j = 0;

            string[] orderNum = new string[10];
            string[] orderDate = new string[10];
            string[] menuName = new string[10];
            string[] price = new string[10];
            int[] num = new int[10];

            int totalPrice = 0;
            int totalNum = 0;

            totalPrice = num[3];

            while (true)
            {

                //데이터그리드뷰에 이 행까지만 존재하면
                if (dataGridView1.Rows[i + 1].Cells[1].Value == null)
                {
                    orderNum[j] = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    orderDate[j] = dataGridView1.Rows[i].Cells[1].Value.ToString();

                    menuName[j] = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    price[j] = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    num[j] = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);

                    fill_label(flag, orderDate[j], menuName, price, num);

                    totalPrice += (Convert.ToInt32(price[j]) * num[j]);
                    totalNum += num[j];
                    j++;
                    break;
                }

                //현재 행의 날짜와 다음 행의 날짜가 같으면 
                if (dataGridView1.Rows[i].Cells[1].Value.ToString() == dataGridView1.Rows[i + 1].Cells[1].Value.ToString())
                {
                    orderNum[j] = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    orderDate[j] = dataGridView1.Rows[i].Cells[1].Value.ToString();

                    menuName[j] = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    price[j] = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    num[j] = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);

                    fill_label(flag, orderDate[j], menuName, price, num);

                    totalPrice += (Convert.ToInt32(price[j]) * num[j]);
                    totalNum += num[j];
                    // totalNum += num;

                }

                //현재 행의 날짜와 다음 행의 날짜가 다르면
                else
                {
                    orderNum[j] = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    orderDate[j] = dataGridView1.Rows[i].Cells[1].Value.ToString();

                    menuName[j] = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    price[j] = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    num[j] = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);

                    fill_label(flag, orderDate[j], menuName, price, num);

                    totalPrice += (Convert.ToInt32(price[j]) * num[j]);
                    totalNum += num[j];
                    j++;
                    break;
                }

                j++;
                i++;

            }


            //총가격 라벨에 표시
            switch (flag)
            {
                case 0:
                    label1_4.Text = string.Format("[메뉴 {0}개] {1}원", j, totalPrice);
                    break;
                case 1:
                    label2_4.Text = string.Format("[메뉴 {0}개] {1}원", j, totalPrice);
                    break;
                case 2:
                    label3_4.Text = string.Format("[메뉴 {0}개] {1}원", j, totalPrice);
                    break;
                case 3:
                    label4_4.Text = string.Format("[메뉴 {0}개] {1}원", j, totalPrice);
                    break;
                case 4:
                    label5_4.Text = string.Format("[메뉴 {0}개] {1}원", j, totalPrice);
                    break;
                case 5:
                    label6_4.Text = string.Format("[메뉴 {0}개] {1}원", j, totalPrice);
                    break;
                case 6:
                    label7_4.Text = string.Format("[메뉴 {0}개] {1}원", j, totalPrice);
                    break;
                case 7:
                    label8_4.Text = string.Format("[메뉴 {0}개] {1}원", j, totalPrice);
                    break;
                case 8:
                    label9_4.Text = string.Format("[메뉴 {0}개] {1}원", j, totalPrice);
                    break;
            }

            //데이터 그리드뷰에서 해당 주문 삭제
            for (int temp = 0; temp < j; temp++)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[0]);
            }

        }

        public void fill_label(int flag, string date, string[] menuName, string[] price, int[] num)
        {
            //int nameLen = strlen;
            int numLen = 0;
            int i = 0, j = 0;

            switch (flag)
            {
                case 0:
                    label1_1.Text = Convert.ToDateTime(date).ToString("MM월 dd일 (ddd)");
                    label1_2.Text = Convert.ToDateTime(date).ToString("HH시 mm분 ss초");

                    label1_5.Text = "";
                    while (menuName[i] != null)
                    {
                        label1_5.Text += menuName[i]+ ",";
                        i++;
                    }
                    i = 0;

                    label1_6.Text = "";
                    while (price[i] != null)
                    {
                        label1_6.Text += price[i].ToString() + ",";
                        i++;
                    }
                    i = 0;

                    label1_7.Text = "";
                    while (num[j] != 0)
                    {
                        label1_7.Text += num[j]+ ",";
                        j++;
                    }
                    j = 0;

                    label1_8.Text = date.ToString();
                    //MessageBox.Show(date);
                    break;
                case 1:
                    label2_1.Text = Convert.ToDateTime(date).ToString("MM월 dd일 (ddd)");
                    label2_2.Text = Convert.ToDateTime(date).ToString("HH시 mm분 ss초");
                    label2_5.Text = "";
                    while (menuName[i] != null)
                    {
                        label2_5.Text += menuName[i] + ",";
                        i++;
                    }
                    i = 0;

                    label2_6.Text = "";
                    while (price[i] != null)
                    {
                        label2_6.Text += price[i].ToString() + ",";
                        i++;
                    }
                    i = 0;
                    label2_7.Text = "";
                    while (num[j] != 0)
                    {
                        label2_7.Text += num[j] + ",";
                        j++;
                    }
                    j = 0;
                    label2_8.Text = date.ToString();
                    break;
                case 2:
                    label3_1.Text = Convert.ToDateTime(date).ToString("MM월 dd일 (ddd)");
                    label3_2.Text = Convert.ToDateTime(date).ToString("HH시 mm분 ss초");
                    label3_5.Text = "";
                    while (menuName[i] != null)
                    {
                        label3_5.Text += menuName[i] + ",";
                        i++;
                    }
                    i = 0;
                    label3_6.Text = "";
                    while (price[i] != null)
                    {
                        label3_6.Text += price[i].ToString() + ",";
                        i++;
                    }
                    i = 0;
                    label3_7.Text = "";
                    while (num[j] != 0)
                    {
                        label3_7.Text += num[j] + ",";
                        j++;
                    }
                    j = 0;
                    label3_8.Text = date.ToString();
                    break;
                case 3:
                    label4_1.Text = Convert.ToDateTime(date).ToString("MM월 dd일 (ddd)");
                    label4_2.Text = Convert.ToDateTime(date).ToString("HH시 mm분 ss초");
                    label4_5.Text = "";
                    while (menuName[i] != null)
                    {
                        label4_5.Text += menuName[i] + ",";
                        i++;
                    }
                    i = 0;
                    label4_6.Text = "";
                    while (price[i] != null)
                    {
                        label4_6.Text += price[i].ToString() + ",";
                        i++;
                    }
                    i = 0;
                    label4_7.Text = "";
                    while (num[j] != 0)
                    {
                        label4_7.Text += num[j] + ",";
                        j++;
                    }
                    j = 0;
                    label4_8.Text = date.ToString();
                    break;
                case 4:
                    label5_1.Text = Convert.ToDateTime(date).ToString("MM월 dd일 (ddd)");
                    label5_2.Text = Convert.ToDateTime(date).ToString("HH시 mm분 ss초");
                    label5_5.Text = "";
                    while (menuName[i] != null)
                    {
                        label5_5.Text += menuName[i] + ",";
                        i++;
                    }
                    i = 0;
                    label5_6.Text = "";
                    while (price[i] != null)
                    {
                        label5_6.Text += price[i].ToString() + ",";
                        i++;
                    }
                    i = 0;
                    label5_7.Text = "";
                    while (num[j] != 0)
                    {
                        label5_7.Text += num[j] + ",";
                        j++;
                    }
                    j = 0;
                    label5_8.Text = date.ToString();
                    break;
                case 5:
                    label6_1.Text = Convert.ToDateTime(date).ToString("MM월 dd일 (ddd)");
                    label6_2.Text = Convert.ToDateTime(date).ToString("HH시 mm분 ss초");
                    label6_5.Text = "";
                    while (menuName[i] != null)
                    {
                        label6_5.Text += menuName[i] + ",";
                        i++;
                    }
                    i = 0;
                    label6_6.Text = "";
                    while (price[i] != null)
                    {
                        label6_6.Text += price[i].ToString() + ",";
                        i++;
                    }
                    i = 0;
                    label6_7.Text = "";
                    while (num[j] != 0)
                    {
                        label6_7.Text += num[j] + ",";
                        j++;
                    }
                    j = 0;
                    label6_8.Text = date.ToString();
                    break;
                case 6:
                    label7_1.Text = Convert.ToDateTime(date).ToString("MM월 dd일 (ddd)");
                    label7_2.Text = Convert.ToDateTime(date).ToString("HH시 mm분 ss초");
                    label7_5.Text = "";
                    while (menuName[i] != null)
                    {
                        label7_5.Text += menuName[i] + ",";
                        i++;
                    }
                    i = 0;
                    label7_6.Text = "";
                    while (price[i] != null)
                    {
                        label7_6.Text += price[i].ToString() + ",";
                        i++;
                    }
                    i = 0;
                    label7_7.Text = "";
                    while (num[j] != 0)
                    {
                        label7_7.Text += num[j] + ",";
                        j++;
                    }
                    j = 0;
                    label7_8.Text = date.ToString();
                    break;
                case 7:
                    label8_1.Text = Convert.ToDateTime(date).ToString("MM월 dd일 (ddd)");
                    label8_2.Text = Convert.ToDateTime(date).ToString("HH시 mm분 ss초");
                    label8_5.Text = "";
                    while (menuName[i] != null)
                    {
                        label8_5.Text += menuName[i] + ",";
                        i++;
                    }
                    i = 0;
                    label8_6.Text = "";
                    while (price[i] != null)
                    {
                        label8_6.Text += price[i].ToString() + ",";
                        i++;
                    }
                    i = 0;
                    label8_7.Text = "";
                    while (num[j] !=0)
                    {
                        label8_7.Text += num[j] + ",";
                        j++;
                    }
                    j = 0;
                    label8_8.Text = date.ToString();
                    break;
                case 8:
                    label9_1.Text = Convert.ToDateTime(date).ToString("MM월 dd일 (ddd)");
                    label9_2.Text = Convert.ToDateTime(date).ToString("HH시 mm분 ss초");
                    label9_5.Text = "";
                    while (menuName[i] != null)
                    {
                        label9_5.Text += menuName[i] + ",";
                        i++;
                    }
                    i = 0;
                    label9_6.Text = "";
                    while (price[i] != null)
                    {
                        label9_6.Text += price[i].ToString() + ",";
                        i++;
                    }
                    i = 0;
                    label9_7.Text = "";
                    while (num[j] != 0)
                    {
                        label9_7.Text += num[j] + ",";
                        j++;
                    }
                    j = 0;
                    label9_8.Text = date.ToString();
                    break;

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
      

        private void panel4_Click(object sender, EventArgs e)
        {
            panel_click p4 = new panel_click(label1_5.Text, label1_6.Text, label1_7.Text, label1_8.Text);
            p4.Show();
        }

        private void panel5_1_Click(object sender, EventArgs e)
        {
            panel_click p5 = new panel_click(label2_5.Text, label2_6.Text, label2_7.Text, label2_8.Text);
            p5.Show();
        }

        private void panel12_Click(object sender, EventArgs e)
        {
            panel_click p12 = new panel_click(label3_5.Text, label3_6.Text, label3_7.Text, label3_8.Text);
            p12.Show();
        }

        private void panel11_Click(object sender, EventArgs e)
        {
            panel_click p11 = new panel_click(label4_5.Text, label4_6.Text, label4_7.Text, label4_8.Text);
            p11.Show();
        }

        private void panel10_Click(object sender, EventArgs e)
        {
            panel_click p10 = new panel_click(label5_5.Text, label5_6.Text, label5_7.Text, label5_8.Text);
            p10.Show();
        }

        private void panel9_Click(object sender, EventArgs e)
        {
            panel_click p9 = new panel_click(label6_5.Text, label6_6.Text, label6_7.Text, label6_8.Text);
            p9.Show();
        }

        private void panel8_Click(object sender, EventArgs e)
        {
            panel_click p8 = new panel_click(label7_5.Text, label7_6.Text, label7_7.Text, label7_8.Text);
            p8.Show();
        }

        private void panel6_Click(object sender, EventArgs e)
        {
            panel_click p6 = new panel_click(label8_5.Text, label8_6.Text, label8_7.Text, label8_8.Text);
            p6.Show();
        }

        private void panel7_Click(object sender, EventArgs e)
        {
            panel_click p7 = new panel_click(label9_5.Text, label9_6.Text, label9_7.Text, label9_8.Text);
            p7.Show();
        }

       

        private void button17_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel5_1.Visible = false;
            panel12.Visible = false;
            panel11.Visible = false;
            panel10.Visible = false;
            panel9.Visible = false;
            panel8.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;

            button17.BackColor = Color.White;
            button17.ForeColor = Color.Black;
            button16.BackColor = Color.FromArgb(64, 64, 64);
            button16.ForeColor = Color.White;

            label1_3_1.Visible = true;
            label2_3_1.Visible = true;
            label3_3_1.Visible = true;
            label4_3_1.Visible = true;
            label5_3_1.Visible = true;
            label6_3_1.Visible = true;
            label7_3_1.Visible = true;
            label8_3_1.Visible = true;
            label9_3_1.Visible = true;

            label1_3.Visible = false;
            label2_3.Visible = false;
            label3_3.Visible = false;
            label4_3.Visible = false;
            label5_3.Visible = false;
            label6_3.Visible = false;
            label7_3.Visible = false;
            label8_3.Visible = false;
            label9_3.Visible = false;


            dataGridView_binding_ING();
            fillPanel();
        } //준비중 버튼

        private void button16_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel5_1.Visible = false;
            panel12.Visible = false;
            panel11.Visible = false;
            panel10.Visible = false;
            panel9.Visible = false;
            panel8.Visible = false;
            panel6.Visible = false;
            panel7.Visible = false;

            button16.BackColor = Color.White;
            button16.ForeColor = Color.Black;
            button17.BackColor = Color.FromArgb(64,64,64);
            button17.ForeColor = Color.White;

            label1_3_1.Visible = false;
            label2_3_1.Visible = false;
            label3_3_1.Visible = false;
            label4_3_1.Visible = false;
            label5_3_1.Visible = false;
            label6_3_1.Visible = false;
            label7_3_1.Visible = false;
            label8_3_1.Visible = false;
            label9_3_1.Visible = false;

            label1_3.Visible = true;
            label2_3.Visible = true;
            label3_3.Visible = true;
            label4_3.Visible = true;
            label5_3.Visible = true;
            label6_3.Visible = true;
            label7_3.Visible = true;
            label8_3.Visible = true;
            label9_3.Visible = true;

            dataGridView_binding();
            fillPanel();
        } //접수대기 버튼

        private void button4_Click(object sender, EventArgs e)
        {
            get_toket();
        }

        public void get_toket()
        {
            string token = string.Empty;
            string sql =
        "select token from orderMenu where currentDate = '#Date'";
            sql = sql.Replace("#Date", label1_8.Text);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            try
            {
                conn.Open();
                token = (string)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //MessageBox.Show(token);
            sendRequestAsync(token);
            conn.Close();

        }

        public async Task sendRequestAsync(string token)
        {
            string url = "http://ec2-13-124-57-226.ap-northeast-2.compute.amazonaws.com/pushNotification/pushNotification.php";
            HttpClient client = new HttpClient();
            
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                {"token",token },{"Message","주문하신 메뉴가 나왔습니다."}
            };
            FormUrlEncodedContent content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();

        }
    }
}
