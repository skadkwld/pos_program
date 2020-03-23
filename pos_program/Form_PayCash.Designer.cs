namespace ook_pos_program
{
    partial class Form_PayCash
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_PayCash));
            this.button42 = new System.Windows.Forms.Button();
            this.tb_money_return = new System.Windows.Forms.TextBox();
            this.tb_money_got = new System.Windows.Forms.TextBox();
            this.tb_money_get = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button41 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_remain = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.SuspendLayout();
            // 
            // button42
            // 
            this.button42.BackColor = System.Drawing.Color.DarkKhaki;
            this.button42.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button42.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button42.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.button42.Location = new System.Drawing.Point(483, 131);
            this.button42.Name = "button42";
            this.button42.Size = new System.Drawing.Size(181, 142);
            this.button42.TabIndex = 95;
            this.button42.Text = "결제";
            this.button42.UseVisualStyleBackColor = false;
            this.button42.Click += new System.EventHandler(this.button42_Click);
            // 
            // tb_money_return
            // 
            this.tb_money_return.Location = new System.Drawing.Point(253, 243);
            this.tb_money_return.Name = "tb_money_return";
            this.tb_money_return.Size = new System.Drawing.Size(161, 25);
            this.tb_money_return.TabIndex = 94;
            // 
            // tb_money_got
            // 
            this.tb_money_got.Location = new System.Drawing.Point(253, 192);
            this.tb_money_got.Name = "tb_money_got";
            this.tb_money_got.Size = new System.Drawing.Size(161, 25);
            this.tb_money_got.TabIndex = 93;
            // 
            // tb_money_get
            // 
            this.tb_money_get.Location = new System.Drawing.Point(253, 147);
            this.tb_money_get.Name = "tb_money_get";
            this.tb_money_get.Size = new System.Drawing.Size(161, 25);
            this.tb_money_get.TabIndex = 92;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(141, 246);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 15);
            this.label9.TabIndex = 91;
            this.label9.Text = "거스름돈";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(141, 195);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 90;
            this.label8.Text = "받은금액";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(141, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 15);
            this.label7.TabIndex = 89;
            this.label7.Text = "받을금액";
            // 
            // button41
            // 
            this.button41.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button41.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button41.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button41.Location = new System.Drawing.Point(698, 14);
            this.button41.Name = "button41";
            this.button41.Size = new System.Drawing.Size(54, 36);
            this.button41.TabIndex = 88;
            this.button41.Text = "X";
            this.button41.UseVisualStyleBackColor = false;
            this.button41.Click += new System.EventHandler(this.button41_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(13, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 24);
            this.label3.TabIndex = 87;
            this.label3.Text = "현금결제";
            // 
            // btn_remain
            // 
            this.btn_remain.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_remain.Font = new System.Drawing.Font("굴림", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_remain.ForeColor = System.Drawing.Color.White;
            this.btn_remain.Location = new System.Drawing.Point(286, 291);
            this.btn_remain.Name = "btn_remain";
            this.btn_remain.Size = new System.Drawing.Size(128, 36);
            this.btn_remain.TabIndex = 101;
            this.btn_remain.Text = "거스름돈계산";
            this.btn_remain.UseVisualStyleBackColor = false;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Font = new System.Drawing.Font("굴림", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(213, 291);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 36);
            this.button1.TabIndex = 103;
            this.button1.Text = "인쇄";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // Form_PayCash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 441);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button42);
            this.Controls.Add(this.tb_money_return);
            this.Controls.Add(this.tb_money_got);
            this.Controls.Add(this.tb_money_get);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button41);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_remain);
            this.Name = "Form_PayCash";
            this.Load += new System.EventHandler(this.Form_PayCash_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button42;
        private System.Windows.Forms.TextBox tb_money_return;
        private System.Windows.Forms.TextBox tb_money_got;
        private System.Windows.Forms.TextBox tb_money_get;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button41;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_remain;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
    }
}