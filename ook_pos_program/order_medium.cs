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

    public partial class order_medium : UserControl
    {
        public delegate int delCilck_m(int price, DataTable dt, int HowMany);
        public event delCilck_m eventClick_m;
        order order = new order();
        adapter ad = new adapter();
        string whipping_t = "true";
        string whipping_f = "false";
        int sumprice = 0;

        DataTable dt;
        int mini_HowMany = 0;

        public order_medium()
        {
            InitializeComponent();
        }
        #region menuButton
        private void button95_Click(object sender, EventArgs e)
        {
            string name = "콜드 폼 콜드브루 미디움";
            order.coldbrew_1[1]++;
            mini_HowMany = order.coldbrew_1[1];
            order_button2(name, order.coldbrew_1[1]);
        } //콜드 폼 콜드브루 미디움

        private void button94_Click(object sender, EventArgs e)
        {
            string name = "바닐라크림 콜드브루 미디움";
            order.coldbrew_2[1]++;
            mini_HowMany = order.coldbrew_2[1];
            order_button2(name, order.coldbrew_2[1]);
        } //바닐라 크림 콜드브루 미디움

        private void button96_Click(object sender, EventArgs e)
        {
            string name = "콜드브루 미디움";
            order.coldbrew_3[1]++;
            mini_HowMany = order.coldbrew_3[1];
            order_button2(name, order.coldbrew_3[1]);
        } //콜드부루 미디움

        private void button124_Click(object sender, EventArgs e)
        {
            string name = "타로스무디 미디움";
            order.smudi_taro[1]++;
            mini_HowMany = order.smudi_taro[1];
            order_button2(name, order.smudi_taro[1]);
        } //타로스무디 미디움

        private void button125_Click(object sender, EventArgs e)
        {
            string name = "망고스무디 미디움";
            order.smudi_mango[1]++;
            mini_HowMany = order.smudi_mango[1];
            order_button2(name, order.smudi_mango[1]);
        } //망고스무디 미디움

        private void button127_Click(object sender, EventArgs e)
        {
            string name = "초콜렛스무디 미디움";
            order.smudi_choco[1]++;
            mini_HowMany = order.smudi_choco[1];
            order_button2(name, order.smudi_choco[1]);
        } //초콜렛스무디 미디움

        private void button129_Click(object sender, EventArgs e)
        {
            string name = "모카스무디 미디움";
            order.smudi_moca[1]++;
            mini_HowMany = order.smudi_moca[1];
            order_button2(name, order.smudi_moca[1]);
        } //모카스무디 미디움

        #endregion

        private void order_button(int count, string date, string name, int sugar, int price, int sumprice, string whipping)
        {
            order.order_id++; //뭐지?

            dt.Rows.Add(name, count.ToString(), "50%", "None", date);
            eventClick_m(sumprice, dt, mini_HowMany);
        } //메뉴 버튼 눌렀을때 공통으로 돌아가는 코드

        public void order_button2(string name, int count)
        {
            int price = 0;
            int sugar = 50;
            string whipping = "true";
            DateTime row_date = DateTime.Now;
            string date = row_date.ToString("yyyy-MM-dd HH:mm:ss");

            order.information.TryGetValue(name, out price); //order 클래스의 딕셔너리에서 가격 불러옴
            //order.price += price;

            sumprice += (price * count);

            order_button(count, date, name, sugar, price, sumprice, whipping);
        }

        public void dt_exchange(DataTable table)
        {
            dt = table;
        }
    }
}
