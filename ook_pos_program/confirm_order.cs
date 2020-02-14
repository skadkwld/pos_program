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
    public partial class confirm_order : Form
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

            conn.Open();
            foreach (DataGridViewRow dgr in dataGridView1.SelectedRows)
            {
                id = Convert.ToInt32(dgr.Cells[0].Value.ToString());
                f_to_ing = "update orderMenu set confirmed='#ing' where idorderMenu=#idorderMenu";
                f_to_ing = f_to_ing.Replace("#idorderMenu", id.ToString());
                f_to_ing = f_to_ing.Replace("#ing", ing.ToString());
                MySqlDataAdapter adapter = new MySqlDataAdapter(f_to_ing, conn);
                MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
            }


            conn.Close();
            dataGridView1.DataSource = dt;

            dataGridView_binding(); //업데이트 된 DB를 새로고침 하는 기능
        } //주문 접수 버튼

        public void dataGridView_binding()
        {
            MySqlConnection conn = new MySqlConnection(strconn);

            conn.Open();
            button6.Enabled = false;
            string binding = "select o.idorderMenu as '주문번호',o.currentDate as '주문날짜',o.name as '메뉴',m.price as '가격',o.count as '개수' from orderMenu o,menu m where o.name=m.name and o.confirmed='#confirmed' order by o.currentDate desc,o.idorderMenu desc";
            string cnt_sameDate= "select count(currentDate) from orderMenu where '#sameDate' and confirmed = '#confirmed'"; //최대 idorderMenu의 날짜와 같은 날짜를 가지는 주문의 개수를 구하는 쿼리문
            string max_idorderMenu = "select max(idorderMenu) from orderMenu where confirmed='#confirmed'";
            string find_sameDate = "select currentDate from orderMenu where idorderMenu='#idorderMenu'"; //최대 idorderMenu의 날짜를 구하는 쿼리문
            string select_price = "select sum(m.price) from orderMenu o, menu m where o.currentDate='#currentDate' and o.name=m.name";
            int maxIdorderMenu = 0;
            string same_date = string.Empty;
            string sameDate_count = string.Empty;

            

            int price = 0;


            //최대 idorderMenu를 구하기 위한 코드--------------------------------------------------------------------------------

            //max_idorderMenu = max_idorderMenu.Replace("#confirmed", f.ToString());
            //MySqlCommand find_max = new MySqlCommand(max_idorderMenu, conn); //최대 idorderMenu값 찾기위한 쿼리문의 커맨더
            //maxIdorderMenu = Convert.ToInt32(find_max.ExecuteScalar());
            //MessageBox.Show(maxIdorderMenu.ToString());

            //------------------------------------------------------------------------------------------------------------------

            //같은 날짜를 구하기 위한 코드----------------------------------------------------------------------------------------

            //find_sameDate = find_sameDate.Replace("#idorderMenu", maxIdorderMenu.ToString());
            //MySqlCommand findDate = new MySqlCommand(find_sameDate, conn);
            //same_date = Convert.ToString(findDate.ExecuteScalar());
            //MessageBox.Show(same_date.ToString());

            //------------------------------------------------------------------------------------------------------------------

            //같은 날짜인 주문의 개수를 구하는 코드--------------------------------------------------------------------------------

            //cnt_sameDate = cnt_sameDate.Replace("#confirmed", f.ToString());
            //cnt_sameDate = cnt_sameDate.Replace("#sameDate", same_date.ToString());
            //MySqlCommand findCount = new MySqlCommand(cnt_sameDate, conn);
            //sameDate_count = Convert.ToString(findCount.ExecuteScalar());
            ////MessageBox.Show(sameDate_count.ToString());
            

            //------------------------------------------------------------------------------------------------------------------

            //같은 날짜의 주문의 가격의 누적합을 구하는 코드------------------------------------------------------------------------

            //select_price = select_price.Replace("#currentDate", same_date.ToString());
            //MySqlCommand findPrice = new MySqlCommand(select_price, conn);
            //price = Convert.ToInt32(findPrice.ExecuteScalar());
            ////MessageBox.Show(price.ToString());
            

            //------------------------------------------------------------------------------------------------------------------

            //필요없어질 수도 있는 코드-------------------------------------------------------------------------------------------

            //binding = binding.Replace("#confirmed", f.ToString());
            //MySqlDataAdapter adapter = new MySqlDataAdapter(binding, conn);
            //MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);

            //-------------------------------------------------------------------------------------------------------------------


            binding = binding.Replace("#confirmed", f.ToString());
            //MySqlCommand cmd = new MySqlCommand(binding, conn);
            //MySqlDataReader rdr = cmd.ExecuteReader();

            //while (rdr.Read())
            //{
            //    label1_1.Text = Convert.ToDateTime(rdr["주문날짜"]).ToString("MM월 dd일 (ddd)");
            //    label1_2.Text = Convert.ToDateTime(rdr["주문날짜"]).ToString("HH시 mm분 ss초");
            //    label1_4.Text = String.Format("[메뉴 {0}개] {1}원", sameDate_count,price);
            //}

            //데이터그리드뷰 코드-----------------------------------

            MySqlDataAdapter adapter = new MySqlDataAdapter(binding, conn);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;


            //-----------------------------------------------------

            conn.Close();
           // dataGridView1.DataSource = dt;
        }

        private void button4_MouseMove(object sender, MouseEventArgs e)
        {
            //button4.BackColor = Color.White;
        }

        private void button5_MouseMove(object sender, MouseEventArgs e)
        {
           // button5.BackColor = Color.White;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            //button4.BackColor = Color.DarkGray;
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            //button5.BackColor = Color.DarkGray;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView_binding();
            button8.Enabled = true;
            button5.BackColor = Color.DarkGray;
            button4.BackColor = Color.White;
        } //접수대기 버튼

        private void button5_Click(object sender, EventArgs e)
        {
            button6.Enabled = true;
            button8.Enabled = false;
            conn = new MySqlConnection(strconn);
            conn.Open();
            string binding = "select o.idorderMenu as '주문번호',o.currentDate as '주문날짜',o.name as '메뉴',m.price as '가격',o.count as '개수' from orderMenu o,menu m where o.name=m.name and o.confirmed='#confirmed' order by o.currentDate desc";
            binding = binding.Replace("#confirmed", ing.ToString());
            MySqlDataAdapter adapter = new MySqlDataAdapter(binding, conn);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
            DataTable dt = new DataTable();
            adapter.Fill(dt);


            conn.Close();
            dataGridView1.DataSource = dt;

            button4.BackColor = Color.DarkGray;
            button5.BackColor = Color.White;
        } //거래중 버튼

        private void button6_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(strconn);
            int id = 0;
            string f_to_ing = string.Empty;

            string name = string.Empty;
            string count = string.Empty;
            string price = string.Empty;

            conn.Open();

            foreach (DataGridViewRow dgr in dataGridView1.SelectedRows)
            {
                name = dgr.Cells[2].Value.ToString();
                count = dgr.Cells[4].Value.ToString();
                price = dgr.Cells[3].Value.ToString();

                id = Convert.ToInt32(dgr.Cells[0].Value.ToString());
                f_to_ing = "update orderMenu set confirmed='#t' where confirmed='#confirmed' and idorderMenu=#idorderMenu";
                f_to_ing = f_to_ing.Replace("#f", f.ToString());
                f_to_ing = f_to_ing.Replace("#confirmed", ing.ToString());
                f_to_ing = f_to_ing.Replace("#idorderMenu", id.ToString());
                MySqlDataAdapter adapter = new MySqlDataAdapter(f_to_ing, conn);
                MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
            }

            conn.Close();

            dataGridView1.DataSource = dt;
            updateING();

            //영주증 부분 코드
            WriteFile(name,count,price);
            ReadFile();
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();

        } //주문표 인쇄

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
            dataGridView1.DataSource = dt;
        }

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
            button7.BackColor = Color.White;
        }

        private void button8_MouseMove(object sender, MouseEventArgs e)
        {
            button8.BackColor = Color.White;
        }

        private void button6_MouseMove(object sender, MouseEventArgs e)
        {
            button6.BackColor = Color.White;
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            button6.BackColor = Color.LightGray;
        }

        private void button7_MouseLeave(object sender, EventArgs e)
        {
            button7.BackColor = Color.LightGray;
        }

        private void button8_MouseLeave(object sender, EventArgs e)
        {
            button8.BackColor = Color.LightGray;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            orderPanel op = new orderPanel();
            
            Panel panel = new Panel();
            Panel panel2 = new Panel();
            panel.Size= new System.Drawing.Size(717, 100);
            panel2.Size = new System.Drawing.Size(717, 100);
            panel3.Controls.Add(panel);
            panel3.Controls.Add(panel2);
            panel.Controls.Add(op);
            panel2.Controls.Add(op);
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

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
