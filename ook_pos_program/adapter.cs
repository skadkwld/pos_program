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

namespace ook_pos_program
{

    class adapter
    {
        //string strconn = string.Format(@"server=mydb.cmkvwrcpwpyc.ap-northeast-2.rds.amazonaws.com;database=ook;user=admin;password=young1700!");
        string strconn = string.Format(@"server=localhost;database=kiosk;user=root;password=1234");

        public DataTable ConnectDB()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(strconn);
                conn.Open();
                string query = @"select * from orderMenu";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                MySqlCommandBuilder builder = new MySqlCommandBuilder(adapter);
                DataTable dt = new DataTable();
                adapter.Fill(dt);


                conn.Close();
                return dt;
            }
            catch (Exception ex)
            {
                DataTable dt2 = new DataTable();
                MessageBox.Show(ex.Message);
                return dt2;
            }
        }
    }
}
