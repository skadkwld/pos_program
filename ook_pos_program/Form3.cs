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
        string strconn = string.Format(@"server=localhost;database=kiosk;user=root;password=1234");
        string ing = "ING..";
        string t = "T";
        string f = "F";
        int rowNum = 0;
        //string strconn = string.Format(@"server=mydb.cmkvwrcpwpyc.ap-northeast-2.rds.amazonaws.com;database=ook;user=admin;password=young1700!");
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
            button6.Enabled = false;

            string sumCount= "select count(*) from orderMenu where confirmed = '#confirmed'";
            string binding = "select o.idorderMenu as '주문번호',o.currentDate as '주문날짜',o.name as '메뉴',m.price as '가격',o.count as '개수' from orderMenu o,menu m where o.name=m.name and o.confirmed='#confirmed' and currentDate='#currentDate' order by o.currentDate desc,o.idorderMenu desc";
            string cnt_sameDate = "select count(currentDate) from orderMenu where currentDate='#sameDate' and confirmed = '#confirmed'"; //최대 idorderMenu의 날짜와 같은 날짜를 가지는 주문의 개수를 구하는 쿼리문
            string max_idorderMenu = "select max(idorderMenu) from orderMenu where confirmed='#confirmed'";
            string find_sameDate = "select currentDate from orderMenu where idorderMenu='#idorderMenu' order by currentDate desc,idorderMenu desc"; //최대 idorderMenu의 날짜를 구하는 쿼리문
            string select_price = "select sum(m.price) from orderMenu o, menu m where o.currentDate='#currentDate' and o.name=m.name and o.confirmed='#confirmed'";
            int maxIdorderMenu =0;
            string same_date = string.Empty;
            string sameDate_count = string.Empty;

            int sameDateCount_temp = 0;

            int price = 0;

            int sum_count = 0;

            sumCount = sumCount.Replace("#confirmed", f.ToString());
            MySqlCommand findSumCount = new MySqlCommand(sumCount, conn);
            sum_count = Convert.ToInt32(findSumCount.ExecuteScalar());

           

                //최대 idorderMenu를 구하기 위한 코드--------------------------------------------------------------------------------

                max_idorderMenu = max_idorderMenu.Replace("#confirmed", f.ToString());
                MySqlCommand find_max = new MySqlCommand(max_idorderMenu, conn); //최대 idorderMenu값 찾기위한 쿼리문의 커맨더
                maxIdorderMenu = Convert.ToInt32(find_max.ExecuteScalar());
                MessageBox.Show(maxIdorderMenu.ToString());

                //------------------------------------------------------------------------------------------------------------------

                //같은 날짜를 구하기 위한 코드----------------------------------------------------------------------------------------

                find_sameDate = find_sameDate.Replace("#idorderMenu", maxIdorderMenu.ToString());
                MySqlCommand findDate = new MySqlCommand(find_sameDate, conn);
                same_date = Convert.ToString(findDate.ExecuteScalar());
                MessageBox.Show(same_date.ToString());

                //--------------------------------------------------------

                //같은 날짜인 주문의 개수를 구하는 코드--------------------------------------------------------------------------------

                cnt_sameDate = cnt_sameDate.Replace("#confirmed", f.ToString());
                cnt_sameDate = cnt_sameDate.Replace("#sameDate", same_date.ToString());
                MySqlCommand findCount = new MySqlCommand(cnt_sameDate, conn);
                sameDate_count = Convert.ToString(findCount.ExecuteScalar());
                MessageBox.Show(sameDate_count.ToString());


                //------------------------------------------------------------------------------------------------------------------

                //같은 날짜의 주문의 가격의 누적합을 구하는 코드------------------------------------------------------------------------

                select_price = select_price.Replace("#currentDate", same_date.ToString());
                select_price = select_price.Replace("#confirmed", f.ToString());
                MySqlCommand findPrice = new MySqlCommand(select_price, conn);
                price = Convert.ToInt32(findPrice.ExecuteScalar());
                //MessageBox.Show(price.ToString());


                //------------------------------------------------------------------------------------------------------------------

            


            binding = binding.Replace("#confirmed", f.ToString());
            binding = binding.Replace("#currentDate", same_date.ToString());
            MySqlCommand cmd = new MySqlCommand(binding, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            sameDateCount_temp = Convert.ToInt32(sameDate_count);
            while (rdr.Read())
            {
                

                switch (panelCount) {
                    case 0:
                        label1_1.Text = Convert.ToDateTime(rdr["주문날짜"]).ToString("MM월 dd일 (ddd)");
                        label1_2.Text = Convert.ToDateTime(rdr["주문날짜"]).ToString("HH시 mm분 ss초");
                        label1_4.Text = String.Format("[메뉴 {0}개] {1}원", sameDate_count, price);
                        break;
                    case 1:
                        panel5.Visible = true;
                        label2_1.Text = Convert.ToDateTime(rdr["주문날짜"]).ToString("MM월 dd일 (ddd)");
                        label2_2.Text = Convert.ToDateTime(rdr["주문날짜"]).ToString("HH시 mm분 ss초");
                        label2_4.Text = String.Format("[메뉴 {0}개] {1}원", sameDate_count, price);
                        break;
                    case 2:
                        panel12.Visible = true;
                        label3_1.Text = Convert.ToDateTime(rdr["주문날짜"]).ToString("MM월 dd일 (ddd)");
                        label3_2.Text = Convert.ToDateTime(rdr["주문날짜"]).ToString("HH시 mm분 ss초");
                        label3_4.Text = String.Format("[메뉴 {0}개] {1}원", sameDate_count, price);
                        break;
                    case 3:
                        panel11.Visible = true;
                        label4_1.Text = Convert.ToDateTime(rdr["주문날짜"]).ToString("MM월 dd일 (ddd)");
                        label4_2.Text = Convert.ToDateTime(rdr["주문날짜"]).ToString("HH시 mm분 ss초");
                        label4_4.Text = String.Format("[메뉴 {0}개] {1}원", sameDate_count, price);
                        break;
                    case 4:
                        panel10.Visible = true;
                        label5_1.Text = Convert.ToDateTime(rdr["주문날짜"]).ToString("MM월 dd일 (ddd)");
                        label5_2.Text = Convert.ToDateTime(rdr["주문날짜"]).ToString("HH시 mm분 ss초");
                        label5_4.Text = String.Format("[메뉴 {0}개] {1}원", sameDate_count, price);
                        break;
                    case 5:
                        panel9.Visible = true;
                        label6_1.Text = Convert.ToDateTime(rdr["주문날짜"]).ToString("MM월 dd일 (ddd)");
                        label6_2.Text = Convert.ToDateTime(rdr["주문날짜"]).ToString("HH시 mm분 ss초");
                        label6_4.Text = String.Format("[메뉴 {0}개] {1}원", sameDate_count, price);
                        break;
                    case 6:
                        panel8.Visible = true;
                        label7_1.Text = Convert.ToDateTime(rdr["주문날짜"]).ToString("MM월 dd일 (ddd)");
                        label7_2.Text = Convert.ToDateTime(rdr["주문날짜"]).ToString("HH시 mm분 ss초");
                        label7_4.Text = String.Format("[메뉴 {0}개] {1}원", sameDate_count, price);
                        break;
                    case 7:
                        panel6.Visible = true;
                        label8_1.Text = Convert.ToDateTime(rdr["주문날짜"]).ToString("MM월 dd일 (ddd)");
                        label8_2.Text = Convert.ToDateTime(rdr["주문날짜"]).ToString("HH시 mm분 ss초");
                        label8_4.Text = String.Format("[메뉴 {0}개] {1}원", sameDate_count, price);
                        break;
                    case 8:
                        panel7.Visible = true;
                        label9_1.Text = Convert.ToDateTime(rdr["주문날짜"]).ToString("MM월 dd일 (ddd)");
                        label9_2.Text = Convert.ToDateTime(rdr["주문날짜"]).ToString("HH시 mm분 ss초");
                        label9_4.Text = String.Format("[메뉴 {0}개] {1}원", sameDate_count, price);
                        break;
                    case 9:
                        break;
            }
                sameDateCount_temp--;
                if (sameDateCount_temp >= 1) panelCount = panelCount;
                else panelCount++;

                //panelCount++;
            }


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
    }
}
