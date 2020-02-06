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
    public partial class Form_PayCash : MetroFramework.Forms.MetroForm
    {
        string[,] arr_for_insert = new string[10, 5];
        order order = new order();
        int rowCount = 0;
        string strconn = string.Format(@"server=localhost;database=kiosk;user=root;password=1234");
        //string strconn = string.Format(@"server=mydb.cmkvwrcpwpyc.ap-northeast-2.rds.amazonaws.com;database=ook;user=admin;password=young1700!");
        adapter ad = new adapter();

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
                    string insert = "insert into oneOrder(name,count,sugar,whippedCream,currentDate,userID) values('" + arr_for_insert[i,j] + "','" + arr_for_insert[i,j+1] + "','" + arr_for_insert[i, j+2] + "','" + arr_for_insert[i, j+3] + "','" + arr_for_insert[i, j+4] + "','" + "uiju" + "')";
                    MySqlCommand cmd = new MySqlCommand(insert, conn);
                    cmd.ExecuteNonQuery();
                    j = 0;
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } //결제 버튼

        public void getArray(string[,] DBarr,int count)
        {
            //DBarr = new string[10, 5];
            arr_for_insert = DBarr;
            rowCount = count;

            for(int i = 0; i < count; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    listBox1.Items.Add(DBarr[i, j]);
                }
            }
        }
    }
}
