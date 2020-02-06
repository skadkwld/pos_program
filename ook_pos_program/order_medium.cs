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

    public partial class order_medium : UserControl
    {
        public delegate int delCilck_m(int price, DataTable dt);
        public event delCilck_m eventClick_m;
        public order_medium()
        {
            InitializeComponent();
        }

        private void button95_Click(object sender, EventArgs e)
        {

        }
    }
}
