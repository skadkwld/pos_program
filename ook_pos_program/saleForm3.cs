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
    public partial class saleForm3 : MetroFramework.Forms.MetroForm
    {
        public saleForm3()
        {
            InitializeComponent();
        }

        private void saleForm3_Load(object sender, EventArgs e)
        {
            string query =
               "select sum(m.price) as totalPrice from orderMenu o,menu m where weekday(o.currentDate) like 0;" +
               "select sum(m.price) as totalPrice from orderMenu o,menu m where weekday(o.currentDate) like 1;" +
               "select sum(m.price) as totalPrice from orderMenu o,menu m where weekday(o.currentDate) like 2;" +
               "select sum(m.price) as totalPrice from orderMenu o,menu m where weekday(o.currentDate) like 3;" +
               "select sum(m.price) as totalPrice from orderMenu o,menu m where weekday(o.currentDate) like 4;" +
               "select sum(m.price) as totalPrice from orderMenu o,menu m where weekday(o.currentDate) like 5;" +
               "select sum(m.price) as totalPrice from orderMenu o,menu m where weekday(o.currentDate) like 6;";

            //차트 옵션
            chart1.Series.Clear();
            Series series = chart1.Series.Add("요일별 총액");
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
                    chart1.Series["요일별 총액"].Points.AddXY("월", myReader["totalPrice"]);
                }
                myReader.NextResult();
                while (myReader.Read())
                {
                    chart1.Series["요일별 총액"].Points.AddXY("화", myReader["totalPrice"]);
                }
                myReader.NextResult();
                while (myReader.Read())
                {
                    chart1.Series["요일별 총액"].Points.AddXY("수", myReader["totalPrice"]);
                }
                myReader.NextResult();
                while (myReader.Read())
                {
                    chart1.Series["요일별 총액"].Points.AddXY("목", myReader["totalPrice"]);
                }
                myReader.NextResult();
                while (myReader.Read())
                {
                    chart1.Series["요일별 총액"].Points.AddXY("금", myReader["totalPrice"]);
                }
                myReader.NextResult();
                while (myReader.Read())
                {
                    chart1.Series["요일별 총액"].Points.AddXY("토", myReader["totalPrice"]);
                }
                myReader.NextResult();
                while (myReader.Read())
                {
                    chart1.Series["요일별 총액"].Points.AddXY("일", myReader["totalPrice"]);
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

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
