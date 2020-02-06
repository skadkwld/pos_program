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
    public partial class saleForm1 : MetroForm
    {
        public saleForm1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = string.Empty;
            string real_query = string.Empty;

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    for (int i = 1; i < 13; i++)
                    {
                        if (i < 10)
                        {
                            query = string.Format("select date_format(o.currentDate,'%Y-%m') as saleDate,sum(m.price) as totalPrice from orderMenu o,menu m where o.currentDate like '2019-0{0}%';", i);
                        }
                        else
                        {
                            query = string.Format("select date_format(o.currentDate,'%Y-%m') as saleDate,sum(m.price) as totalPrice from orderMenu o,menu m where o.currentDate like '2019-{0}%';", i);
                        }
                        real_query += query;
                        //query = string.Empty;
                    }
                    break;
                case 1:
                    for (int i = 1; i < 13; i++)
                    {
                        if (i < 10)
                        {
                            query = string.Format("select date_format(o.currentDate,'%Y-%m') as saleDate,sum(m.price) as totalPrice from orderMenu o,menu m where o.currentDate like '2020-0{0}%';", i);
                        }
                        else
                        {
                            query = string.Format("select date_format(o.currentDate,'%Y-%m') as saleDate,sum(m.price) as totalPrice from orderMenu o,menu m where o.currentDate like '2020-{0}%';", i);
                        }
                        real_query += query;
                        //query = string.Empty;
                    }
                    break;
                case 2:
                    for (int i = 1; i < 13; i++)
                    {
                        if (i < 10)
                        {
                            query = string.Format("select date_format(o.currentDate,'%Y-%m') as saleDate,sum(m.price) as totalPrice from orderMenu o,menu m where o.currentDate like '2021-0{0}%';", i);
                        }
                        else
                        {
                            query = string.Format("select date_format(o.currentDate,'%Y-%m') as saleDate,sum(m.price) as totalPrice from orderMenu o,menu m where o.currentDate like '2021-{0}%';", i);
                        }
                        real_query += query;
                        //query = string.Empty;
                    }
                    break;
            }
            chart1.Series.Clear();

            Series series = chart1.Series.Add("월별 총액");
            ChartArea myChartArea = new ChartArea("LineArea");
            chart1.ChartAreas.FirstOrDefault().AxisX.Interval = 1;

            series.LabelForeColor = Color.Red;
            series.MarkerColor = Color.Red;
            series.MarkerSize = 3;
            series.IsValueShownAsLabel = true;

            // 데이터베이스 연결
            string strconn = string.Format(@"server=localhost;database=kiosk;user=root;password=1234");
            MySqlConnection conn = new MySqlConnection(strconn);
            MySqlDataReader myReader;
            MySqlCommand command = new MySqlCommand(real_query, conn);

            conn.Open();

            myReader = command.ExecuteReader();

            try
            {
                for (int i = 0; i < 12; i++)
                {
                    while (myReader.Read())
                    {
                        chart1.Series["월별 총액"].Points.AddXY(myReader["saleDate"], myReader["totalPrice"]);
                    }
                    myReader.NextResult();
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

        private void saleForm1_Load(object sender, EventArgs e)
        {
            string[] date = { "2019년", "2020년", "2021년" };

            comboBox1.Items.AddRange(date);
            comboBox1.SelectedIndex = 1;
            string query = string.Empty;
            string real_query = string.Empty;

            //string query = "select date_format(o.currentDate,'%Y-%m') as saleDate,sum(m.price) as totalPrice from orderMenu o,menu m where o.currentDate like '2020-01%';" +
            //    "select date_format(o.currentDate,'%Y-%m') as saleDate,sum(m.price) as totalPrice from orderMenu o,menu m where o.currentDate like '2020-02%';" +
            //    "select date_format(o.currentDate,'%Y-%m') as saleDate,sum(m.price) as totalPrice from orderMenu o,menu m where o.currentDate like '2020-03%';" +
            //    "select date_format(o.currentDate,'%Y-%m') as saleDate,sum(m.price) as totalPrice from orderMenu o,menu m where o.currentDate like '2020-04%';" +
            //    "select date_format(o.currentDate,'%Y-%m') as saleDate,sum(m.price) as totalPrice from orderMenu o,menu m where o.currentDate like '2020-05%';" +
            //    "select date_format(o.currentDate,'%Y-%m') as saleDate,sum(m.price) as totalPrice from orderMenu o,menu m where o.currentDate like '2020-06%';" +
            //    "select date_format(o.currentDate,'%Y-%m') as saleDate,sum(m.price) as totalPrice from orderMenu o,menu m where o.currentDate like '2020-07%';" +
            //    "select date_format(o.currentDate,'%Y-%m') as saleDate,sum(m.price) as totalPrice from orderMenu o,menu m where o.currentDate like '2020-08%';" +
            //    "select date_format(o.currentDate,'%Y-%m') as saleDate,sum(m.price) as totalPrice from orderMenu o,menu m where o.currentDate like '2020-09%';" +
            //    "select date_format(o.currentDate,'%Y-%m') as saleDate,sum(m.price) as totalPrice from orderMenu o,menu m where o.currentDate like '2020-10%';" +
            //    "select date_format(o.currentDate,'%Y-%m') as saleDate,sum(m.price) as totalPrice from orderMenu o,menu m where o.currentDate like '2020-11%';" +
            //    "select date_format(o.currentDate,'%Y-%m') as saleDate,sum(m.price) as totalPrice from orderMenu o,menu m where o.currentDate like '2020-12%';";

            for (int i = 1; i < 13; i++)
            {
                if (i < 10)
                {
                    query = string.Format("select date_format(o.currentDate,'%Y-%m') as saleDate,sum(m.price) as totalPrice from orderMenu o,menu m where o.currentDate like '2020-0{0}%';", i);
                }
                else
                {
                    query = string.Format("select date_format(o.currentDate,'%Y-%m') as saleDate,sum(m.price) as totalPrice from orderMenu o,menu m where o.currentDate like '2020-{0}%';", i);
                }
                real_query += query;
                //query = string.Empty;
            }
            //MessageBox.Show(real_query);
            //차트 옵션
            chart1.Series.Clear();
            Series series = chart1.Series.Add("월별 총액");
            ChartArea myChartArea = new ChartArea("LineArea");
            chart1.ChartAreas.FirstOrDefault().AxisX.Interval = 1;


            series.LabelForeColor = Color.Red;
            series.MarkerColor = Color.Red;
            series.MarkerSize = 3;
            series.IsValueShownAsLabel = true;


            // 데이터베이스 연결
            string strconn = string.Format(@"server=localhost;database=kiosk;user=root;password=1234");
            MySqlConnection conn = new MySqlConnection(strconn);
            MySqlDataReader myReader;
            MySqlCommand command = new MySqlCommand(real_query, conn);

            conn.Open();

            myReader = command.ExecuteReader();

            try
            {
                for (int i = 0; i < 12; i++)
                {
                    while (myReader.Read())
                    {
                        chart1.Series["월별 총액"].Points.AddXY(myReader["saleDate"], myReader["totalPrice"]);
                    }
                    myReader.NextResult();
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
    }
}
