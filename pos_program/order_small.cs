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
        order order = new order();
        adapter ad = new adapter();
        string whipping_t = "true";
        string whipping_f = "false";
        public delegate int delClick(int price, DataTable dt,int HowMany);
        public event delClick eventClick;
        DataTable dt;
        int mini_HowMany = 0;
        int sumprice = 0;

        public order_small()
        {
            InitializeComponent();
        }

        #region menuButton

        private void btn_coldbrew_1_Click(object sender, EventArgs e)
        {
            string name = "콜드 폼 콜드브루 스몰";
            order.coldbrew_1[0]++;
            mini_HowMany = order.coldbrew_1[0];
            order_button2(name, order.coldbrew_1[0]);

        } //콜드 폼 콜드브루 스몰

        private void btn_coldbrew_2_Click(object sender, EventArgs e)
        {
            string name = "바닐라크림 콜드브루 스몰";
            order.coldbrew_2[0]++;
            mini_HowMany = order.coldbrew_2[0];
            order_button2(name, order.coldbrew_2[0]);

        } //바닐라크림 콜드브루 스몰

        private void btn_coldbrew_3_Click(object sender, EventArgs e)
        {
            string name = "콜드브루 스몰";
            order.coldbrew_3[0]++;
            mini_HowMany = order.coldbrew_3[0];
            order_button2(name, order.coldbrew_3[0]);

        } //콜드브루 스몰

        private void btn_sm_taro_Click(object sender, EventArgs e)
        {
            string name = "타로스무디 스몰";
            order.smudi_taro[0]++;
            mini_HowMany = order.smudi_taro[0];
            order_button2(name, order.smudi_taro[0]);
        } //타로스무디 스몰

        private void btn_sm_mango_Click(object sender, EventArgs e)
        {
            string name = "망고스무디 스몰";
            order.smudi_mango[0]++;
            mini_HowMany = order.smudi_mango[0];
            order_button2(name, order.smudi_mango[0]);
        } //망고스무디 스몰

        private void btn_sm_choco_Click(object sender, EventArgs e)
        {
            string name = "초콜렛스무디 스몰";
            order.smudi_choco[0]++;
            mini_HowMany = order.smudi_choco[0];
            order_button2(name, order.smudi_choco[0]);
        } //초콜렛스무디 스몰

        private void btn_sm_moka_Click(object sender, EventArgs e)
        {
            string name = "모카스무디 스몰";
            order.smudi_moca[0]++;
            mini_HowMany = order.smudi_moca[0];
            order_button2(name, order.smudi_moca[0]);
        } //모카스무디 스몰

        #endregion

        private void order_button(int count, string date, string name, int sugar, int price, int sumprice, string whipping)
        {
            order.order_id++; //뭐지?

            dt.Rows.Add(name, count.ToString(), "50%", "None", date);
            eventClick(sumprice, dt, mini_HowMany);

        } //메뉴 버튼 눌렀을때 공통으로 돌아가는 코드

        public void order_button2(string name,int count)
        {
            int price = 0;
            int sugar = 50;
            string whipping = "true";
            DateTime row_date = DateTime.Now;
            string date = row_date.ToString("yyyy-MM-dd HH:mm:ss");

            order.information.TryGetValue(name, out price); //order 클래스의 딕셔너리에서 가격 불러옴
            //order.price += price;
            sumprice += (price*count);

            //sumprice = price * count;

            order_button(count, date, name, sugar, price, sumprice, whipping);
        }

        public void dt_exchange(DataTable table)
        {
            dt = table;
        }

        
    }
}
