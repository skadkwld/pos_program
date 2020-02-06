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

    public partial class confirmOrder : MetroFramework.Forms.MetroForm
    {
        string strconn = string.Format(@"server=localhost;database=kiosk;user=root;password=1234");
        //string strconn = string.Format(@"server=mydb.cmkvwrcpwpyc.ap-northeast-2.rds.amazonaws.com;database=ook;user=admin;password=young1700!");
        order order = new order();
        adapter ad = new adapter();
        DataTable dt;
        //App_order appOrder;

        public confirmOrder()
        {
            InitializeComponent();
            dataGridView_binding();
        }


        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button41_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void dataGridView_binding()
        {
            MySqlConnection conn = new MySqlConnection(strconn);
            string f = "F";

            conn.Open();
            string binding = "select o.idorderMenu,o.currentDate,o.name,m.price,o.count from orderMenu o,menu m where o.name=m.name and o.confirmed='#confirmed' order by o.currentDate desc";
            binding = binding.Replace("#confirmed", f.ToString());
            MySqlDataAdapter adapter = new MySqlDataAdapter(binding, conn);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
            DataTable dt = new DataTable();
            adapter.Fill(dt);


            conn.Close();
            dataGridView1.DataSource = dt;
        }

        private void confirmOrder_Load(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(strconn);
            string t = "T";
            string f = "F";
            int id = 0;
            string f_to_t = string.Empty;
            //f_to_t = f_to_t.Replace("#t", t.ToString());
            //f_to_t = f_to_t.Replace("#f", f.ToString());

            conn.Open();
            foreach (DataGridViewRow dgr in dataGridView1.SelectedRows)
            {
                id = Convert.ToInt32(dgr.Cells[0].Value.ToString());
                f_to_t = "update orderMenu set confirmed='#t' where idorderMenu=#idorderMenu";
                f_to_t = f_to_t.Replace("#idorderMenu", id.ToString());
                f_to_t = f_to_t.Replace("#t", t.ToString());
                MySqlDataAdapter adapter = new MySqlDataAdapter(f_to_t, conn);
                MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
            }

            
            conn.Close();
            dataGridView1.DataSource = dt;

            dataGridView_binding(); //업데이트 된 DB를 새로고침 하는 기능

        } //주문접수 버튼(f를 t로)
    }
}
