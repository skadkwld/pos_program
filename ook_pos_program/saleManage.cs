using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MetroFramework.Forms;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Windows;
using System.Globalization;

namespace Method
{
    public partial class saleManage : MetroForm
    {
        int temp = 0;
        int price1 = 0;
        int price2 = 0;
        int price3 = 0;
        int price4 = 0;
        int price5 = 0;
        int price6 = 0;
        int price7 = 0;
        int price8 = 0;
        order order = new order();
        public saleManage()
        {
            InitializeComponent();
            groupBox2.Hide();
            groupBox4.Hide();
            groupBox5.Hide();
            button3.BackColor = SystemColors.ActiveCaption;
        }

        private void saleManage_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
            label10.Text = "";
            label11.Text = "";
            label12.Text = "";
            label13.Text = "";
            label14.Text = "";
            label15.Text = "";
            label16.Text = "";
            label17.Text = "";
            label35.Text = "";
            label36.Text = "";
            label37.Text = "";
            label38.Text = "";
            label39.Text = "";
            label40.Text = "";
            label41.Text = "";
            label42.Text = "";
            label20.Text = "0";
            String strUrl = "http://www.kma.go.kr/weather/forecast/mid-term-xml.jsp";

            UriBuilder ub = new UriBuilder(strUrl);
            ub.Query = "srnLd=109";

            HttpWebRequest request;
            request = HttpWebRequest.Create(ub.Uri) as HttpWebRequest;
            request.BeginGetResponse(new AsyncCallback(GetResponse), request);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text))
                label10.Text = "";
            else
            {
                string a = textBox1.Text;
                int aa = Int32.Parse(a);
                int aaa = aa * 50000;

                string b = label20.Text;
                int bb = Int32.Parse(b);

                label10.Text = aaa.ToString();

                price1 = aaa;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox2.Text))
                label11.Text = "";
            else
            {
                string a = textBox2.Text;
                int aa = Int32.Parse(a);
                int aaa = aa * 10000;
                label11.Text = aaa.ToString();

                string b = label20.Text;
                int bb = Int32.Parse(b);

                bb = aaa;
                price2 = aaa;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox3.Text))
                label12.Text = "";
            else
            {
                string a = textBox3.Text;
                int aa = Int32.Parse(a);
                int aaa = aa * 5000;
                label12.Text = aaa.ToString();

                string b = label20.Text;
                int bb = Int32.Parse(b);

                bb = aaa;
                price3 = aaa;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox4.Text))
                label13.Text = "";
            else
            {
                string a = textBox4.Text;
                int aa = Int32.Parse(a);
                int aaa = aa * 1000;
                label13.Text = aaa.ToString();

                string b = label20.Text;
                int bb = Int32.Parse(b);

                bb = aaa;
                price4 = aaa;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox5.Text))
                label14.Text = "";
            else
            {
                string a = textBox5.Text;
                int aa = Int32.Parse(a);
                int aaa = aa * 500;
                label14.Text = aaa.ToString();

                string b = label20.Text;
                int bb = Int32.Parse(b);

                bb = aaa;
                price5 = aaa;
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox6.Text))
                label15.Text = "";
            else
            {
                string a = textBox6.Text;
                int aa = Int32.Parse(a);
                int aaa = aa * 100;
                label15.Text = aaa.ToString();

                string b = label20.Text;
                int bb = Int32.Parse(b);

                bb = aaa;
                price6 = aaa;
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox7.Text))
                label16.Text = "";
            else
            {
                string a = textBox7.Text;
                int aa = Int32.Parse(a);
                int aaa = aa * 50;
                label16.Text = aaa.ToString();

                string b = label20.Text;
                int bb = Int32.Parse(b);

                bb = aaa;
                price7 = aaa;
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox8.Text))
                label17.Text = "";
            else
            {
                string a = textBox8.Text;
                int aa = Int32.Parse(a);
                int aaa = aa * 10;
                label17.Text = aaa.ToString();

                string b = label20.Text;
                int bb = Int32.Parse(b);

                bb = aaa;
                price8 = aaa;
            }
        }

        private void button_0_Click(object sender, EventArgs e)
        {
            if (temp == 0)
                textBox1.Text += "0";
            if (temp == 1)
                textBox2.Text += "0";
            if (temp == 2)
                textBox3.Text += "0";
            if (temp == 3)
                textBox4.Text += "0";
            if (temp == 4)
                textBox5.Text += "0";
            if (temp == 5)
                textBox6.Text += "0";
            if (temp == 6)
                textBox7.Text += "0";
            if (temp == 7)
                textBox8.Text += "0";
            if (temp == 8)
                textBox23.Text += "0";
        }

        private void button_00_Click(object sender, EventArgs e)
        {
            if (temp == 0)
                textBox1.Text += "00";
            if (temp == 1)
                textBox2.Text += "00";
            if (temp == 2)
                textBox3.Text += "00";
            if (temp == 3)
                textBox4.Text += "00";
            if (temp == 4)
                textBox5.Text += "00";
            if (temp == 5)
                textBox6.Text += "00";
            if (temp == 6)
                textBox7.Text += "00";
            if (temp == 7)
                textBox8.Text += "00";
            if (temp == 8)
                textBox23.Text += "00";
        }

        private void button_1_Click(object sender, EventArgs e)
        {
            if (temp == 0)
                textBox1.Text += "1";
            if (temp == 1)
                textBox2.Text += "1";
            if (temp == 2)
                textBox3.Text += "1";
            if (temp == 3)
                textBox4.Text += "1";
            if (temp == 4)
                textBox5.Text += "1";
            if (temp == 5)
                textBox6.Text += "1";
            if (temp == 6)
                textBox7.Text += "1";
            if (temp == 7)
                textBox8.Text += "1";
            if (temp == 8)
                textBox23.Text += "1";
        }

        private void button_2_Click(object sender, EventArgs e)
        {
            if (temp == 0)
                textBox1.Text += "2";
            if (temp == 1)
                textBox2.Text += "2";
            if (temp == 2)
                textBox3.Text += "2";
            if (temp == 3)
                textBox4.Text += "2";
            if (temp == 4)
                textBox5.Text += "2";
            if (temp == 5)
                textBox6.Text += "2";
            if (temp == 6)
                textBox7.Text += "2";
            if (temp == 7)
                textBox8.Text += "2";
            if (temp == 8)
                textBox23.Text += "2";
        }

        private void button_3_Click(object sender, EventArgs e)
        {
            if (temp == 0)
                textBox1.Text += "3";
            if (temp == 1)
                textBox2.Text += "3";
            if (temp == 2)
                textBox3.Text += "3";
            if (temp == 3)
                textBox4.Text += "3";
            if (temp == 4)
                textBox5.Text += "3";
            if (temp == 5)
                textBox6.Text += "3";
            if (temp == 6)
                textBox7.Text += "3";
            if (temp == 7)
                textBox8.Text += "3";
            if (temp == 8)
                textBox23.Text += "3";
        }

        private void button_4_Click(object sender, EventArgs e)
        {
            if (temp == 0)
                textBox1.Text += "4";
            if (temp == 1)
                textBox2.Text += "4";
            if (temp == 2)
                textBox3.Text += "4";
            if (temp == 3)
                textBox4.Text += "4";
            if (temp == 4)
                textBox5.Text += "4";
            if (temp == 5)
                textBox6.Text += "4";
            if (temp == 6)
                textBox7.Text += "4";
            if (temp == 7)
                textBox8.Text += "4";
            if (temp == 8)
                textBox23.Text += "4";
        }

        private void button_5_Click(object sender, EventArgs e)
        {
            if (temp == 0)
                textBox1.Text += "5";
            if (temp == 1)
                textBox2.Text += "5";
            if (temp == 2)
                textBox3.Text += "5";
            if (temp == 3)
                textBox4.Text += "5";
            if (temp == 4)
                textBox5.Text += "5";
            if (temp == 5)
                textBox6.Text += "5";
            if (temp == 6)
                textBox7.Text += "5";
            if (temp == 7)
                textBox8.Text += "5";
            if (temp == 8)
                textBox23.Text += "5";
        }

        private void button_6_Click(object sender, EventArgs e)
        {
            if (temp == 0)
                textBox1.Text += "6";
            if (temp == 1)
                textBox2.Text += "6";
            if (temp == 2)
                textBox3.Text += "6";
            if (temp == 3)
                textBox4.Text += "6";
            if (temp == 4)
                textBox5.Text += "6";
            if (temp == 5)
                textBox6.Text += "6";
            if (temp == 6)
                textBox7.Text += "6";
            if (temp == 7)
                textBox8.Text += "6";
            if (temp == 8)
                textBox23.Text += "6";
        }

        private void button_7_Click(object sender, EventArgs e)
        {
            if (temp == 0)
                textBox1.Text += "7";
            if (temp == 1)
                textBox2.Text += "7";
            if (temp == 2)
                textBox3.Text += "7";
            if (temp == 3)
                textBox4.Text += "7";
            if (temp == 4)
                textBox5.Text += "7";
            if (temp == 5)
                textBox6.Text += "7";
            if (temp == 6)
                textBox7.Text += "7";
            if (temp == 7)
                textBox8.Text += "7";
            if (temp == 8)
                textBox23.Text += "7";
        }

        private void button_8_Click(object sender, EventArgs e)
        {
            if (temp == 0)
                textBox1.Text += "8";
            if (temp == 1)
                textBox2.Text += "8";
            if (temp == 2)
                textBox3.Text += "8";
            if (temp == 3)
                textBox4.Text += "8";
            if (temp == 4)
                textBox5.Text += "8";
            if (temp == 5)
                textBox6.Text += "8";
            if (temp == 6)
                textBox7.Text += "8";
            if (temp == 7)
                textBox8.Text += "8";
            if (temp == 8)
                textBox23.Text += "8";
        }

        private void button_9_Click(object sender, EventArgs e)
        {
            if (temp == 0)
                textBox1.Text += "9";
            if (temp == 1)
                textBox2.Text += "9";
            if (temp == 2)
                textBox3.Text += "9";
            if (temp == 3)
                textBox4.Text += "9";
            if (temp == 4)
                textBox5.Text += "9";
            if (temp == 5)
                textBox6.Text += "9";
            if (temp == 6)
                textBox7.Text += "9";
            if (temp == 7)
                textBox8.Text += "9";
            if (temp == 8)
                textBox23.Text += "9";
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            if (temp == 0)
                textBox1.Text = textBox1.Text.Remove(textBox1.TextLength - 1);
            if (temp == 1)
                textBox2.Text = textBox2.Text.Remove(textBox2.TextLength - 1);
            if (temp == 2)
                textBox3.Text = textBox3.Text.Remove(textBox3.TextLength - 1);
            if (temp == 3)
                textBox4.Text = textBox4.Text.Remove(textBox4.TextLength - 1);
            if (temp == 4)
                textBox5.Text = textBox5.Text.Remove(textBox5.TextLength - 1);
            if (temp == 5)
                textBox6.Text = textBox6.Text.Remove(textBox6.TextLength - 1);
            if (temp == 6)
                textBox7.Text = textBox7.Text.Remove(textBox7.TextLength - 1);
            if (temp == 7)
                textBox8.Text = textBox8.Text.Remove(textBox8.TextLength - 1);
            if (temp == 8)
                textBox23.Text = textBox8.Text.Remove(textBox8.TextLength - 1);
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            temp = 0;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            temp = 1;
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            temp = 2;
        }

        private void textBox4_Click(object sender, EventArgs e)
        {
            temp = 3;
        }

        private void textBox5_Click(object sender, EventArgs e)
        {
            temp = 4;

        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            temp = 5;

        }

        private void textBox7_Click(object sender, EventArgs e)
        {
            temp = 6;

        }

        private void textBox8_Click(object sender, EventArgs e)
        {
            temp = 7;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            int total;
            total = price1 + price2 + price3 + price4 + price5 + price6 + price7 + price8;
            
            label20.Text = total.ToString();
            label56.Text = total.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.BackColor = SystemColors.ActiveCaption;
            button1.BackColor = SystemColors.Window;
            button2.BackColor = SystemColors.Window;
            //button4.BackColor = SystemColors.Window;
            //button5.BackColor = SystemColors.Window;
            button6.BackColor = SystemColors.Window;
            groupBox1.Show();
            groupBox2.Hide();
            groupBox4.Hide();
            groupBox5.Hide();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox7.Clear();
            textBox8.Clear();
            label20.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button1.BackColor = SystemColors.Window;
            button2.BackColor = SystemColors.Window;
            button3.BackColor = SystemColors.Window;
            //button4.BackColor = SystemColors.Window;
            //button5.BackColor = SystemColors.Window;
            button6.BackColor = SystemColors.ActiveCaption;
            groupBox1.Hide();
            groupBox2.Show();
            groupBox4.Hide();
            groupBox5.Hide();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox9.Text))
                label35.Text = "";
            else
            {
                string a = textBox9.Text;
                int aa = Int32.Parse(a);
                int aaa = aa * 50000;

                label35.Text = aaa.ToString();
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox10.Text))
                label36.Text = "";
            else
            {
                string a = textBox10.Text;
                int aa = Int32.Parse(a);
                int aaa = aa * 10000;

                label36.Text = aaa.ToString();
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox11.Text))
                label37.Text = "";
            else
            {
                string a = textBox11.Text;
                int aa = Int32.Parse(a);
                int aaa = aa * 5000;

                label37.Text = aaa.ToString();
            }
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox12.Text))
                label38.Text = "";
            else
            {
                string a = textBox12.Text;
                int aa = Int32.Parse(a);
                int aaa = aa * 1000;

                label38.Text = aaa.ToString();
            }
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox13.Text))
                label39.Text = "";
            else
            {
                string a = textBox13.Text;
                int aa = Int32.Parse(a);
                int aaa = aa * 500;

                label39.Text = aaa.ToString();
            }
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox14.Text))
                label40.Text = "";
            else
            {
                string a = textBox14.Text;
                int aa = Int32.Parse(a);
                int aaa = aa * 100;

                label40.Text = aaa.ToString();
            }
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox15.Text))
                label41.Text = "";
            else
            {
                string a = textBox15.Text;
                int aa = Int32.Parse(a);
                int aaa = aa * 50;

                label41.Text = aaa.ToString();
            }
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBox16.Text))
                label42.Text = "";
            else
            {
                string a = textBox16.Text;
                int aa = Int32.Parse(a);
                int aaa = aa * 10;

                label42.Text = aaa.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.BackColor = SystemColors.ActiveCaption;
            button2.BackColor = SystemColors.Window;
            button3.BackColor = SystemColors.Window;
            //button4.BackColor = SystemColors.Window;
            //button5.BackColor = SystemColors.Window;
            button6.BackColor = SystemColors.Window;
            groupBox1.Hide();
            groupBox2.Hide();
            groupBox5.Hide();
            groupBox4.Show();
            
        }

        private void GetResponse(IAsyncResult ar)
        {
            HttpWebRequest wr = (HttpWebRequest)ar.AsyncState;
            HttpWebResponse wp = (HttpWebResponse)wr.EndGetResponse(ar);

            Stream stream = wp.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            String strRead = reader.ReadToEnd();

            XElement xmlMain = XElement.Parse(strRead);
            XElement xmlHead = xmlMain.Descendants("header").First();

            String strTitle = xmlHead.Element("title").Value;
            String strDate = xmlHead.Element("tm").Value;
            String strDesc = xmlHead.Element("wf").Value;
            //strDesc = strDesc.Replace("</br></br>", "\n");
            strDesc = strDesc.Replace("<br />", "\n");
            String strTemp = strTitle + "\n" + strDate + "\n" + strDesc + "\n";


            //String strContent = xmlLocation.Element("data").Value;

            //String strTemp = strContent + "\n";

            this.Invoke(new Action(() =>
            { label68.Text = strTemp; }
        ));
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("등록되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("추가되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            label69.Text = textBox19.Text;
            textBox19.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.BackColor = SystemColors.ActiveCaption;
            button1.BackColor = SystemColors.Window;
            button3.BackColor = SystemColors.Window;
            //button4.BackColor = SystemColors.Window;
            //button5.BackColor = SystemColors.Window;
            button6.BackColor = SystemColors.Window;
            groupBox5.Show();
            groupBox1.Hide();
            groupBox2.Hide();
            groupBox4.Hide();
            CultureInfo cultures = CultureInfo.CreateSpecificCulture("ko-KR");

            textBox20.Text = DateTime.Now.ToString(string.Format("yyyy-MM-dd (ddd)", cultures));
            textBox21.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            textBox22.Text = "0000 - 관리자";
            
            
            //string time = DateTime.Now.ToString("yyyy년 MM월 dd일 HH시 mm분 ss초\nPOS 로그인 확인");
        }

        private void button10_Click(object sender, EventArgs e)
        {

            
            if (textBox23.Text.Equals("1234"))
            {
                DialogResult dr = MessageBox.Show("마감된 영업일자를 취소하시겠습니까?", "알림", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    MessageBox.Show("마감취소 하였습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox20.Clear();
                    textBox21.Clear();
                    textBox22.Clear();
                    textBox23.Clear();
                }
            }
            else
            {
                MessageBox.Show("비밀번호를 잘못 입력하셨습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            /*
            if (textBox23.Text.Equals("1234"))
            {
                
                MessageBox.Show("마감된 영업일자를 취소하시겠습니까?");
                
            }
            else
            {
                
                MessageBox.Show("비밀번호를 잘못 입력하셨습니다");

            }
            */
        }

        private void textBox23_Click(object sender, EventArgs e)
        {
            temp = 8;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("마감 하시겠습니까?", "알림", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                MessageBox.Show("마감 하였습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Visible = false;

                Form2 form = new Form2(1);
                form.Owner = this;
                form.Show();
            }
        }
    }
}
