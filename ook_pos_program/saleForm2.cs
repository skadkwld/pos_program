using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class saleForm2 : MetroFramework.Forms.MetroForm
    {
        string strconn = string.Format(@"server=localhost;database=kiosk;user=root;password=1234");

        public saleForm2()
        {
            InitializeComponent();
        }

        private void saleForm2_Load(object sender, EventArgs e)
        {
            string[] date = { "콜드 브루", "커피", "블렌디드", "스타벅스 피지오", "프라푸치노", "초콜릿", "스무디" };

            comboBox1.Items.AddRange(date);
            comboBox1.SelectedIndex = 1;

            string query =
                "select sum(m.price) as totalPrice from orderMenu o,menu m where o.name like '콜드 폼 콜드브루%';" +
                "select sum(m.price) as totalPrice from orderMenu o,menu m where o.name like '바닐라크림 콜드브루%';" +
                "select sum(m.price) as totalPrice from orderMenu o,menu m where o.name like '콜드브루%';";

            init();

            // 데이터베이스 연결
            
            MySqlConnection conn = new MySqlConnection(strconn);
            MySqlDataReader myReader;
            MySqlCommand command = new MySqlCommand(query, conn);

            conn.Open();

            myReader = command.ExecuteReader();

            try
            {
                if(myReader.Read())
                {
                    chart1.Series["상품별 총액"].Points.AddXY("콜드 폼 콜드브루", myReader["totalPrice"]);
                }
                myReader.NextResult();

                if (myReader.Read())
                {
                    chart1.Series["상품별 총액"].Points.AddXY("바닐라크림 콜드브루", myReader["totalPrice"]);
                }
                myReader.NextResult();

                if (myReader.Read())
                {
                    chart1.Series["상품별 총액"].Points.AddXY("콜드브루", myReader["totalPrice"]);
                }
                myReader.NextResult();
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = string.Empty;
            MySqlConnection conn;
            MySqlDataReader myReader;
            MySqlCommand command;

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    query =
                "select sum(m.price) as totalPrice from orderMenu o,menu m where o.name like '콜드 폼 콜드브루%';" +
                "select sum(m.price) as totalPrice from orderMenu o,menu m where o.name like '바닐라크림 콜드브루%';" +
                "select sum(m.price) as totalPrice from orderMenu o,menu m where o.name like '콜드브루%';";
                    init();
                    conn = new MySqlConnection(strconn);
                    command = new MySqlCommand(query, conn);

                    conn.Open();

                    myReader = command.ExecuteReader();

                    try
                    {
                        if (myReader.Read())
                        {
                            chart1.Series["상품별 총액"].Points.AddXY("콜드 폼 콜드브루", myReader["totalPrice"]);
                        }
                        myReader.NextResult();

                        if (myReader.Read())
                        {
                            chart1.Series["상품별 총액"].Points.AddXY("바닐라크림 콜드브루", myReader["totalPrice"]);
                        }
                        myReader.NextResult();

                        if (myReader.Read())
                        {
                            chart1.Series["상품별 총액"].Points.AddXY("콜드브루", myReader["totalPrice"]);
                        }
                        myReader.NextResult();
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("안나옴;");
                    }
                    finally
                    {
                        conn.Close();
                    }
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    query =
                "select sum(m.price) as totalPrice from orderMenu o,menu m where o.name like '타로스무디%';" +
                "select sum(m.price) as totalPrice from orderMenu o,menu m where o.name like '망고스무디%';" +
                "select sum(m.price) as totalPrice from orderMenu o,menu m where o.name like '초콜렛스무디%';" +
                "select sum(m.price) as totalPrice from orderMenu o,menu m where o.name like '모카스무디%';";
                    init();
                    conn = new MySqlConnection(strconn);
                    command = new MySqlCommand(query, conn);

                    conn.Open();

                    myReader = command.ExecuteReader();

                    try
                    {
                        if (myReader.Read())
                        {
                            chart1.Series["상품별 총액"].Points.AddXY("타로 스무디", myReader["totalPrice"]);
                        }
                        myReader.NextResult();

                        if (myReader.Read())
                        {
                            chart1.Series["상품별 총액"].Points.AddXY("망고 스무디", myReader["totalPrice"]);
                        }
                        myReader.NextResult();

                        if (myReader.Read())
                        {
                            chart1.Series["상품별 총액"].Points.AddXY("초콜렛 스무디", myReader["totalPrice"]);
                        }
                        myReader.NextResult();

                        if (myReader.Read())
                        {
                            chart1.Series["상품별 총액"].Points.AddXY("모카 스무디", myReader["totalPrice"]);
                        }
                        myReader.NextResult();
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show("안나옴;");
                    }
                    finally
                    {
                        conn.Close();
                    }
                    break;
            }
        }

        public void init()
        {
            chart1.Series.Clear();
            Series series = chart1.Series.Add("상품별 총액");
            ChartArea myChartArea = new ChartArea("LineArea");
            chart1.ChartAreas.FirstOrDefault().AxisX.Interval = 1;

            series.LabelForeColor = Color.Red;
            series.MarkerColor = Color.Red;
            series.MarkerSize = 3;
            series.IsValueShownAsLabel = true;
        }
    }
}
