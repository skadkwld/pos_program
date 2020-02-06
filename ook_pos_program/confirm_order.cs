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
        public confirm_order()
        {
            
            InitializeComponent();
            dataGridView_binding();
        }

        public confirm_order(int newRow)
        {
            InitializeComponent();
            dataGridView_binding();
            rowNum = newRow;
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
            string binding = "select o.idorderMenu as '주문번호',o.currentDate as '주문날짜',o.name as '메뉴',m.price as '가격',o.count as '개수' from orderMenu o,menu m where o.name=m.name and o.confirmed='#confirmed' order by o.currentDate desc";
            binding = binding.Replace("#confirmed", f.ToString());
            MySqlDataAdapter adapter = new MySqlDataAdapter(binding, conn);
            MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
            DataTable dt = new DataTable();
            adapter.Fill(dt);


            conn.Close();
            dataGridView1.DataSource = dt;
        }

        private void button4_MouseMove(object sender, MouseEventArgs e)
        {
            button4.BackColor = Color.SkyBlue;
        }

        private void button5_MouseMove(object sender, MouseEventArgs e)
        {
            button5.BackColor = Color.SkyBlue;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.BackColor = Color.MidnightBlue; 
        }

        private void button5_MouseLeave(object sender, EventArgs e)
        {
            button5.BackColor = Color.MidnightBlue;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView_binding();
            button8.Enabled = true;
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
        } //거래중 버튼

        private void button6_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(strconn);
            int id = 0;
            string f_to_ing = string.Empty;

            conn.Open();
            foreach (DataGridViewRow dgr in dataGridView1.SelectedRows)
            {
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
    }
}
