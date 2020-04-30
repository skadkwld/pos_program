using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Media;

namespace ook_pos_program
{
    public partial class Form2 : MetroForm
    {
        SoundPlayer sp = new SoundPlayer();
        SoundPlayer error = new SoundPlayer();
        string file = (string)@"C:\버튼.wav";
        string error_file = (string)@"C:\error.wav";
        string pwd = "";
        int index = -1;
        public Form2()
        {
            InitializeComponent();
            //panel1.SendToBack();
            //panel2.BringToFront();
        }

        public Form2(string noLogin)
        {
            InitializeComponent();
            panel2.SendToBack();
            panel1.BringToFront();
            panel2.Visible = false;
            panel1.Visible = true;
        }



        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        } //종료버튼

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        } //종료이미지

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            
        } //판매관리 이미지

        private void button2_Click(object sender, EventArgs e)
        {
            
        } //매출관리 버튼

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Visible = false; //현재 폼 안보이게 하기
            salesForm frm = new salesForm();
            frm.Owner = this; //form1의 오너를 현재 폼으로
            frm.Show(); //form1 보여주기
        } //매출관리 이미지


        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Visible = false; //현재 폼 안보이게 하기
            Form1 frm = new Form1();
            frm.Owner = this; //form1의 오너를 현재 폼으로
            frm.Show(); //form1 보여주기
        } //판매관리 버튼

        private void btn_1_Click(object sender, EventArgs e)
        {
            textBox_pwd.Text += "*";
            index++;
            pwd += "1";
            sp.SoundLocation = file;
            sp.Play();
        }

        private void btn_2_Click(object sender, EventArgs e)
        {
            textBox_pwd.Text += "*";
            index++;
            pwd += "2";
            sp.SoundLocation = file;
            sp.Play();
        }

        private void btn_3_Click(object sender, EventArgs e)
        {
            textBox_pwd.Text += "*";
            index++;
            pwd += "3";
            sp.SoundLocation = file;
            sp.Play();
        }

        private void btn_4_Click(object sender, EventArgs e)
        {
            textBox_pwd.Text += "*";
            index++;
            pwd += "4";
            sp.SoundLocation = file;
            sp.Play();
        }

        private void btn_5_Click(object sender, EventArgs e)
        {
            textBox_pwd.Text += "*";
            index++;
            pwd += 5;
            sp.SoundLocation = file;
            sp.Play();
        }

        private void btn_6_Click(object sender, EventArgs e)
        {
            textBox_pwd.Text += "*";
            index++;
            pwd += "6";
            sp.SoundLocation = file;
            sp.Play();
        }

        private void btn_7_Click(object sender, EventArgs e)
        {
            textBox_pwd.Text += "*";
            index++;
            pwd += "7";
            sp.SoundLocation = file;
            sp.Play();
        }

        private void btn_8_Click(object sender, EventArgs e)
        {
            textBox_pwd.Text += "*";
            index++;
            pwd += "8";
            sp.SoundLocation = file;
            sp.Play();
        }

        private void btn_9_Click(object sender, EventArgs e)
        {
            textBox_pwd.Text += "*";
            index++;
            pwd += "9";
            sp.SoundLocation = file;
            sp.Play();
        }

        private void btn_0_Click(object sender, EventArgs e)
        {
            textBox_pwd.Text += "*";
            index++;
            pwd += "0";
            sp.SoundLocation = file;
            sp.Play();
        }

        private void btn_C_Click(object sender, EventArgs e)
        {
            textBox_pwd.Clear();
            index = -1;
            pwd = "";
        }

        private void btn_del_Click(object sender, EventArgs e)
        {
            if (index > -1)
            {
                index--;
                textBox_pwd.Text = textBox_pwd.Text.Remove(textBox_pwd.TextLength - 1);
            }
            else
            {
                return;
            }
            string temp = "";
            for (int i = 0; i < index + 1; i++)
            {
                temp += pwd[i];
            }
            pwd = temp;
            //MessageBox.Show(pwd);
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (pwd.Equals("1234"))
            {

                panel2.Visible = false;
                panel1.Visible = true;
                //MessageBox.Show("로그인 성공");
            }
            else
            {
                error.SoundLocation = error_file;
                error.Play();
                MessageBox.Show("비밀번호를 잘못 입력하셨습니다");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false; //현재 폼 안보이게 하기
            Form1 frm = new Form1();
            frm.Owner = this; //form1의 오너를 현재 폼으로
            frm.Show(); //form1 보여주기
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            this.Visible = false; //현재 폼 안보이게 하기
            Form1 frm = new Form1();
            frm.Owner = this; //form1의 오너를 현재 폼으로
            frm.Show(); //form1 보여주기
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Visible = false; //현재 폼 안보이게 하기
            salesForm frm = new salesForm();
            frm.Owner = this; //form1의 오너를 현재 폼으로
            frm.Show(); //form1 보여주기
        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            this.Visible = false; //현재 폼 안보이게 하기
            salesForm frm = new salesForm();
            frm.Owner = this; //form1의 오너를 현재 폼으로
            frm.Show(); //form1 보여주기
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox6_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_MouseMove(object sender, MouseEventArgs e)
        {
            //button1.BackColor = Color.Cyan;
            //pictureBox3.BackColor = Color.Cyan;
        }
    }
}
