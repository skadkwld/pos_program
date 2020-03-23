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
    public partial class saleForm4 : MetroFramework.Forms.MetroForm
    {
        public saleForm4()
        {
            InitializeComponent();
        }

        private void saleForm4_Load(object sender, EventArgs e)
        {
            string query =
                "select sum(m.price) as totalPrice from orderMenu o,menu m where o.currentDate like '___________09%' or currentDate like '___________10%' or currentDate like '___________11%';" +
                "select sum(m.price) as totalPrice from orderMenu o,menu m where o.currentDate like '___________12%' or currentDate like '___________13%' or currentDate like '___________14%';" +
                "select sum(m.price) as totalPrice from orderMenu o,menu m where o.currentDate like '___________15%' or currentDate like '___________16%' or currentDate like '___________17%';" +
                "select sum(m.price) as totalPrice from orderMenu o,menu m where o.currentDate like '___________18%' or currentDate like '___________19%' or currentDate like '___________20%';" +
                "select sum(m.price) as totalPrice from orderMenu o,menu m where o.currentDate like '___________21%' or currentDate like '___________22%' or currentDate like '___________23%'";

            //차트 옵션
            chart1.Series.Clear();
            Series series = chart1.Series.Add("시간별 총액");
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
            MySqlCommand command = new MySqlCommand(query, conn);

            conn.Open();

            myReader = command.ExecuteReader();

            try
            {
                while (myReader.Read())
                {
                    chart1.Series["시간별 총액"].Points.AddXY("09시~12시", myReader["totalPrice"]);
                }
                myReader.NextResult();
                while (myReader.Read())
                {
                    chart1.Series["시간별 총액"].Points.AddXY("12시~15시", myReader["totalPrice"]);
                }
                myReader.NextResult();
                while (myReader.Read())
                {
                    chart1.Series["시간별 총액"].Points.AddXY("15시~18시", myReader["totalPrice"]);
                }
                myReader.NextResult();
                while (myReader.Read())
                {
                    chart1.Series["시간별 총액"].Points.AddXY("18시~21시", myReader["totalPrice"]);
                }
                myReader.NextResult();
                while (myReader.Read())
                {
                    chart1.Series["시간별 총액"].Points.AddXY("21시~24시", myReader["totalPrice"]);
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
