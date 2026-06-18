using System.Drawing;
using System.Windows.Forms;

namespace WordQuest.GUI
{
    partial class frmLevels : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLevels));
            btnBack = new Button();
            label1 = new Label();
            label2 = new Label();
            pnlLevel1 = new Panel();
            lblStar1 = new Label();
            btnPlayEN1 = new Button();
            btnPlayVI1 = new Button();
            label5 = new Label();
            lblLock1 = new Label();
            label3 = new Label();
            pnlLevel2 = new Panel();
            lblStar2 = new Label();
            btnPlayEN2 = new Button();
            btnPlayVI2 = new Button();
            label6 = new Label();
            lblLock2 = new Label();
            label8 = new Label();
            pnlLevel3 = new Panel();
            lblStar3 = new Label();
            btnPlayEN3 = new Button();
            btnPlayVI3 = new Button();
            label9 = new Label();
            lblLock3 = new Label();
            label11 = new Label();
            btnPrevLevel = new Button();
            btnNextLevel = new Button();
            lblLevelPage = new Label();
            pnlLevel1.SuspendLayout();
            pnlLevel2.SuspendLayout();
            pnlLevel3.SuspendLayout();
            SuspendLayout();
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.DarkOrange;
            btnBack.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(52, 21);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(163, 54);
            btnBack.TabIndex = 0;
            btnBack.Text = "Quay lại";
            btnBack.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(255, 128, 0);
            label1.Location = new Point(759, 27);
            label1.Name = "label1";
            label1.Size = new Size(173, 48);
            label1.TabIndex = 1;
            label1.Text = "Động vật";
            // 
            // label2
            // 
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.DarkOrange;
            label2.Location = new Point(356, 74);
            label2.Name = "label2";
            label2.Size = new Size(259, 46);
            label2.TabIndex = 2;
            label2.Text = "Chọn màn chơi";
            // 
            // pnlLevel1
            // 
            pnlLevel1.BackColor = SystemColors.ActiveCaption;
            pnlLevel1.BackgroundImage = (Image)resources.GetObject("pnlLevel1.BackgroundImage");
            pnlLevel1.BackgroundImageLayout = ImageLayout.Stretch;
            pnlLevel1.Controls.Add(lblStar1);
            pnlLevel1.Controls.Add(btnPlayEN1);
            pnlLevel1.Controls.Add(btnPlayVI1);
            pnlLevel1.Controls.Add(label5);
            pnlLevel1.Controls.Add(lblLock1);
            pnlLevel1.Controls.Add(label3);
            pnlLevel1.Location = new Point(145, 143);
            pnlLevel1.Name = "pnlLevel1";
            pnlLevel1.Size = new Size(660, 153);
            pnlLevel1.TabIndex = 3;
            // 
            // lblStar1
            // 
            lblStar1.AutoSize = true;
            lblStar1.BackColor = Color.Transparent;
            lblStar1.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStar1.ForeColor = Color.Brown;
            lblStar1.Location = new Point(152, 12);
            lblStar1.Name = "lblStar1";
            lblStar1.Size = new Size(167, 48);
            lblStar1.TabIndex = 5;
            lblStar1.Text = "🌟🌟🌟";
            // 
            // btnPlayEN1
            // 
            btnPlayEN1.BackColor = Color.Lime;
            btnPlayEN1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPlayEN1.ForeColor = Color.Black;
            btnPlayEN1.Location = new Point(487, 89);
            btnPlayEN1.Name = "btnPlayEN1";
            btnPlayEN1.Size = new Size(161, 49);
            btnPlayEN1.TabIndex = 4;
            btnPlayEN1.Text = "Tiếng Anh";
            btnPlayEN1.UseVisualStyleBackColor = false;
            // 
            // btnPlayVI1
            // 
            btnPlayVI1.BackColor = Color.FromArgb(255, 128, 0);
            btnPlayVI1.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPlayVI1.ForeColor = Color.Black;
            btnPlayVI1.Location = new Point(487, 16);
            btnPlayVI1.Name = "btnPlayVI1";
            btnPlayVI1.Size = new Size(161, 49);
            btnPlayVI1.TabIndex = 3;
            btnPlayVI1.Text = "Tiếng Việt";
            btnPlayVI1.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Brown;
            label5.Location = new Point(34, 64);
            label5.Name = "label5";
            label5.Size = new Size(258, 38);
            label5.TabIndex = 2;
            label5.Text = "15 câu";
            // 
            // lblLock1
            // 
            lblLock1.BackColor = Color.Transparent;
            lblLock1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLock1.ForeColor = Color.Brown;
            lblLock1.Location = new Point(34, 102);
            lblLock1.Name = "lblLock1";
            lblLock1.Size = new Size(356, 36);
            lblLock1.TabIndex = 1;
            lblLock1.Text = "Chưa hoàn thành";
            // 
            // label3
            // 
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Brown;
            label3.Location = new Point(34, 12);
            label3.Name = "label3";
            label3.Size = new Size(124, 52);
            label3.TabIndex = 0;
            label3.Text = "Màn 1";
            // 
            // pnlLevel2
            // 
            pnlLevel2.BackgroundImage = (Image)resources.GetObject("pnlLevel2.BackgroundImage");
            pnlLevel2.BackgroundImageLayout = ImageLayout.Stretch;
            pnlLevel2.Controls.Add(lblStar2);
            pnlLevel2.Controls.Add(btnPlayEN2);
            pnlLevel2.Controls.Add(btnPlayVI2);
            pnlLevel2.Controls.Add(label6);
            pnlLevel2.Controls.Add(lblLock2);
            pnlLevel2.Controls.Add(label8);
            pnlLevel2.Location = new Point(145, 331);
            pnlLevel2.Name = "pnlLevel2";
            pnlLevel2.Size = new Size(660, 153);
            pnlLevel2.TabIndex = 5;
            // 
            // lblStar2
            // 
            lblStar2.AutoSize = true;
            lblStar2.BackColor = Color.Transparent;
            lblStar2.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStar2.ForeColor = Color.Brown;
            lblStar2.Location = new Point(152, 12);
            lblStar2.Name = "lblStar2";
            lblStar2.Size = new Size(167, 48);
            lblStar2.TabIndex = 7;
            lblStar2.Text = "🌟🌟🌟";
            // 
            // btnPlayEN2
            // 
            btnPlayEN2.BackColor = Color.Lime;
            btnPlayEN2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPlayEN2.ForeColor = Color.Black;
            btnPlayEN2.Location = new Point(487, 92);
            btnPlayEN2.Name = "btnPlayEN2";
            btnPlayEN2.Size = new Size(161, 49);
            btnPlayEN2.TabIndex = 4;
            btnPlayEN2.Text = "Tiếng Anh";
            btnPlayEN2.UseVisualStyleBackColor = false;
            // 
            // btnPlayVI2
            // 
            btnPlayVI2.BackColor = Color.FromArgb(255, 128, 0);
            btnPlayVI2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPlayVI2.ForeColor = Color.Black;
            btnPlayVI2.Location = new Point(487, 19);
            btnPlayVI2.Name = "btnPlayVI2";
            btnPlayVI2.Size = new Size(161, 49);
            btnPlayVI2.TabIndex = 3;
            btnPlayVI2.Text = "Tiếng Việt";
            btnPlayVI2.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Brown;
            label6.Location = new Point(34, 64);
            label6.Name = "label6";
            label6.Size = new Size(315, 38);
            label6.TabIndex = 2;
            label6.Text = "15 câu";
            // 
            // lblLock2
            // 
            lblLock2.BackColor = Color.Transparent;
            lblLock2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLock2.ForeColor = Color.Brown;
            lblLock2.Location = new Point(34, 102);
            lblLock2.Name = "lblLock2";
            lblLock2.Size = new Size(356, 36);
            lblLock2.TabIndex = 1;
            lblLock2.Text = "Chưa hoàn thành";
            // 
            // label8
            // 
            label8.BackColor = Color.Transparent;
            label8.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.Brown;
            label8.Location = new Point(34, 12);
            label8.Name = "label8";
            label8.Size = new Size(124, 52);
            label8.TabIndex = 0;
            label8.Text = "Màn 2";
            // 
            // pnlLevel3
            // 
            pnlLevel3.BackgroundImage = (Image)resources.GetObject("pnlLevel3.BackgroundImage");
            pnlLevel3.BackgroundImageLayout = ImageLayout.Stretch;
            pnlLevel3.Controls.Add(lblStar3);
            pnlLevel3.Controls.Add(btnPlayEN3);
            pnlLevel3.Controls.Add(btnPlayVI3);
            pnlLevel3.Controls.Add(label9);
            pnlLevel3.Controls.Add(lblLock3);
            pnlLevel3.Controls.Add(label11);
            pnlLevel3.Location = new Point(145, 522);
            pnlLevel3.Name = "pnlLevel3";
            pnlLevel3.Size = new Size(660, 153);
            pnlLevel3.TabIndex = 5;
            // 
            // lblStar3
            // 
            lblStar3.AutoSize = true;
            lblStar3.BackColor = Color.Transparent;
            lblStar3.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblStar3.ForeColor = Color.Brown;
            lblStar3.Location = new Point(152, 9);
            lblStar3.Name = "lblStar3";
            lblStar3.Size = new Size(167, 48);
            lblStar3.TabIndex = 6;
            lblStar3.Text = "🌟🌟🌟";
            // 
            // btnPlayEN3
            // 
            btnPlayEN3.BackColor = Color.Lime;
            btnPlayEN3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPlayEN3.ForeColor = Color.Black;
            btnPlayEN3.Location = new Point(487, 85);
            btnPlayEN3.Name = "btnPlayEN3";
            btnPlayEN3.Size = new Size(161, 49);
            btnPlayEN3.TabIndex = 4;
            btnPlayEN3.Text = "Tiếng Anh";
            btnPlayEN3.UseVisualStyleBackColor = false;
            // 
            // btnPlayVI3
            // 
            btnPlayVI3.BackColor = Color.FromArgb(255, 128, 0);
            btnPlayVI3.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPlayVI3.ForeColor = Color.Black;
            btnPlayVI3.Location = new Point(487, 16);
            btnPlayVI3.Name = "btnPlayVI3";
            btnPlayVI3.Size = new Size(161, 49);
            btnPlayVI3.TabIndex = 3;
            btnPlayVI3.Text = "Tiếng Việt";
            btnPlayVI3.UseVisualStyleBackColor = false;
            // 
            // label9
            // 
            label9.BackColor = Color.Transparent;
            label9.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.Brown;
            label9.Location = new Point(34, 64);
            label9.Name = "label9";
            label9.Size = new Size(258, 38);
            label9.TabIndex = 2;
            label9.Text = "15 câu";
            // 
            // lblLock3
            // 
            lblLock3.BackColor = Color.Transparent;
            lblLock3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLock3.ForeColor = Color.Brown;
            lblLock3.Location = new Point(34, 102);
            lblLock3.Name = "lblLock3";
            lblLock3.Size = new Size(356, 36);
            lblLock3.TabIndex = 1;
            lblLock3.Text = "Chưa hoàn thành";
            // 
            // label11
            // 
            label11.BackColor = Color.Transparent;
            label11.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.ForeColor = Color.Brown;
            label11.Location = new Point(34, 12);
            label11.Name = "label11";
            label11.Size = new Size(124, 52);
            label11.TabIndex = 0;
            label11.Text = "Màn 3";
            // 
            // btnPrevLevel
            // 
            btnPrevLevel.BackColor = Color.FromArgb(255, 128, 0);
            btnPrevLevel.Font = new Font("Showcard Gothic", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPrevLevel.ForeColor = Color.White;
            btnPrevLevel.Location = new Point(36, 686);
            btnPrevLevel.Name = "btnPrevLevel";
            btnPrevLevel.Size = new Size(162, 59);
            btnPrevLevel.TabIndex = 7;
            btnPrevLevel.Text = "Màn trước";
            btnPrevLevel.UseVisualStyleBackColor = false;
            // 
            // btnNextLevel
            // 
            btnNextLevel.BackColor = Color.FromArgb(255, 128, 0);
            btnNextLevel.Font = new Font("Showcard Gothic", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnNextLevel.ForeColor = Color.White;
            btnNextLevel.Location = new Point(706, 686);
            btnNextLevel.Name = "btnNextLevel";
            btnNextLevel.Size = new Size(162, 59);
            btnNextLevel.TabIndex = 8;
            btnNextLevel.Text = "Màn sau";
            btnNextLevel.UseVisualStyleBackColor = false;
            // 
            // lblLevelPage
            // 
            lblLevelPage.AutoSize = true;
            lblLevelPage.BackColor = Color.Transparent;
            lblLevelPage.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLevelPage.ForeColor = Color.Yellow;
            lblLevelPage.Location = new Point(380, 699);
            lblLevelPage.Name = "lblLevelPage";
            lblLevelPage.Size = new Size(139, 32);
            lblLevelPage.TabIndex = 9;
            lblLevelPage.Text = "Trang 1 / 1";
            // 
            // frmLevels
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(958, 774);
            Controls.Add(lblLevelPage);
            Controls.Add(btnNextLevel);
            Controls.Add(btnPrevLevel);
            Controls.Add(pnlLevel2);
            Controls.Add(pnlLevel3);
            Controls.Add(pnlLevel1);
            Controls.Add(label1);
            Controls.Add(btnBack);
            Controls.Add(label2);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "frmLevels";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            pnlLevel1.ResumeLayout(false);
            pnlLevel1.PerformLayout();
            pnlLevel2.ResumeLayout(false);
            pnlLevel2.PerformLayout();
            pnlLevel3.ResumeLayout(false);
            pnlLevel3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnBack;
        private Label label1;
        private Label label2;
        private Panel pnlLevel1;
        private Label label3;
        private Button btnPlayEN1;
        private Button btnPlayVI1;
        private Label label5;
        private Label lblLock1;
        private Panel pnlLevel2;
        private Button btnPlayEN2;
        private Button btnPlayVI2;
        private Label label6;
        private Label lblLock2;
        private Label label8;
        private Panel pnlLevel3;
        private Button btnPlayEN3;
        private Button btnPlayVI3;
        private Label label9;
        private Label lblLock3;
        private Label label11;
        private Label lblStar1;
        private Label lblStar2;
        private Label lblStar3;
        private Button btnPrevLevel;
        private Button btnNextLevel;
        private Label lblLevelPage;
    }
}
