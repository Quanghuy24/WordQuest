namespace WordQuest.GUI
{
    partial class frmAdminMenu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            btnCauHoi = new Button();
            btnNguoiChoi = new Button();
            btnQuyTac = new Button();
            btnClose = new Button();
            btnTopics = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Showcard Gothic", 14F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(255, 220, 80);
            lblTitle.Location = new Point(121, 25);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(125, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "CÀI ĐẶT";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnCauHoi
            // 
            btnCauHoi.BackColor = Color.FromArgb(255, 128, 0);
            btnCauHoi.FlatStyle = FlatStyle.Flat;
            btnCauHoi.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCauHoi.ForeColor = Color.White;
            btnCauHoi.Location = new Point(54, 88);
            btnCauHoi.Name = "btnCauHoi";
            btnCauHoi.Size = new Size(263, 59);
            btnCauHoi.TabIndex = 1;
            btnCauHoi.Text = "QUẢN LÝ CÂU HỎI";
            btnCauHoi.UseVisualStyleBackColor = false;
            btnCauHoi.Click += btnCauHoi_Click;
            // 
            // btnNguoiChoi
            // 
            btnNguoiChoi.BackColor = Color.FromArgb(255, 128, 0);
            btnNguoiChoi.FlatStyle = FlatStyle.Flat;
            btnNguoiChoi.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNguoiChoi.ForeColor = Color.White;
            btnNguoiChoi.Location = new Point(54, 218);
            btnNguoiChoi.Name = "btnNguoiChoi";
            btnNguoiChoi.Size = new Size(263, 59);
            btnNguoiChoi.TabIndex = 2;
            btnNguoiChoi.Text = "QUẢN LÝ NGƯỜI CHƠI";
            btnNguoiChoi.UseVisualStyleBackColor = false;
            btnNguoiChoi.Click += btnNguoiChoi_Click;
            // 
            // btnQuyTac
            // 
            btnQuyTac.BackColor = Color.FromArgb(255, 128, 0);
            btnQuyTac.FlatStyle = FlatStyle.Flat;
            btnQuyTac.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnQuyTac.ForeColor = Color.White;
            btnQuyTac.Location = new Point(54, 283);
            btnQuyTac.Name = "btnQuyTac";
            btnQuyTac.Size = new Size(263, 59);
            btnQuyTac.TabIndex = 3;
            btnQuyTac.Text = "QUY TẮC GAME";
            btnQuyTac.UseVisualStyleBackColor = false;
            btnQuyTac.Click += btnQuyTac_Click;
            // 
            // btnClose
            // 
            btnClose.BackColor = Color.FromArgb(75, 45, 15);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnClose.ForeColor = Color.FromArgb(255, 220, 80);
            btnClose.Location = new Point(107, 370);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(139, 48);
            btnClose.TabIndex = 4;
            btnClose.Text = "ĐÓNG";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // btnTopics
            // 
            btnTopics.BackColor = Color.FromArgb(255, 128, 0);
            btnTopics.FlatStyle = FlatStyle.Flat;
            btnTopics.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnTopics.ForeColor = Color.White;
            btnTopics.Location = new Point(54, 153);
            btnTopics.Name = "btnTopics";
            btnTopics.Size = new Size(263, 59);
            btnTopics.TabIndex = 5;
            btnTopics.Text = "QUẢN LÝ CHỦ ĐỀ";
            btnTopics.UseVisualStyleBackColor = false;
            btnTopics.Click += btnTopics_Click;
            // 
            // frmAdminMenu
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(120, 80, 40);
            ClientSize = new Size(364, 444);
            Controls.Add(btnTopics);
            Controls.Add(btnClose);
            Controls.Add(btnQuyTac);
            Controls.Add(btnNguoiChoi);
            Controls.Add(btnCauHoi);
            Controls.Add(lblTitle);
            Name = "frmAdminMenu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cài đặt";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle;
        private Button btnCauHoi;
        private Button btnNguoiChoi;
        private Button btnQuyTac;
        private Button btnClose;
        private Button btnTopics;
    }
}
