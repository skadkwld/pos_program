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
using System.IO;


namespace ook_pos_program
{
    public partial class Form_PayCash : MetroFramework.Forms.MetroForm
    {
        string docName = "test.txt";
        string docPath = @"C:\Users\eoaud\source\repos\ook_pos_program\ook_pos_program\";
        string stringToPrint;
        string documentContents;

        string[,] arr_for_insert = new string[10, 5];
        order order = new order();
        int rowCount = 0;
        //string strconn = string.Format(@"server=localhost;database=kiosk;user=root;password=1234");
        MySqlCommand cmd;
        string strconn = string.Format(@"server=mydb.cmkvwrcpwpyc.ap-northeast-2.rds.amazonaws.com;database=ook;user=admin;password=young1700!");
        adapter ad = new adapter();
        int sumprice = 0;

        public Form_PayCash()
        {
            InitializeComponent();
           
            
        }

        private void button41_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button42_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(strconn);
            int j = 0;

            try
            {
                conn.Open();

                for (int i = 0; i < rowCount; i++)
                {
                    string insert = "insert into orderMenu(name,count,sugar,whippedCream,currentDate,userID,confirmed) values('" + arr_for_insert[i,j] + "','" + arr_for_insert[i,j+1] + "','" + arr_for_insert[i, j+2] + "','" + arr_for_insert[i, j+3] + "','" + arr_for_insert[i, j+4] + "','" + "uiju" + "','" +"T"+ "')";
                    cmd = new MySqlCommand(insert, conn);
                    cmd.ExecuteNonQuery();
                    j = 0;
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MessageBox.Show("주문이 완료되었습니다.");
        } //결제 버튼

        public void getArray(string[,] DBarr, int count, int price)
        {
            //DBarr = new string[10, 5];
            arr_for_insert = DBarr;
            rowCount = count;
            tb_money_get.Text = price.ToString();
           
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

        public void WriteFile()
        {
            int j = 0;
            int price = 0, sumprice=0;
            int length = 22;
            string name = string.Empty;
            
            using(StreamWriter outputFile=new StreamWriter(@"C:\Users\eoaud\source\repos\ook_pos_program\ook_pos_program\test.txt"))
            {
                outputFile.WriteLine("-----------------------------------------------------------");
                outputFile.WriteLine("메뉴                                      수량          금액");
                outputFile.WriteLine("-----------------------------------------------------------");

                for (int i = 0; i < rowCount; i++)
                {
                    name = arr_for_insert[i, j];
                    price = order.information[name];
                    sumprice += price;
                    outputFile.Write(string.Format("{0}", name));

                    //빈칸 맞추기
                    for(int a = 0; a < 31 - (name.Trim().Length); a++)
                    {
                        outputFile.Write(" ");
                    }

                    //outputFile.WriteLine(string.Format("{0}                 {1}          {2}", arr_for_insert[i, j], arr_for_insert[i, j + 1], price));
                    outputFile.WriteLine(string.Format("{0}          {1}",arr_for_insert[i, j + 1], price));
                    j = 0;
                }

                outputFile.WriteLine("-----------------------------------------------------------");
                outputFile.WriteLine(string.Format("총 결제금액 : {0}", sumprice));
                //outputFile.Close();
            }
        }

        private void Form_PayCash_Load(object sender, EventArgs e)
        {
            
            WriteFile();
            ReadFile();
        }

        private void printPreviewControl1_Click(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            WriteFile();
            ReadFile();
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

    }
}
