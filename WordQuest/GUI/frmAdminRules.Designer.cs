namespace WordQuest.GUI
{
    partial class frmAdminRules
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
            lblTitle = new Label();
            pnlMain = new Panel();
            label7 = new Label();
            label2 = new Label();
            nudStar3 = new NumericUpDown();
            nudStar2 = new NumericUpDown();
            nudStar1 = new NumericUpDown();
            nudStreakBonus = new NumericUpDown();
            nudLives = new NumericUpDown();
            nudTimeLđiểmit = new NumericUpDown();
            nudQuestionCount = new NumericUpDown();
            lblStar3 = new Label();
            lblStar2 = new Label();
            lblStar1 = new Label();
            lblStreakBonus = new Label();
            lblLives = new Label();
            lblTimeLđiểmit = new Label();
            lblQuestionCount = new Label();
            btnSave = new Button();
            btnDefault = new Button();
            btnBack = new Button();
            pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudStar3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStar2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudStreakBonus).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudLives).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudTimeLđiểmit).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudQuestionCount).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Showcard Gothic", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(305, 37);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(306, 50);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "QUY TẮC GAME";
            // 
            // pnlMain
            // 
            pnlMain.AutoSize = true;
            pnlMain.Controls.Add(label7);
            pnlMain.Controls.Add(label2);
            pnlMain.Controls.Add(nudStar3);
            pnlMain.Controls.Add(nudStar2);
            pnlMain.Controls.Add(nudStar1);
            pnlMain.Controls.Add(nudStreakBonus);
            pnlMain.Controls.Add(nudLives);
            pnlMain.Controls.Add(nudTimeLđiểmit);
            pnlMain.Controls.Add(nudQuestionCount);
            pnlMain.Controls.Add(lblStar3);
            pnlMain.Controls.Add(lblStar2);
            pnlMain.Controls.Add(lblStar1);
            pnlMain.Controls.Add(lblStreakBonus);
            pnlMain.Controls.Add(lblLives);
            pnlMain.Controls.Add(lblTimeLđiểmit);
            pnlMain.Controls.Add(lblQuestionCount);
            pnlMain.Location = new Point(90, 102);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(754, 566);
            pnlMain.TabIndex = 1;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(52, 0);
            label7.Name = "label7";
            label7.Size = new Size(0, 25);
            label7.TabIndex = 20;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(52, 224);
            label2.Name = "label2";
            label2.Size = new Size(0, 25);
            label2.TabIndex = 15;
            // 
            // nudStar3
            // 
            nudStar3.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            nudStar3.Location = new Point(546, 485);
            nudStar3.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            nudStar3.Name = "nudStar3";
            nudStar3.Size = new Size(153, 45);
            nudStar3.TabIndex = 13;
            nudStar3.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // nudStar2
            // 
            nudStar2.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            nudStar2.Location = new Point(546, 409);
            nudStar2.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            nudStar2.Name = "nudStar2";
            nudStar2.Size = new Size(153, 45);
            nudStar2.TabIndex = 12;
            nudStar2.Value = new decimal(new int[] { 80, 0, 0, 0 });
            // 
            // nudStar1
            // 
            nudStar1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            nudStar1.Location = new Point(546, 333);
            nudStar1.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            nudStar1.Name = "nudStar1";
            nudStar1.Size = new Size(153, 45);
            nudStar1.TabIndex = 11;
            nudStar1.Value = new decimal(new int[] { 50, 0, 0, 0 });
            // 
            // nudStreakBonus
            // 
            nudStreakBonus.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            nudStreakBonus.Location = new Point(546, 257);
            nudStreakBonus.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            nudStreakBonus.Name = "nudStreakBonus";
            nudStreakBonus.Size = new Size(153, 45);
            nudStreakBonus.TabIndex = 10;
            nudStreakBonus.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // nudLives
            // 
            nudLives.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            nudLives.Location = new Point(546, 178);
            nudLives.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            nudLives.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudLives.Name = "nudLives";
            nudLives.Size = new Size(153, 45);
            nudLives.TabIndex = 9;
            nudLives.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // nudTimeLđiểmit
            // 
            nudTimeLđiểmit.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            nudTimeLđiểmit.Location = new Point(546, 100);
            nudTimeLđiểmit.Maximum = new decimal(new int[] { 120, 0, 0, 0 });
            nudTimeLđiểmit.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            nudTimeLđiểmit.Name = "nudTimeLđiểmit";
            nudTimeLđiểmit.Size = new Size(153, 45);
            nudTimeLđiểmit.TabIndex = 8;
            nudTimeLđiểmit.Value = new decimal(new int[] { 60, 0, 0, 0 });
            // 
            // nudQuestionCount
            // 
            nudQuestionCount.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            nudQuestionCount.Location = new Point(541, 28);
            nudQuestionCount.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            nudQuestionCount.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            nudQuestionCount.Name = "nudQuestionCount";
            nudQuestionCount.Size = new Size(153, 45);
            nudQuestionCount.TabIndex = 7;
            nudQuestionCount.Value = new decimal(new int[] { 15, 0, 0, 0 });
            // 
            // lblStar3
            // 
            lblStar3.AutoSize = true;
            lblStar3.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblStar3.Location = new Point(52, 487);
            lblStar3.Name = "lblStar3";
            lblStar3.Size = new Size(291, 38);
            lblStar3.TabIndex = 6;
            lblStar3.Text = "Ngưỡng sao 3 (Khó):";
            // 
            // lblStar2
            // 
            lblStar2.AutoSize = true;
            lblStar2.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblStar2.Location = new Point(52, 409);
            lblStar2.Name = "lblStar2";
            lblStar2.Size = new Size(273, 38);
            lblStar2.TabIndex = 5;
            lblStar2.Text = "Ngưỡng sao 2 (TB):";
            // 
            // lblStar1
            // 
            lblStar1.AutoSize = true;
            lblStar1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblStar1.Location = new Point(52, 335);
            lblStar1.Name = "lblStar1";
            lblStar1.Size = new Size(275, 38);
            lblStar1.TabIndex = 4;
            lblStar1.Text = "Ngưỡng sao 1 (Dễ):";
            // 
            // lblStreakBonus
            // 
            lblStreakBonus.AutoSize = true;
            lblStreakBonus.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblStreakBonus.Location = new Point(52, 259);
            lblStreakBonus.Name = "lblStreakBonus";
            lblStreakBonus.Size = new Size(291, 76);
            lblStreakBonus.TabIndex = 3;
            lblStreakBonus.Text = "Điểm thưởng streak:\r\n\r\n";
            // 
            // lblLives
            // 
            lblLives.AutoSize = true;
            lblLives.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblLives.Location = new Point(52, 185);
            lblLives.Name = "lblLives";
            lblLives.Size = new Size(141, 38);
            lblLives.TabIndex = 2;
            lblLives.Text = "Số mạng:";
            // 
            // lblTimeLđiểmit
            // 
            lblTimeLđiểmit.AutoSize = true;
            lblTimeLđiểmit.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTimeLđiểmit.Location = new Point(57, 99);
            lblTimeLđiểmit.Name = "lblTimeLđiểmit";
            lblTimeLđiểmit.Size = new Size(366, 38);
            lblTimeLđiểmit.TabIndex = 1;
            lblTimeLđiểmit.Text = "Thời gian làm 1 câu (giây):";
            // 
            // lblQuestionCount
            // 
            lblQuestionCount.AutoSize = true;
            lblQuestionCount.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblQuestionCount.Location = new Point(57, 30);
            lblQuestionCount.Name = "lblQuestionCount";
            lblQuestionCount.Size = new Size(249, 38);
            lblQuestionCount.TabIndex = 0;
            lblQuestionCount.Text = "Câu hỏi mỗi màn:";
            lblQuestionCount.Click += lblQuestionCount_Click;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(187, 682);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(165, 61);
            btnSave.TabIndex = 2;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += BtnSave_Click;
            // 
            // btnDefault
            // 
            btnDefault.Location = new Point(383, 682);
            btnDefault.Name = "btnDefault";
            btnDefault.Size = new Size(165, 61);
            btnDefault.TabIndex = 3;
            btnDefault.Text = "Mặc định";
            btnDefault.UseVisualStyleBackColor = true;
            btnDefault.Click += BtnDefault_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(587, 682);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(165, 61);
            btnBack.TabIndex = 4;
            btnBack.Text = "Quay lại";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += BtnBack_Click;
            // 
            // frmAdminRules
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(958, 774);
            Controls.Add(btnBack);
            Controls.Add(btnDefault);
            Controls.Add(btnSave);
            Controls.Add(pnlMain);
            Controls.Add(lblTitle);
            Name = "frmAdminRules";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý quy tắc game";
            Load += frmAdminRules_Load;
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudStar3).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStar2).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudStreakBonus).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudLives).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudTimeLđiểmit).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudQuestionCount).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Panel pnlMain;
        private Label lblStar3;
        private Label lblStar2;
        private Label lblStar1;
        private Label lblStreakBonus;
        private Label lblLives;
        private Label lblTimeLđiểmit;
        private Label lblQuestionCount;
        private NumericUpDown nudStar3;
        private NumericUpDown nudStar2;
        private NumericUpDown nudStar1;
        private NumericUpDown nudStreakBonus;
        private NumericUpDown nudLives;
        private NumericUpDown nudTimeLđiểmit;
        private NumericUpDown nudQuestionCount;
        private Button btnSave;
        private Button btnDefault;
        private Button btnBack;
        private Label label2;
        private Label label7;
    }
}
