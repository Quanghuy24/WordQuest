using System;
using System.Drawing;
using System.Windows.Forms;

namespace WordQuest.GUI
{
    partial class frmTopics : System.Windows.Forms.Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTopics));
            lblName = new Label();
            lblStrat = new Label();
            lblTopic = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            button9 = new Button();
            btnNext = new Button();
            btnReset = new Button();
            btnXepHang = new Button();
            btnCaiDat = new Button();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.BackColor = Color.Transparent;
            lblName.FlatStyle = FlatStyle.Flat;
            lblName.Font = new Font("Segoe UI Black", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblName.ForeColor = Color.FromArgb(255, 128, 0);
            lblName.Location = new Point(12, 9);
            lblName.Name = "lblName";
            lblName.Size = new Size(252, 73);
            lblName.TabIndex = 0;
            lblName.Text = "Xin chào, Huy";
            lblName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStrat
            // 
            lblStrat.AutoSize = true;
            lblStrat.BackColor = Color.Transparent;
            lblStrat.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStrat.ForeColor = Color.FromArgb(255, 128, 0);
            lblStrat.Location = new Point(819, 26);
            lblStrat.Name = "lblStrat";
            lblStrat.Size = new Size(105, 38);
            lblStrat.TabIndex = 1;
            lblStrat.Text = "15 Sao\u0090";
            // 
            // lblTopic
            // 
            lblTopic.AutoSize = true;
            lblTopic.BackColor = Color.Transparent;
            lblTopic.Font = new Font("Segoe UI Black", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTopic.ForeColor = Color.FromArgb(255, 220, 0);
            lblTopic.Location = new Point(270, 69);
            lblTopic.Name = "lblTopic";
            lblTopic.Size = new Size(432, 45);
            lblTopic.TabIndex = 2;
            lblTopic.Text = "Chọn chủ đề học hôm nay:";
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            button1.BackColor = Color.FromArgb(255, 224, 192);
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button1.ForeColor = Color.FromArgb(100, 40, 0);
            button1.Location = new Point(72, 166);
            button1.Name = "button1";
            button1.Size = new Size(201, 122);
            button1.TabIndex = 3;
            button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(255, 224, 192);
            button2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button2.ForeColor = Color.FromArgb(100, 40, 0);
            button2.Location = new Point(374, 166);
            button2.Name = "button2";
            button2.Size = new Size(201, 122);
            button2.TabIndex = 4;
            button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.BackColor = Color.FromArgb(255, 224, 192);
            button3.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button3.ForeColor = Color.FromArgb(100, 40, 0);
            button3.Location = new Point(678, 166);
            button3.Name = "button3";
            button3.Size = new Size(201, 122);
            button3.TabIndex = 5;
            button3.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            button4.BackColor = Color.FromArgb(255, 224, 192);
            button4.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button4.ForeColor = Color.FromArgb(100, 40, 0);
            button4.Location = new Point(72, 315);
            button4.Name = "button4";
            button4.Size = new Size(201, 122);
            button4.TabIndex = 6;
            button4.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            button5.BackColor = Color.FromArgb(255, 224, 192);
            button5.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button5.ForeColor = Color.FromArgb(100, 40, 0);
            button5.Location = new Point(374, 315);
            button5.Name = "button5";
            button5.Size = new Size(201, 122);
            button5.TabIndex = 7;
            button5.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            button6.BackColor = Color.FromArgb(255, 224, 192);
            button6.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button6.ForeColor = Color.FromArgb(100, 40, 0);
            button6.Location = new Point(678, 315);
            button6.Name = "button6";
            button6.Size = new Size(201, 122);
            button6.TabIndex = 8;
            button6.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            button7.BackColor = Color.FromArgb(255, 224, 192);
            button7.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button7.ForeColor = Color.FromArgb(100, 40, 0);
            button7.Location = new Point(72, 469);
            button7.Name = "button7";
            button7.Size = new Size(201, 122);
            button7.TabIndex = 9;
            button7.UseVisualStyleBackColor = false;
            // 
            // button8
            // 
            button8.BackColor = Color.FromArgb(255, 224, 192);
            button8.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button8.ForeColor = Color.FromArgb(100, 40, 0);
            button8.Location = new Point(374, 469);
            button8.Name = "button8";
            button8.Size = new Size(201, 122);
            button8.TabIndex = 10;
            button8.UseVisualStyleBackColor = false;
            // 
            // button9
            // 
            button9.BackColor = Color.FromArgb(255, 224, 192);
            button9.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button9.ForeColor = Color.FromArgb(100, 40, 0);
            button9.Location = new Point(678, 469);
            button9.Name = "button9";
            button9.Size = new Size(201, 122);
            button9.TabIndex = 11;
            button9.UseVisualStyleBackColor = false;
            // 
            // btnNext
            // 
            btnNext.BackColor = Color.FromArgb(255, 128, 0);
            btnNext.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnNext.ForeColor = Color.White;
            btnNext.Location = new Point(777, 693);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(169, 57);
            btnNext.TabIndex = 12;
            btnNext.Text = "Trang tiếp";
            btnNext.UseVisualStyleBackColor = false;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.FromArgb(255, 128, 0);
            btnReset.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnReset.ForeColor = Color.White;
            btnReset.Location = new Point(12, 693);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(169, 57);
            btnReset.TabIndex = 13;
            btnReset.Text = "Quay lại";
            btnReset.UseVisualStyleBackColor = false;
            // 
            // btnXepHang
            // 
            btnXepHang.BackColor = Color.FromArgb(255, 128, 0);
            btnXepHang.Font = new Font("Showcard Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnXepHang.ForeColor = Color.White;
            btnXepHang.Location = new Point(374, 664);
            btnXepHang.Name = "btnXepHang";
            btnXepHang.Size = new Size(201, 56);
            btnXepHang.TabIndex = 14;
            btnXepHang.Text = "Xếp hạng";
            btnXepHang.UseVisualStyleBackColor = false;
            // 
            // btnCaiDat
            // 
            btnCaiDat.BackColor = Color.FromArgb(255, 128, 0);
            btnCaiDat.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCaiDat.ForeColor = Color.White;
            btnCaiDat.Location = new Point(395, 597);
            btnCaiDat.Name = "btnCaiDat";
            btnCaiDat.Size = new Size(153, 61);
            btnCaiDat.TabIndex = 15;
            btnCaiDat.Text = "Cài đặt";
            btnCaiDat.UseVisualStyleBackColor = false;
            // 
            // frmTopics
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(958, 774);
            Controls.Add(btnCaiDat);
            Controls.Add(btnXepHang);
            Controls.Add(btnReset);
            Controls.Add(btnNext);
            Controls.Add(button9);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(lblTopic);
            Controls.Add(lblStrat);
            Controls.Add(lblName);
            ForeColor = Color.White;
            Name = "frmTopics";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WordQuest - Chọn chủ đề";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblName;
        private Label lblStrat;
        private Label lblTopic;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        private Button btnNext;
        private Button btnReset;
        private Button btnXepHang;
        private Button btnCaiDat;
    }
}
