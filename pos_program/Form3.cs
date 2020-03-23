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
    public partial class Form3 : Form
    {
        SoundPlayer sp = new SoundPlayer();
        string file = (string)@"C:\주문이 접수되었습니다.wav";
        //string strconn = string.Format(@"server=localhost;database=kiosk;user=root;password=1234");
        string ing = "ING..";
        string t = "T";
        string f = "F";
        int rowNum = 0;
        string strconn = string.Format(@"server=mydb.cmkvwrcpwpyc.ap-northeast-2.rds.amazonaws.com;database=ook;user=admin;password=young1700!");
        order order = new order();
        adapter ad = new adapter();
        DataTable dt = null;
        MySqlConnection conn;
        string[,] arr_for_insert = new string[10, 5];
        int rowCount = 0;
        string docName = "test.txt";
        string docPath = @"C:\Users\eoaud\source\repos\ook_pos_program\ook_pos_program\";
        string stringToPrint;
        string documentContents;

        public Form3()
        {
            InitializeComponent();
            dataGridView_binding();
            fillPanel();
        }

        public Form3(string[,] strData, int HowManyInsert, int price)
        {
            InitializeComponent();
            dataGridView_binding();
            getArray(strData, HowManyInsert, price);
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        public void dataGridView_binding()
        {
            int panelCount = 0;
            MySqlConnection conn = new MySqlConnection(strconn);

            conn.Open();
            //button6.Enabled = false;

            string binding = "select o.idorderMenu as '주문번호',o.currentDate as '주문날짜',o.name as '메뉴',m.price as '가격',o.count as '개수' from orderMenu o,menu m where o.name=m.name and o.confirmed='#confirmed' order by o.currentDate desc,o.idorderMenu desc";

            
            //데이터그리드뷰 코드-----------------------------------

            binding = binding.Replace("#confirmed", f.ToString());
            MySqlDataAdapter adapter = new MySqlDataAdapter(binding, conn);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;

            conn.Close();
            // dataGridView1.DataSource = dt;
        }
        public void getArray(string[,] DBarr, int count, int price)
        {
            //DBarr = new string[10, 5];
            arr_for_insert = DBarr;
            rowCount = count;

        }

        public void WriteFile(string name, string count, string price)
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


                outputFile.WriteLine(string.Format("{0}                    {1}        {2}", name, count, price));

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

        public void updateING()
        {
            conn = new MySqlConnection(strconn);
            conn.Open();
            string binding = "select o.idorderMenu as '주문번호',o.currentDate as '주문날짜',o.name as '메뉴',m.price as '가격',o.count as '개수' from orderMenu o,menu m where o.name=m.name and o.confirmed='#confirmed' order by o.currentDate desc";
            binding = binding.Replace("#confirmed", ing.ToString());
            MySqlDataAdapter adapter = new MySqlDataAdapter(binding, conn);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
            DataTable dt = new DataTable();
            adapter.Fill(dt);


            conn.Close();
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

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

        private void button4_Click(object sender, EventArgs e)
        {

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
                        panel5.Visible = true;
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
                        //case 5:
                        //    panel9.Visible = true;
                        //    break;
                        //case 6:
                        //    panel8.Visible = true;
                        //    break;
                        //case 7:
                        //    panel6.Visible = true;
                        //    break;
                        //case 8:
                        //    panel7.Visible = true;
                        //    break;

                }

                if (dataGridView1.Rows.Count==1) break;
                panelNum++;
            }
        }
        
        public void getDatagridview(int flag)
        {
            int i = 0, j = 0;
            
            string[] orderNum = new string[10];
            string[] orderDate = new string[10];
            string[] menuName = new string[10];
            int price = 0;
            int[] num = new int[10];

            int totalPrice = 0;
            int totalNum = 0;


            while (true)
            {
                
                //데이터그리드뷰에 이 행까지만 존재하면
                if (dataGridView1.Rows[i+1].Cells[1].Value == null)
                {
                    orderNum[j] = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    orderDate[j] = dataGridView1.Rows[i].Cells[1].Value.ToString();

                    menuName[j] = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    price = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);
                    num[j] = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value.ToString());

                    fill_label(flag, orderDate[j],menuName[j],price,num[j]);

                    totalPrice += (price*num[j]);
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
                    price = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);
                    num[j] = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value.ToString());

                    fill_label(flag, orderDate[j], menuName[j], price, num[j]);

                    totalPrice += (price * num[j]);
                    totalNum += num[j];
                    // totalNum += num;

                }

                //현재 행의 날짜와 다음 행의 날짜가 다르면
                else
                {
                    orderNum[j] = dataGridView1.Rows[i].Cells[0].Value.ToString();
                    orderDate[j] = dataGridView1.Rows[i].Cells[1].Value.ToString();

                    menuName[j] = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    price = Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);
                    num[j] = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value.ToString());

                    fill_label(flag, orderDate[j], menuName[j], price, num[j]);

                    totalPrice += (price * num[j]);
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
                    //case 5:
                    //    panel9.Visible = true;
                    //    break;
                    //case 6:
                    //    panel8.Visible = true;
                    //    break;
                    //case 7:
                    //    panel6.Visible = true;
                    //    break;
                    //case 8:
                    //    panel7.Visible = true;
                    //    break;
            }
            
            //데이터 그리드뷰에서 해당 주문 삭제
            for (int temp = 0; temp < j; temp++)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[0]);
            }

        }

        public void fill_label(int flag,string date,string menuName,int price,int num)
        {

            switch (flag)
            {
                case 0:
                    label1_1.Text = Convert.ToDateTime(date).ToString("MM월 dd일 (ddd)");
                    label1_2.Text = Convert.ToDateTime(date).ToString("HH시 mm분 ss초");
                    label1_5.Text = menuName;
                    label1_6.Text = price.ToString();
                    label1_7.Text = num.ToString();
                    //MessageBox.Show(date);
                    break;
                case 1:
                    label2_1.Text = Convert.ToDateTime(date).ToString("MM월 dd일 (ddd)");
                    label2_2.Text = Convert.ToDateTime(date).ToString("HH시 mm분 ss초");
                    label2_5.Text = menuName;
                    label2_6.Text = price.ToString();
                    label2_7.Text = num.ToString();
                    break;
                case 2:
                    label3_1.Text = Convert.ToDateTime(date).ToString("MM월 dd일 (ddd)");
                    label3_2.Text = Convert.ToDateTime(date).ToString("HH시 mm분 ss초");
                    label3_5.Text = menuName;
                    label3_6.Text = price.ToString();
                    label3_7.Text = num.ToString();
                    break;
                case 3:
                    label4_1.Text = Convert.ToDateTime(date).ToString("MM월 dd일 (ddd)");
                    label4_2.Text = Convert.ToDateTime(date).ToString("HH시 mm분 ss초");
                    label4_5.Text = menuName;
                    label4_6.Text = price.ToString();
                    label4_7.Text = num.ToString();
                    break;
                case 4:
                    label5_1.Text = Convert.ToDateTime(date).ToString("MM월 dd일 (ddd)");
                    label5_2.Text = Convert.ToDateTime(date).ToString("HH시 mm분 ss초");
                    label5_5.Text = menuName;
                    label5_6.Text = price.ToString();
                    label5_7.Text = num.ToString();
                    break;
                    //case 5:
                    //    panel9.Visible = true;
                    //    break;
                    //case 6:
                    //    panel8.Visible = true;
                    //    break;
                    //case 7:
                    //    panel6.Visible = true;
                    //    break;
                    //case 8:
                    //    panel7.Visible = true;
                    //    break;

            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Click(object sender, EventArgs e)
        {
            //panel_click p4 = new panel_click(label1_5.Text, label1_6.Text, label1_7.Text);
            //p4.Show();
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            //panel_click p5 = new panel_click(label2_5.Text, label2_6.Text, label2_7.Text);
            //p5.Show();
        }

        private void panel12_Click(object sender, EventArgs e)
        {
            //panel_click p12 = new panel_click(label3_5.Text, label3_6.Text, label3_7.Text);
            //p12.Show();
        }

        private void panel11_Click(object sender, EventArgs e)
        {
            //panel_click p11 = new panel_click(label4_5.Text, label4_6.Text, label4_7.Text);
            //p11.Show();
        }

        private void panel10_Click(object sender, EventArgs e)
        {
            //panel_click p10 = new panel_click(label5_5.Text, label5_6.Text, label5_7.Text);
            //p10.Show();
        }
    }
}
