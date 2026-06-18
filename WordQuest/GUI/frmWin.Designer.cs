using System;
using System.Drawing;
using System.Windows.Forms;

namespace WordQuest.GUI
{
    partial class frmWin : System.Windows.Forms.Form
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
            lblStars = new Label();
            pnlInfo = new Panel();
            lblRankInfo = new Label();
            lblScoreInfo = new Label();
            lblCorrectInfo = new Label();
            lblTopicInfo = new Label();
            btnReplay = new Button();
            btnLevels = new Button();
            btnHome = new Button();
            pnlInfo.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Black", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.Yellow;
            lblTitle.Location = new Point(333, 47);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(271, 65);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "XUẤT SẮC";
            // 
            // lblStars
            // 
            lblStars.AutoSize = true;
            lblStars.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStars.ForeColor = Color.Yellow;
            lblStars.Location = new Point(377, 143);
            lblStars.Name = "lblStars";
            lblStars.Size = new Size(191, 54);
            lblStars.TabIndex = 1;
            lblStars.Text = "🌟🌟🌟";
            // 
            // pnlInfo
            // 
            pnlInfo.BackColor = Color.FromArgb(30, 30, 60);
            pnlInfo.Controls.Add(lblRankInfo);
            pnlInfo.Controls.Add(lblScoreInfo);
            pnlInfo.Controls.Add(lblCorrectInfo);
            pnlInfo.Controls.Add(lblTopicInfo);
            pnlInfo.Location = new Point(229, 237);
            pnlInfo.Name = "pnlInfo";
            pnlInfo.Size = new Size(500, 240);
            pnlInfo.TabIndex = 2;
            // 
            // lblRankInfo
            // 
            lblRankInfo.AutoSize = true;
            lblRankInfo.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblRankInfo.ForeColor = Color.FromArgb(255, 128, 0);
            lblRankInfo.Location = new Point(21, 180);
            lblRankInfo.Name = "lblRankInfo";
            lblRankInfo.Size = new Size(253, 32);
            lblRankInfo.TabIndex = 3;
            lblRankInfo.Text = "Xếp loại: S - Hoàn hảo";
            // 
            // lblScoreInfo
            // 
            lblScoreInfo.AutoSize = true;
            lblScoreInfo.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblScoreInfo.ForeColor = Color.White;
            lblScoreInfo.Location = new Point(21, 131);
            lblScoreInfo.Name = "lblScoreInfo";
            lblScoreInfo.Size = new Size(289, 32);
            lblScoreInfo.TabIndex = 2;
            lblScoreInfo.Text = "🎯 Điểm số:      150 điểm";
            // 
            // lblCorrectInfo
            // 
            lblCorrectInfo.AutoSize = true;
            lblCorrectInfo.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblCorrectInfo.ForeColor = Color.White;
            lblCorrectInfo.Location = new Point(21, 72);
            lblCorrectInfo.Name = "lblCorrectInfo";
            lblCorrectInfo.Size = new Size(268, 32);
            lblCorrectInfo.TabIndex = 1;
            lblCorrectInfo.Text = "✅ Số câu đúng:  15/15";
            // 
            // lblTopicInfo
            // 
            lblTopicInfo.AutoSize = true;
            lblTopicInfo.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTopicInfo.ForeColor = Color.FromArgb(255, 128, 0);
            lblTopicInfo.Location = new Point(21, 21);
            lblTopicInfo.Name = "lblTopicInfo";
            lblTopicInfo.Size = new Size(293, 32);
            lblTopicInfo.TabIndex = 0;
            lblTopicInfo.Text = "Chủ đề: Động vật - Màn 1";
            // 
            // btnReplay
            // 
            btnReplay.BackColor = Color.FromArgb(255, 128, 0);
            btnReplay.FlatAppearance.BorderSize = 0;
            btnReplay.FlatStyle = FlatStyle.Flat;
            btnReplay.Font = new Font("Segoe UI Black", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnReplay.ForeColor = Color.White;
            btnReplay.Location = new Point(194, 536);
            btnReplay.Name = "btnReplay";
            btnReplay.Size = new Size(238, 52);
            btnReplay.TabIndex = 3;
            btnReplay.Text = "CHƠI LẠI";
            btnReplay.UseVisualStyleBackColor = false;
            // 
            // btnLevels
            // 
            btnLevels.BackColor = Color.FromArgb(255, 128, 0);
            btnLevels.FlatAppearance.BorderSize = 0;
            btnLevels.FlatStyle = FlatStyle.Flat;
            btnLevels.Font = new Font("Segoe UI Black", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLevels.ForeColor = Color.White;
            btnLevels.Location = new Point(523, 536);
            btnLevels.Name = "btnLevels";
            btnLevels.Size = new Size(238, 52);
            btnLevels.TabIndex = 4;
            btnLevels.Text = "CHỌN MÀN";
            btnLevels.UseVisualStyleBackColor = false;
            // 
            // btnHome
            // 
            btnHome.BackColor = Color.FromArgb(255, 128, 0);
            btnHome.FlatAppearance.BorderSize = 0;
            btnHome.FlatStyle = FlatStyle.Flat;
            btnHome.Font = new Font("Segoe UI Black", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnHome.ForeColor = Color.White;
            btnHome.Location = new Point(366, 633);
            btnHome.Name = "btnHome";
            btnHome.Size = new Size(254, 52);
            btnHome.TabIndex = 5;
            btnHome.Text = "TRANG CHỦ";
            btnHome.UseVisualStyleBackColor = false;
            // 
            // frmWin
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 12, 41);
            ClientSize = new Size(958, 774);
            Controls.Add(btnHome);
            Controls.Add(btnLevels);
            Controls.Add(btnReplay);
            Controls.Add(pnlInfo);
            Controls.Add(lblStars);
            Controls.Add(lblTitle);
            Name = "frmWin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmWin";
            Load += frmWin_Load;
            pnlInfo.ResumeLayout(false);
            pnlInfo.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblStars;
        private Panel pnlInfo;
        private Label lblRankInfo;
        private Label lblScoreInfo;
        private Label lblCorrectInfo;
        private Label lblTopicInfo;
        private Button btnReplay;
        private Button btnLevels;
        private Button btnHome;
    }
}
