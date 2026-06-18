namespace WordQuest.GUI
{
    partial class frmAdminPlayers
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
            pnlStat1 = new Panel();
            lblTotalPlayers = new Label();
            lblStat1Title = new Label();
            pnlStat2 = new Panel();
            lblTotalGames = new Label();
            lblStat2Title = new Label();
            pnlStat3 = new Panel();
            lblTopScore = new Label();
            lblStat3Title = new Label();
            pnlStat4 = new Panel();
            lblTopStreak = new Label();
            lblStat4Title = new Label();
            dgvPlayers = new DataGridView();
            btnBack = new Button();
            lblSelectedPlayer = new Label();
            btnTabProgress = new Button();
            btnTabHistory = new Button();
            dgvDetail = new DataGridView();
            pnlReset = new Panel();
            btnResetAll = new Button();
            btnResetSH = new Button();
            btnResetPP = new Button();
            colPlayerID = new DataGridViewTextBoxColumn();
            colUsername = new DataGridViewTextBoxColumn();
            colTotalScore = new DataGridViewTextBoxColumn();
            colTotalStars = new DataGridViewTextBoxColumn();
            colDayStreak = new DataGridViewTextBoxColumn();
            colLastPlayed = new DataGridViewTextBoxColumn();
            colCreatedAt = new DataGridViewTextBoxColumn();
            colXoaNguoiChoi = new DataGridViewTextBoxColumn();
            colD1 = new DataGridViewTextBoxColumn();
            colD2 = new DataGridViewTextBoxColumn();
            colD3 = new DataGridViewTextBoxColumn();
            colD4 = new DataGridViewTextBoxColumn();
            colD5 = new DataGridViewTextBoxColumn();
            colD6 = new DataGridViewTextBoxColumn();
            pnlStat1.SuspendLayout();
            pnlStat2.SuspendLayout();
            pnlStat3.SuspendLayout();
            pnlStat4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPlayers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetail).BeginInit();
            pnlReset.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Showcard Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitle.Location = new Point(300, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(257, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "QUẢN LÝ NGƯỜI CHƠI";
            // 
            // pnlStat1
            // 
            pnlStat1.Controls.Add(lblTotalPlayers);
            pnlStat1.Controls.Add(lblStat1Title);
            pnlStat1.Location = new Point(16, 58);
            pnlStat1.Name = "pnlStat1";
            pnlStat1.Size = new Size(228, 56);
            pnlStat1.TabIndex = 1;
            // 
            // lblTotalPlayers
            // 
            lblTotalPlayers.AutoSize = true;
            lblTotalPlayers.Location = new Point(99, 25);
            lblTotalPlayers.Name = "lblTotalPlayers";
            lblTotalPlayers.Size = new Size(22, 25);
            lblTotalPlayers.TabIndex = 1;
            lblTotalPlayers.Text = "0";
            // 
            // lblStat1Title
            // 
            lblStat1Title.AutoSize = true;
            lblStat1Title.Location = new Point(42, 0);
            lblStat1Title.Name = "lblStat1Title";
            lblStat1Title.Size = new Size(143, 25);
            lblStat1Title.TabIndex = 0;
            lblStat1Title.Text = "Tổng người chơi";
            // 
            // pnlStat2
            // 
            pnlStat2.Controls.Add(lblTotalGames);
            pnlStat2.Controls.Add(lblStat2Title);
            pnlStat2.Location = new Point(250, 58);
            pnlStat2.Name = "pnlStat2";
            pnlStat2.Size = new Size(228, 56);
            pnlStat2.TabIndex = 2;
            // 
            // lblTotalGames
            // 
            lblTotalGames.AutoSize = true;
            lblTotalGames.Location = new Point(102, 25);
            lblTotalGames.Name = "lblTotalGames";
            lblTotalGames.Size = new Size(22, 25);
            lblTotalGames.TabIndex = 1;
            lblTotalGames.Text = "0";
            // 
            // lblStat2Title
            // 
            lblStat2Title.AutoSize = true;
            lblStat2Title.Location = new Point(42, 0);
            lblStat2Title.Name = "lblStat2Title";
            lblStat2Title.Size = new Size(128, 25);
            lblStat2Title.TabIndex = 0;
            lblStat2Title.Text = "Tổng lượt chơi";
            // 
            // pnlStat3
            // 
            pnlStat3.Controls.Add(lblTopScore);
            pnlStat3.Controls.Add(lblStat3Title);
            pnlStat3.Location = new Point(484, 58);
            pnlStat3.Name = "pnlStat3";
            pnlStat3.Size = new Size(228, 56);
            pnlStat3.TabIndex = 2;
            // 
            // lblTopScore
            // 
            lblTopScore.AutoSize = true;
            lblTopScore.Location = new Point(98, 25);
            lblTopScore.Name = "lblTopScore";
            lblTopScore.Size = new Size(22, 25);
            lblTopScore.TabIndex = 1;
            lblTopScore.Text = "0";
            // 
            // lblStat3Title
            // 
            lblStat3Title.AutoSize = true;
            lblStat3Title.Location = new Point(42, 0);
            lblStat3Title.Name = "lblStat3Title";
            lblStat3Title.Size = new Size(127, 25);
            lblStat3Title.TabIndex = 0;
            lblStat3Title.Text = "Điểm cao nhất";
            // 
            // pnlStat4
            // 
            pnlStat4.Controls.Add(lblTopStreak);
            pnlStat4.Controls.Add(lblStat4Title);
            pnlStat4.Location = new Point(718, 58);
            pnlStat4.Name = "pnlStat4";
            pnlStat4.Size = new Size(228, 56);
            pnlStat4.TabIndex = 2;
            // 
            // lblTopStreak
            // 
            lblTopStreak.AutoSize = true;
            lblTopStreak.Location = new Point(98, 25);
            lblTopStreak.Name = "lblTopStreak";
            lblTopStreak.Size = new Size(22, 25);
            lblTopStreak.TabIndex = 1;
            lblTopStreak.Text = "0";
            // 
            // lblStat4Title
            // 
            lblStat4Title.AutoSize = true;
            lblStat4Title.Location = new Point(42, 0);
            lblStat4Title.Name = "lblStat4Title";
            lblStat4Title.Size = new Size(129, 25);
            lblStat4Title.TabIndex = 0;
            lblStat4Title.Text = "Streak dài nhất";
            // 
            // dgvPlayers
            // 
            dgvPlayers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPlayers.Columns.AddRange(new DataGridViewColumn[] { colPlayerID, colUsername, colTotalScore, colTotalStars, colDayStreak, colLastPlayed, colCreatedAt, colXoaNguoiChoi });
            dgvPlayers.Location = new Point(16, 120);
            dgvPlayers.Name = "dgvPlayers";
            dgvPlayers.RowHeadersVisible = false;
            dgvPlayers.RowHeadersWidth = 62;
            dgvPlayers.Size = new Size(930, 379);
            dgvPlayers.TabIndex = 3;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(797, 3);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(130, 34);
            btnBack.TabIndex = 7;
            btnBack.Text = "Quay lại";
            btnBack.UseVisualStyleBackColor = true;
            // 
            // lblSelectedPlayer
            // 
            lblSelectedPlayer.AutoSize = true;
            lblSelectedPlayer.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSelectedPlayer.Location = new Point(16, 516);
            lblSelectedPlayer.Name = "lblSelectedPlayer";
            lblSelectedPlayer.Size = new Size(344, 30);
            lblSelectedPlayer.TabIndex = 8;
            lblSelectedPlayer.Text = "Chọn người chơi để xem chi tiết";
            // 
            // btnTabProgress
            // 
            btnTabProgress.Location = new Point(657, 505);
            btnTabProgress.Name = "btnTabProgress";
            btnTabProgress.Size = new Size(140, 38);
            btnTabProgress.TabIndex = 9;
            btnTabProgress.Text = "Tiến độ chủ đề";
            btnTabProgress.UseVisualStyleBackColor = true;
            // 
            // btnTabHistory
            // 
            btnTabHistory.Location = new Point(803, 505);
            btnTabHistory.Name = "btnTabHistory";
            btnTabHistory.Size = new Size(140, 38);
            btnTabHistory.TabIndex = 10;
            btnTabHistory.Text = "Lịch sử chơi";
            btnTabHistory.UseVisualStyleBackColor = true;
            // 
            // dgvDetail
            // 
            dgvDetail.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetail.Columns.AddRange(new DataGridViewColumn[] { colD1, colD2, colD3, colD4, colD5, colD6 });
            dgvDetail.Location = new Point(16, 549);
            dgvDetail.Name = "dgvDetail";
            dgvDetail.RowHeadersVisible = false;
            dgvDetail.RowHeadersWidth = 62;
            dgvDetail.Size = new Size(930, 263);
            dgvDetail.TabIndex = 12;
            // 
            // pnlReset
            // 
            pnlReset.Controls.Add(btnResetAll);
            pnlReset.Controls.Add(btnResetSH);
            pnlReset.Controls.Add(btnResetPP);
            pnlReset.Controls.Add(btnBack);
            pnlReset.Location = new Point(16, 818);
            pnlReset.Name = "pnlReset";
            pnlReset.Size = new Size(930, 42);
            pnlReset.TabIndex = 13;
            // 
            // btnResetAll
            // 
            btnResetAll.Location = new Point(449, 3);
            btnResetAll.Name = "btnResetAll";
            btnResetAll.Size = new Size(217, 36);
            btnResetAll.TabIndex = 3;
            btnResetAll.Text = "Làm mới tất cả";
            btnResetAll.UseVisualStyleBackColor = true;
            // 
            // btnResetSH
            // 
            btnResetSH.Location = new Point(226, 3);
            btnResetSH.Name = "btnResetSH";
            btnResetSH.Size = new Size(217, 36);
            btnResetSH.TabIndex = 2;
            btnResetSH.Text = "Xoá lịch sử chơi";
            btnResetSH.UseVisualStyleBackColor = true;
            // 
            // btnResetPP
            // 
            btnResetPP.Location = new Point(3, 3);
            btnResetPP.Name = "btnResetPP";
            btnResetPP.Size = new Size(217, 36);
            btnResetPP.TabIndex = 1;
            btnResetPP.Text = "Làm mới tiến độ";
            btnResetPP.UseVisualStyleBackColor = true;
            // 
            // colPlayerID
            // 
            colPlayerID.HeaderText = "ID";
            colPlayerID.MinimumWidth = 8;
            colPlayerID.Name = "colPlayerID";
            colPlayerID.Width = 50;
            // 
            // colUsername
            // 
            colUsername.HeaderText = "Tên người chơi";
            colUsername.MinimumWidth = 8;
            colUsername.Name = "colUsername";
            colUsername.Width = 150;
            // 
            // colTotalScore
            // 
            colTotalScore.HeaderText = "Tổng điểm";
            colTotalScore.MinimumWidth = 8;
            colTotalScore.Name = "colTotalScore";
            colTotalScore.Width = 150;
            // 
            // colTotalStars
            // 
            colTotalStars.HeaderText = "Tổng sao";
            colTotalStars.MinimumWidth = 8;
            colTotalStars.Name = "colTotalStars";
            colTotalStars.Width = 150;
            // 
            // colDayStreak
            // 
            colDayStreak.HeaderText = "Streak";
            colDayStreak.MinimumWidth = 8;
            colDayStreak.Name = "colDayStreak";
            colDayStreak.Width = 150;
            // 
            // colLastPlayed
            // 
            colLastPlayed.HeaderText = "Lần cuối chơi";
            colLastPlayed.MinimumWidth = 8;
            colLastPlayed.Name = "colLastPlayed";
            colLastPlayed.Width = 150;
            // 
            // colCreatedAt
            // 
            colCreatedAt.HeaderText = "Ngày tạo";
            colCreatedAt.MinimumWidth = 8;
            colCreatedAt.Name = "colCreatedAt";
            colCreatedAt.Width = 150;
            // 
            // colXoaNguoiChoi
            // 
            colXoaNguoiChoi.HeaderText = "Xoá";
            colXoaNguoiChoi.MinimumWidth = 8;
            colXoaNguoiChoi.Name = "colXoaNguoiChoi";
            colXoaNguoiChoi.Width = 76;
            // 
            // colD1
            // 
            colD1.HeaderText = "Chủ đề";
            colD1.MinimumWidth = 8;
            colD1.Name = "colD1";
            colD1.Width = 150;
            // 
            // colD2
            // 
            colD2.HeaderText = "Màn";
            colD2.MinimumWidth = 8;
            colD2.Name = "colD2";
            colD2.Width = 150;
            // 
            // colD3
            // 
            colD3.HeaderText = "Sao";
            colD3.MinimumWidth = 8;
            colD3.Name = "colD3";
            colD3.Width = 136;
            // 
            // colD4
            // 
            colD4.HeaderText = "Điểm cao nhất";
            colD4.MinimumWidth = 8;
            colD4.Name = "colD4";
            colD4.Width = 140;
            // 
            // colD5
            // 
            colD5.HeaderText = "Hoàn thành";
            colD5.MinimumWidth = 8;
            colD5.Name = "colD5";
            colD5.Width = 150;
            // 
            // colD6
            // 
            colD6.HeaderText = "Ngày";
            colD6.MinimumWidth = 8;
            colD6.Name = "colD6";
            colD6.Width = 200;
            // 
            // frmAdminPlayers
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(958, 872);
            Controls.Add(pnlReset);
            Controls.Add(dgvDetail);
            Controls.Add(btnTabHistory);
            Controls.Add(btnTabProgress);
            Controls.Add(lblSelectedPlayer);
            Controls.Add(dgvPlayers);
            Controls.Add(pnlStat2);
            Controls.Add(pnlStat3);
            Controls.Add(pnlStat4);
            Controls.Add(pnlStat1);
            Controls.Add(lblTitle);
            Name = "frmAdminPlayers";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += frmAdminPlayers_Load;
            pnlStat1.ResumeLayout(false);
            pnlStat1.PerformLayout();
            pnlStat2.ResumeLayout(false);
            pnlStat2.PerformLayout();
            pnlStat3.ResumeLayout(false);
            pnlStat3.PerformLayout();
            pnlStat4.ResumeLayout(false);
            pnlStat4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPlayers).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetail).EndInit();
            pnlReset.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Panel pnlStat1;
        private Label lblTotalPlayers;
        private Label lblStat1Title;
        private Panel pnlStat2;
        private Label lblTotalGames;
        private Label lblStat2Title;
        private Panel pnlStat3;
        private Label lblTopScore;
        private Label lblStat3Title;
        private Panel pnlStat4;
        private Label lblTopStreak;
        private Label lblStat4Title;
        private DataGridView dgvPlayers;
        private Button btnBack;
        private Label lblSelectedPlayer;
        private Button btnTabProgress;
        private Button btnTabHistory;
        private DataGridView dgvDetail;
        private Panel pnlReset;
        private Button btnResetAll;
        private Button btnResetSH;
        private Button btnResetPP;
        private DataGridViewTextBoxColumn colPlayerID;
        private DataGridViewTextBoxColumn colUsername;
        private DataGridViewTextBoxColumn colTotalScore;
        private DataGridViewTextBoxColumn colTotalStars;
        private DataGridViewTextBoxColumn colDayStreak;
        private DataGridViewTextBoxColumn colLastPlayed;
        private DataGridViewTextBoxColumn colCreatedAt;
        private DataGridViewTextBoxColumn colXoaNguoiChoi;
        private DataGridViewTextBoxColumn colD1;
        private DataGridViewTextBoxColumn colD2;
        private DataGridViewTextBoxColumn colD3;
        private DataGridViewTextBoxColumn colD4;
        private DataGridViewTextBoxColumn colD5;
        private DataGridViewTextBoxColumn colD6;
    }
}
