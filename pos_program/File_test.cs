using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ook_pos_program
{
    public partial class File_test : Form
    {
        string docName = "test.txt";
        string docPath= @"C:\Users\eoaud\source\repos\ook_pos_program\ook_pos_program\";
        string stringToPrint;

        public File_test()
        {
            InitializeComponent();
            
        }

        private void File_test_Load(object sender, EventArgs e)
        {
            ReadFile();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //int charactersOnPage = 0;
            //int linesPerPage = 0;

            //// Sets the value of charactersOnPage to the number of characters 
            //// of stringToPrint that will fit within the bounds of the page.
            //e.Graphics.MeasureString(stringToPrint, this.Font,
            //    e.MarginBounds.Size, StringFormat.GenericTypographic,
            //    out charactersOnPage, out linesPerPage);

            //// Draws the string within the bounds of the page
            //e.Graphics.DrawString(stringToPrint, this.Font, Brushes.Black,
            //    e.MarginBounds, StringFormat.GenericTypographic);

            //// Remove the portion of the string that has been printed.
            //stringToPrint = stringToPrint.Substring(charactersOnPage);

            //// Check to see if more pages are to be printed.
            //e.HasMorePages = (stringToPrint.Length > 0);

            Graphics g = e.Graphics;

            PointF drawPoint = new PointF(150.0F, 150.0F);

            using(Font font=new Font("Lucica Console",30))
            using(SolidBrush drawBrush=new SolidBrush(Color.Black))
            {
                //g.DrawString()
            }

        }

        public void ReadFile()
        {
            //printPreviewControl1.Document = printDocument1;
            //printDocument1.DocumentName = docName;
            //using (FileStream stream = new FileStream(docPath + docName, FileMode.Open))
            //using (StreamReader reader = new StreamReader(stream))
            //{
            //    stringToPrint = reader.ReadToEnd();
            //}
        }
    }
}
