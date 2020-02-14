using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MetroFramework.Forms;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Data.Odbc;
using System.Data.SqlClient;

namespace ook_pos_program
{
    public partial class salesForm : MetroFramework.Forms.MetroForm
    {
        string strconn = string.Format(@"server=localhost;database=kiosk;user=root;password=1234");

        public salesForm()
        {
            InitializeComponent();
            chart1.Series.Clear();
            chart2.Series.Clear();
        }

        private void salesForm_Load(object sender, EventArgs e)
        {
            // 위쪽 텍스트 박스에 날짜 넣기
            string day1 = dateTimePicker1.Value.ToShortDateString();
            string day2 = dateTimePicker1.Value.AddDays(-1).ToShortDateString();
            string day3 = dateTimePicker1.Value.AddDays(-2).ToShortDateString();
            string day4 = dateTimePicker1.Value.AddDays(-3).ToShortDateString();
            string day5 = dateTimePicker1.Value.AddDays(-4).ToShortDateString();
            string day6 = dateTimePicker1.Value.AddDays(-5).ToShortDateString();
            string day7 = dateTimePicker1.Value.AddDays(-6).ToShortDateString();

            textBox1.Text = day1;
            textBox2.Text = day2;
            textBox3.Text = day3;
            textBox4.Text = day4;
            textBox5.Text = day5;
            textBox6.Text = day6;
            textBox7.Text = day7;
            textBox15.Text = "날짜"; textBox15.TextAlign = HorizontalAlignment.Center;
            textBox16.Text = "총액"; textBox16.TextAlign = HorizontalAlignment.Center;

            // 데이터값 읽어올 쿼리문


            string query = "select distinct o.currentDate,m.price from orderMenu o,menu m where o.currentDate IN (curdate()) and o.name=m.name;" +
                "select distinct o.currentDate,m.price as price from orderMenu o,menu m where o.currentDate IN (curdate() - interval 1 day) and o.name=m.name;" +
                "select distinct o.currentDate,m.price from orderMenu o,menu m where o.currentDate IN (curdate() - interval 2 day) and o.name=m.name;" +
                "select distinct o.currentDate,m.price from orderMenu o,menu m where o.currentDate IN (curdate() - interval 3 day) and o.name=m.name;" +
                "select distinct o.currentDate,m.price from orderMenu o,menu m where o.currentDate IN (curdate() - interval 4 day) and o.name=m.name;" +
                "select distinct o.currentDate,m.price from orderMenu o,menu m where o.currentDate IN (curdate() - interval 5 day) and o.name=m.name;" +
                "select distinct o.currentDate,m.price from orderMenu o,menu m where o.currentDate IN (curdate() - interval 6 day) and o.name=m.name";


            //차트 옵션
            Series series = chart1.Series.Add("총액");
            ChartArea myChartArea = new ChartArea("LineArea");

            series.ChartType = SeriesChartType.Line;
            series.LabelForeColor = Color.Red;
            series.MarkerColor = Color.Red;
            series.MarkerSize = 3;
            series.IsValueShownAsLabel = true;

            // 데이터베이스 연결
            MySqlConnection conn = new MySqlConnection(strconn);
            MySqlDataReader myReader;
            MySqlCommand command = new MySqlCommand(query, conn);

            conn.Open();

            myReader = command.ExecuteReader();

            Series series2 = chart2.Series.Add("날짜");
            series2.ChartType = SeriesChartType.Pie;
            //series2.IsValueShownAsLabel = false;

            int total = 0;
            int a = 0, b = 0, c = 0, d = 0, ee = 0, f = 0, g = 0, h = 0;

            try
            {
                if (myReader.Read())
                {
                    a = myReader.GetInt32("price");
                    total += a;
                    textBox8.Text = myReader.GetInt32("price").ToString();   // 14번 텍스트박스에 날짜에 맞게 값 넣기
                    chart1.Series["총액"].Points.AddXY(myReader["currentDate"], myReader["price"]);
                    chart2.Series["날짜"].Points.AddXY(myReader["currentDate"], myReader["price"]);
                }
                myReader.NextResult();
                if (myReader.Read())
                {
                    b = myReader.GetInt32("price");
                    total += b;
                    textBox9.Text = myReader.GetInt32("price").ToString();
                    chart1.Series["총액"].Points.AddXY(myReader["currentDate"], myReader["price"]);
                    chart2.Series["날짜"].Points.AddXY(myReader["currentDate"], myReader["price"]);
                }
                myReader.NextResult();
                if (myReader.Read())
                {
                    c = myReader.GetInt32("price");
                    total += c;
                    textBox10.Text = myReader.GetInt32("price").ToString();
                    chart1.Series["총액"].Points.AddXY(myReader["currentDate"], myReader["price"]);
                    chart2.Series["날짜"].Points.AddXY(myReader["currentDate"], myReader["price"]);
                }
                myReader.NextResult();
                if (myReader.Read())
                {
                    d = myReader.GetInt32("price");
                    total += d;
                    textBox11.Text = myReader.GetInt32("price").ToString();
                    chart1.Series["총액"].Points.AddXY(myReader["currentDate"], myReader["price"]);
                    chart2.Series["날짜"].Points.AddXY(myReader["currentDate"], myReader["price"]);

                }
                myReader.NextResult();
                if (myReader.Read())
                {
                    ee = myReader.GetInt32("price");
                    total += ee;
                    textBox12.Text = myReader.GetInt32("price").ToString();
                    chart1.Series["총액"].Points.AddXY(myReader["currentDate"], myReader["price"]);
                    chart2.Series["날짜"].Points.AddXY(myReader["currentDate"], myReader["price"]);
                    //chart2.Series.Add(a);
                }
                myReader.NextResult();
                if (myReader.Read())
                {
                    f = myReader.GetInt32("price");
                    total += f;
                    textBox13.Text = myReader.GetInt32("price").ToString();
                    chart1.Series["총액"].Points.AddXY(myReader["currentDate"], myReader["price"]);
                    chart2.Series["날짜"].Points.AddXY(myReader["currentDate"], myReader["price"]);
                }
                myReader.NextResult();
                if (myReader.Read())
                {
                    g = myReader.GetInt32("price");
                    total += g;
                    textBox14.Text = myReader.GetInt32("price").ToString();
                    chart1.Series["총액"].Points.AddXY(myReader["currentDate"], myReader["price"]);
                    chart2.Series["날짜"].Points.AddXY(myReader["currentDate"], myReader["price"]);
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show("안나옴;");
            }
            finally
            {
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saleForm1 newForm = new saleForm1();
            newForm.Show();
        } //월별 매출 현황

        private void button3_Click(object sender, EventArgs e)
        {
            saleForm3 newForm = new saleForm3();
            newForm.Show();
        } //요일별 매출 현황

        private void button4_Click(object sender, EventArgs e)
        {
            saleForm4 newForm = new saleForm4();
            newForm.Show();
        } //시간별 매출 현황

        private void button2_Click(object sender, EventArgs e)
        {
            saleForm2 newForm = new saleForm2();
            newForm.Show();
        } //상품별 매출 현황

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string day1 = dateTimePicker1.Value.ToShortDateString();
            string day2 = dateTimePicker1.Value.AddDays(-1).ToShortDateString();
            string day3 = dateTimePicker1.Value.AddDays(-2).ToShortDateString();
            string day4 = dateTimePicker1.Value.AddDays(-3).ToShortDateString();
            string day5 = dateTimePicker1.Value.AddDays(-4).ToShortDateString();
            string day6 = dateTimePicker1.Value.AddDays(-5).ToShortDateString();
            string day7 = dateTimePicker1.Value.AddDays(-6).ToShortDateString();


            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();
            textBox14.Clear();

            textBox1.Text = day1;
            textBox2.Text = day2;
            textBox3.Text = day3;
            textBox4.Text = day4;
            textBox5.Text = day5;
            textBox6.Text = day6;
            textBox7.Text = day7;

            chart1.Series.Clear();
            chart2.Series.Clear();

            Series series = chart1.Series.Add("총액");
            series.ChartType = SeriesChartType.Line;
            series.LabelForeColor = Color.Red;
            series.MarkerColor = Color.Red;
            series.MarkerSize = 3;
            series.IsValueShownAsLabel = true;

            string query = "select distinct o.currentDate,m.price from orderMenu o,menu m where o.currentDate IN ('#day1') and o.name=m.name;" +
                "select distinct o.currentDate,m.price as price from orderMenu o,menu m where o.currentDate IN ('#day2') and o.name=m.name;" +
                "select distinct o.currentDate,m.price as price from orderMenu o,menu m where o.currentDate IN ('#day3') and o.name=m.name;" +
                "select distinct o.currentDate,m.price as price from orderMenu o,menu m where o.currentDate IN ('#day4') and o.name=m.name;" +
                "select distinct o.currentDate,m.price as price from orderMenu o,menu m where o.currentDate IN ('#day5') and o.name=m.name;" +
                "select distinct o.currentDate,m.price as price from orderMenu o,menu m where o.currentDate IN ('#day6') and o.name=m.name;" +
                "select distinct o.currentDate,m.price as price from orderMenu o,menu m where o.currentDate IN ('#day7') and o.name=m.name";

            query = query.Replace("#day1", day1.ToString());
            query = query.Replace("#day2", day2.ToString());
            query = query.Replace("#day3", day3.ToString());
            query = query.Replace("#day4", day4.ToString());
            query = query.Replace("#day5", day5.ToString());
            query = query.Replace("#day6", day6.ToString());
            query = query.Replace("#day7", day7.ToString());

            MessageBox.Show(query);

            MySqlConnection conn = new MySqlConnection(strconn);
            MySqlDataReader myReader;
            MySqlCommand command = new MySqlCommand(query, conn);

            conn.Open();

            myReader = command.ExecuteReader();

            Series series2 = chart2.Series.Add("날짜");
            series2.ChartType = SeriesChartType.Pie;
            series2.IsValueShownAsLabel = false;

            try
            {
                if (myReader.Read())
                {
                    textBox8.Text = myReader.GetInt32("price").ToString();   // 14번 텍스트박스에 날짜에 맞게 값 넣기
                    chart1.Series["총액"].Points.AddXY(myReader["currentDate"], myReader["price"]);
                    chart2.Series["날짜"].Points.AddXY(myReader["currentDate"], myReader["price"]);
                }
                myReader.NextResult();
                if (myReader.Read())
                {
                    textBox9.Text = myReader.GetInt32("price").ToString();
                    chart1.Series["총액"].Points.AddXY(myReader["currentDate"], myReader["price"]);
                    chart2.Series["날짜"].Points.AddXY(myReader["currentDate"], myReader["price"]);
                }
                myReader.NextResult();
                if (myReader.Read())
                {
                    textBox10.Text = myReader.GetInt32("price").ToString();
                    chart1.Series["총액"].Points.AddXY(myReader["currentDate"], myReader["price"]);
                    chart2.Series["날짜"].Points.AddXY(myReader["currentDate"], myReader["price"]);
                }
                myReader.NextResult();
                if (myReader.Read())
                {
                    textBox11.Text = myReader.GetInt32("price").ToString();
                    chart1.Series["총액"].Points.AddXY(myReader["currentDate"], myReader["price"]);
                    chart2.Series["날짜"].Points.AddXY(myReader["currentDate"], myReader["price"]);

                }
                myReader.NextResult();
                if (myReader.Read())
                {
                    textBox12.Text = myReader.GetInt32("price").ToString();
                    chart1.Series["총액"].Points.AddXY(myReader["currentDate"], myReader["price"]);
                    chart2.Series["날짜"].Points.AddXY(myReader["currentDate"], myReader["price"]);
                    //chart2.Series.Add(a);
                }
                myReader.NextResult();
                if (myReader.Read())
                {
                    textBox13.Text = myReader.GetInt32("price").ToString();
                    chart1.Series["총액"].Points.AddXY(myReader["currentDate"], myReader["price"]);
                    chart2.Series["날짜"].Points.AddXY(myReader["currentDate"], myReader["price"]);
                }
                myReader.NextResult();
                if (myReader.Read())
                {
                    textBox14.Text = myReader.GetInt32("price").ToString();
                    chart1.Series["총액"].Points.AddXY(myReader["currentDate"], myReader["price"]);
                    chart2.Series["날짜"].Points.AddXY(myReader["currentDate"], myReader["price"]);
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show("안나옴;");
            }
            finally
            {
                conn.Close();
            }
        }

        private void button41_Click(object sender, EventArgs e)
        {
            string noLogin = "noLogin";
            this.Visible = false; //현재 폼 안보이게 하기
            Form2 frm = new Form2(noLogin);

            frm.Owner = this; //form2의 오너를 현재 폼으로
            frm.Show(); //form2 보여주기
        } //닫기 버튼

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
