using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ook_pos_program
{
    public partial class App_order : Form
    {
        int newRow = 0;
        //string strconn = string.Format(@"server=mydb.cmkvwrcpwpyc.ap-northeast-2.rds.amazonaws.com;database=ook;user=admin;password=young1700!");
        string strconn = string.Format(@"server=localhost;database=kiosk;user=root;password=1234");
        public delegate int delMessage(int idorderMenu);
        public event delMessage eventdelMessage;
        public App_order()
        {
            InitializeComponent();
        }

        public App_order(int row_number)
        {
            InitializeComponent();
            newRow = row_number;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            confirm_order co = new confirm_order(newRow);
            co.Show();
            MessageBox.Show(newRow.ToString());
            //co.dataGridView_input();
           // this.Close();
        }
    }
}
