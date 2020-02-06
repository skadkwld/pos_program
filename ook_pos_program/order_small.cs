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
    public partial class order_small : UserControl
    {
        string strconn = string.Format(@"server=localhost;database=kiosk;user=root;password=1234");
        //string strconn = string.Format(@"server=mydb.cmkvwrcpwpyc.ap-northeast-2.rds.amazonaws.com;database=ook;user=admin;password=young1700!");
        order order = new order();
        adapter ad = new adapter();
        string whipping_t = "true";
        string whipping_f = "false";
        public delegate int delClick(int price, DataTable dt,int HowMany);
        public event delClick eventClick;
        DataTable dt;

        public order_small()
        {
            InitializeComponent();
        }

        private void btn_coldbrew_1_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(strconn);

            order.coldbrew_1[0]++;  //order 클래스의 americano 변수(개수 나타냄) 1증가
            DateTime row_date = DateTime.Now;
            string date = row_date.ToString("yyyy-MM-dd HH:mm:ss");
            order.price += 4500;  //order 클래스의 총 주문 비용 3000원 만큼 증가

            //---------------------------------
            DataTable table = new DataTable();

            //---------------------------------

            string name = "콜드 폼 콜드브루 스몰";
            int sugar = 50;
            int sumprice = 0;
            string whipping = "true";
            //panel_etc.Visible = true;
            sumprice = 4500 * order.coldbrew_1[0];

            order_button(order.coldbrew_1[0], date, name, sugar, 4500, sumprice, whipping);
           
        } //콜드 폼 콜드브루 스몰

        private void order_button(int count, string date, string name, int sugar, int price, int sumprice, string whipping)
        {
            MySqlConnection conn = new MySqlConnection(strconn);
            order.order_id++;
            //panel_etc.Visible = true;
            //mysql에 데이터 삽입
            try
            {
                conn.Open();
                //string insert = "insert into orderMenu(name,count,sugar,whippedCream,currentDate,userID) values('"  + name + "','" + count + "','" + "50" + "','"  + whipping_t + "','" + date + "','" + "uiju" + "')";
                //MySqlCommand cmd = new MySqlCommand(insert, conn);
                //cmd.ExecuteNonQuery();
                //MessageBox.Show("입력 성공");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            order.HowManyInsert++;

            dt.Rows.Add(name, count.ToString(), "50%", "None", date);
            //dt = ad.ConnectDB();
            eventClick(order.price, dt,order.HowManyInsert);

        } //메뉴 버튼 눌렀을때 공통으로 돌아가는 코드

        public void dt_exchange(DataTable table)
        {
            dt = table;
        }
    }
}
