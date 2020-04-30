using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ook_pos_program
{
    public partial class order_large : UserControl
    {
        order order = new order();
        adapter ad = new adapter();
        string whipping_t = "true";
        string whipping_f = "false";
        public delegate int delCilck_l(int price, DataTable dt, int HowMany);
        public event delCilck_l eventClick_l;
        DataTable dt;
        int mini_HowMany = 0;
        int sumprice = 0;

        #region menuButton
        public order_large()
        {
            InitializeComponent();
        }

        private void button229_Click(object sender, EventArgs e)
        {
            string name = "콜드 폼 콜드브루 라지";
            order.coldbrew_1[2]++;
            mini_HowMany = order.coldbrew_1[2];
            order_button2(name, order.coldbrew_1[2]);
        } //콜드 폼 콜드브루 라지

        private void button228_Click(object sender, EventArgs e)
        {
            string name = "바닐라크림 콜드브루 라지";
            order.coldbrew_2[2]++;
            mini_HowMany = order.coldbrew_2[2];
            order_button2(name, order.coldbrew_2[2]);
        } //바닐라크림 콜드브루 라지

        private void button230_Click(object sender, EventArgs e)
        {
            string name = "콜드브루 라지";
            order.coldbrew_3[2]++;
            mini_HowMany = order.coldbrew_3[2];
            order_button2(name, order.coldbrew_3[2]);
        } //콜드브루 라지

        private void button258_Click(object sender, EventArgs e)
        {
            string name = "타로스무디 라지";
            order.smudi_taro[2]++;
            mini_HowMany = order.smudi_taro[2];
            order_button2(name, order.smudi_taro[2]);

        } //타로스무디 라지

        private void button259_Click(object sender, EventArgs e)
        {
            string name = "망고스무디 라지";
            order.smudi_mango[2]++;
            mini_HowMany = order.smudi_mango[2];
            order_button2(name, order.smudi_mango[2]);

        } //망고스무디 라지

        private void button261_Click(object sender, EventArgs e)
        {
            string name = "초콜렛스무디 라지";
            order.smudi_choco[2]++;
            mini_HowMany = order.smudi_choco[2];
            order_button2(name, order.smudi_choco[2]);
        } //초콜렛스무디 라지

        private void button263_Click(object sender, EventArgs e)
        {
            string name = "모카스무디 라지";
            order.smudi_moca[2]++;
            mini_HowMany = order.smudi_moca[2];
            order_button2(name, order.smudi_moca[2]);
        } //모카스무디 라지
        #endregion

        private void order_button(int count, string date, string name, int sugar, int price, int sumprice, string whipping)
        {
            order.order_id++; //뭐지?

            dt.Rows.Add(name, count.ToString(), "50%", "None", date);
            eventClick_l(sumprice, dt, mini_HowMany);

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
