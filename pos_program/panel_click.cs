using System;
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
using System.Net;
using MetroFramework.Forms;

using System.Collections.Specialized;
using System.Net.Http;

namespace ook_pos_program
{
    public partial class panel_click : MetroFramework.Forms.MetroForm
    {
        //string strconn = string.Format(@"server=localhost;database=kiosk;user=root;password=1234");
        string strconn = string.Format(@"server=mydb.cmkvwrcpwpyc.ap-northeast-2.rds.amazonaws.com;database=ook;user=admin;password=young1700!");
        string[] _menuName=new string[10];
        string[] _price =  new string[10];
        string[] _num=new string[10];
        string _date;
        string _tempDate;

        MySqlConnection conn;
        MySqlCommand cmd;
        MySqlTransaction tr = null;

        public panel_click(string menuName,string price,string num,string date)
        {
            
            InitializeComponent();
            int temp_price = 0, temp_num = 0; //string을 int로 바꿔서 곱셈할 용도의 변수들
            int i = 0,j = 0;
            int sum = 0;
            _menuName = menuName.Split(',');
            _price = price.Split(',');
            _num = num.Split(',');
            //MessageBox.Show(_num[1]);
            _date = Convert.ToDateTime(date).ToString("yyyy-MM-dd HH:mm:ss");

            label1.Text = menuName;
            while (_menuName.Length-1>i)
            {
                temp_num = Convert.ToInt32(_num[i]);
                temp_price = Convert.ToInt32(_price[i]);
                switch (i)
                {
                    case 0:
                        menuLabel1.Visible = true;
                        numLabel1.Visible = true;
                        priceLabel1.Visible = true;
                        menuLabel1.Text = _menuName[i].ToString();
                        numLabel1.Text = _num[i].ToString();
                        priceLabel1.Text = (temp_price*temp_num).ToString();
                        sum += Convert.ToInt32(priceLabel1.Text);
                        break;
                    case 1:
                        menuLabel2.Visible = true;
                        numLabel2.Visible = true;
                        priceLabel2.Visible = true;
                        menuLabel2.Text = _menuName[i].ToString();
                        numLabel2.Text = _num[i].ToString();
                        priceLabel2.Text = (temp_price * temp_num).ToString();
                        sum += Convert.ToInt32(priceLabel2.Text);
                        break;
                    case 2:
                        menuLabel3.Visible = true;
                        numLabel3.Visible = true;
                        priceLabel3.Visible = true;
                        menuLabel3.Text = _menuName[i].ToString();
                        numLabel3.Text = _num[i].ToString();
                        priceLabel3.Text = (temp_price * temp_num).ToString();
                        sum += Convert.ToInt32(priceLabel3.Text);
                        break;
                    case 3:
                        menuLabel4.Visible = true;
                        numLabel4.Visible = true;
                        priceLabel4.Visible = true;
                        menuLabel4.Text = _menuName[i].ToString();
                        numLabel4.Text = _num[i].ToString();
                        priceLabel4.Text = (temp_price * temp_num).ToString();
                        sum += Convert.ToInt32(priceLabel4.Text);
                        break;
                }
                label10.Text = sum.ToString()+"원";
                listBox1.Items.Add(_menuName[i].ToString());
                listBox1.Items.Add(_price[i].ToString());
                listBox1.Items.Add(_num[i]).ToString();
                i++;
            } //리스트박스에 메뉴이름과 가격, 개수를 입력한다

            //label2.Text = price.ToString();
           
            label3.Text = num.ToString();
            //while (_num.Length-1 > j)
            //{
            //    listBox1.Items.Add(_num[j]).ToString();
            //    j++;
            //}
            label4.Text = _date.ToString();
            listBox1.Items.Add(_date.ToString());

            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            int num = 78;
            string Num = num.ToString();
            DateTime dt = Convert.ToDateTime(_date);
            conn = new MySqlConnection(strconn);

            conn.Open();

            //tr = conn.BeginTransaction();

            string f = "F";
            string ing = "ING";
            string f_to_ing = "update orderMenu set confirmed="+"'ING' where currentDate='#Date'";
            //string f_to_ing2 = "update orderMenu set confirmed="+"'ING'"+" where idorderMenu="+"76";
            string token = string.Empty;

            f_to_ing = f_to_ing.Replace("#Date", _date);

            cmd = new MySqlCommand(f_to_ing, conn);
            cmd.ExecuteNonQuery();

           // tr.Commit();

            

            conn.Close();

            get_toket(); //로컬에는 없어서 로컬로 테스트할때는 주석처리
            //sendRequest();

        } //주문접수 버튼

        private void button2_Click(object sender, EventArgs e)
        {

        } //주문거부 버튼

        public async Task sendRequestAsync(string token)
        {
            string url = "http://ec2-13-124-57-226.ap-northeast-2.compute.amazonaws.com/pushNotification/pushNotification.php";
            HttpClient client = new HttpClient();
            
            Dictionary<string, string>values = new Dictionary<string, string>
            {
                {"token",token },{"Message","주문이 접수되었습니다"}
            };
            FormUrlEncodedContent content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();

        }

        public void get_toket()
        {
            string token = string.Empty;
            string sql =
        "select token from orderMenu where currentDate = '#Date'";
            sql = sql.Replace("#Date", _date);
            cmd = new MySqlCommand(sql, conn);
                try
                {
                    conn.Open();
                    token = (string)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            //MessageBox.Show(token);
            sendRequestAsync(token);
            conn.Close();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
    }
}
