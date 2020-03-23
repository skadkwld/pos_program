namespace ook_pos_program
{
    partial class orderPanel
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1_2 = new System.Windows.Forms.Label();
            this.label1_4 = new System.Windows.Forms.Label();
            this.label1_3 = new System.Windows.Forms.Label();
            this.label1_1 = new System.Windows.Forms.Label();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label1_2);
            this.panel4.Controls.Add(this.label1_4);
            this.panel4.Controls.Add(this.label1_3);
            this.panel4.Controls.Add(this.label1_1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(717, 100);
            this.panel4.TabIndex = 2;
            // 
            // label1_2
            // 
            this.label1_2.AutoSize = true;
            this.label1_2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1_2.Location = new System.Drawing.Point(13, 44);
            this.label1_2.Name = "label1_2";
            this.label1_2.Size = new System.Drawing.Size(49, 15);
            this.label1_2.TabIndex = 7;
            this.label1_2.Text = "17:18";
            // 
            // label1_4
            // 
            this.label1_4.AutoSize = true;
            this.label1_4.Location = new System.Drawing.Point(219, 46);
            this.label1_4.Name = "label1_4";
            this.label1_4.Size = new System.Drawing.Size(139, 15);
            this.label1_4.TabIndex = 6;
            this.label1_4.Text = "[메뉴 2개] 10000원";
            // 
            // label1_3
            // 
            this.label1_3.AutoSize = true;
            this.label1_3.BackColor = System.Drawing.Color.Black;
            this.label1_3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1_3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1_3.Location = new System.Drawing.Point(28, 73);
            this.label1_3.Name = "label1_3";
            this.label1_3.Size = new System.Drawing.Size(71, 15);
            this.label1_3.TabIndex = 5;
            this.label1_3.Text = "접수대기";
            // 
            // label1_1
            // 
            this.label1_1.AutoSize = true;
            this.label1_1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1_1.Location = new System.Drawing.Point(14, 15);
            this.label1_1.Name = "label1_1";
            this.label1_1.Size = new System.Drawing.Size(117, 15);
            this.label1_1.TabIndex = 4;
            this.label1_1.Text = "02월 06일 (목)";
            // 
            // orderPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel4);
            this.Name = "orderPanel";
            this.Size = new System.Drawing.Size(717, 100);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1_2;
        private System.Windows.Forms.Label label1_4;
        private System.Windows.Forms.Label label1_3;
        private System.Windows.Forms.Label label1_1;
    }
}
