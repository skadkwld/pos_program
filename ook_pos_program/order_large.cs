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
    public partial class order_large : UserControl
    {
        public delegate int delCilck_l(int price, DataTable dt);
        public event delCilck_l eventClick_l;
        public order_large()
        {
            InitializeComponent();
        }

        private void button229_Click(object sender, EventArgs e)
        {

        }
    }
}
