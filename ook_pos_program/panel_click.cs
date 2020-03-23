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
using System.Collections.Specialized;
using System.Net.Http;

namespace ook_pos_program
{
    public partial class panel_click : Form
    {
        //string strconn = string.Format(@"server=localhost;database=kiosk;user=root;password=1234");
        string strconn = string.Format(@"server=mydb.cmkvwrcpwpyc.ap-northeast-2.rds.amazonaws.com;database=ook;user=admin;password=young1700!");
        string[] _menuName=new string[10];
        string[] _price =  new string[10];
        string[] _num=new string[10];
        string _date;

        MySqlConnection conn;
        MySqlCommand cmd = new MySqlCommand();
        MySqlTransaction tr = null;

        public panel_click(string menuName,string price,string num,string date)
        {
            InitializeComponent();
            int i = 0,j = 0;
            _menuName = menuName.Split(',');
            _price = price.Split(',');
            _num = num.Split(',');
            //MessageBox.Show(_num[1]);
            _date = Convert.ToDateTime(date).ToString("yyyy-MM-dd HH:mm:ss");

            label1.Text = menuName;
            while (_menuName.Length-1>i)
            {
                listBox1.Items.Add(_menuName[i].ToString());
                i++;
            }

            //label2.Text = price.ToString();
            while (_menuName.Length - 1 > i)
            {
                listBox1.Items.Add(_price[i].ToString());
                i++;
            }
            label3.Text = num.ToString();
            while (_num.Length-1 > j)
            {
                listBox1.Items.Add(_num[j]).ToString();
                j++;
            }
            label4.Text = _date.ToString();

            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            int num = 78;
            string Num = num.ToString();
            //SqlCommand command;
            //SqlDataReader reader;
            conn = new MySqlConnection(strconn);

            conn.Open();

            tr = conn.BeginTransaction();

            string f = "F";
            string ing = "ING";
            string f_to_ing = "update orderMenu set confirmed="+"'ING' where currentDate='#Date'";
            string getToken = "select token from orderMenu where currentDate='#Date'";
            //string f_to_ing2 = "update orderMenu set confirmed="+"'ING'"+" where idorderMenu="+"76";
            string token = string.Empty;

            //f_to_ing.Replace("#confirmed", ing);


            //f_to_ing2.Replace("#confirmed", ing);
            //f_to_ing2.Replace("#i", Num);

            getToken = getToken.Replace("#Date", _date);

            cmd.Connection = conn;
            cmd.Transaction = tr;
            cmd.CommandText = f_to_ing.Replace("#Date", _date);

            tr.Commit();

            

            conn.Close();

            get_toket();
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
                {"token",token },{"Message","주문이 접수되었습니다."}
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
    }
}
