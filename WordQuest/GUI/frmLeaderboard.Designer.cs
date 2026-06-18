using System.Drawing;
using System.Windows.Forms;

namespace WordQuest.GUI
{
    partial class frmLeaderboard : Form
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            lblTitle = new Label();
            dgvScore = new DataGridView();
            colHang = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colScore = new DataGridViewTextBoxColumn();
            colStar = new DataGridViewTextBoxColumn();
            btnBack = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvScore).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Showcard Gothic", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = Color.FromArgb(255, 220, 80);
            lblTitle.Location = new Point(304, 82);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(337, 50);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "BẢNG XẾP HẠNG";
            // 
            // dgvScore
            // 
            dgvScore.AllowUserToAddRows = false;
            dgvScore.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(100, 65, 30);
            dataGridViewCellStyle1.SelectionBackColor = Color.DarkOrange;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dgvScore.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvScore.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvScore.BackgroundColor = Color.FromArgb(120, 80, 40);
            dgvScore.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(75, 45, 15);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(255, 220, 80);
            dataGridViewCellStyle2.SelectionBackColor = Color.DarkOrange;
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvScore.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvScore.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvScore.Columns.AddRange(new DataGridViewColumn[] { colHang, colName, colScore, colStar });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(120, 80, 40);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvScore.DefaultCellStyle = dataGridViewCellStyle3;
            dgvScore.EnableHeadersVisualStyles = false;
            dgvScore.GridColor = Color.FromArgb(150, 100, 50);
            dgvScore.Location = new Point(41, 167);
            dgvScore.Name = "dgvScore";
            dgvScore.ReadOnly = true;
            dgvScore.RowHeadersVisible = false;
            dgvScore.RowHeadersWidth = 40;
            dgvScore.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvScore.Size = new Size(878, 582);
            dgvScore.TabIndex = 2;
            // 
            // colHang
            // 
            colHang.FillWeight = 50F;
            colHang.HeaderText = "Hạng";
            colHang.MinimumWidth = 8;
            colHang.Name = "colHang";
            colHang.ReadOnly = true;
            // 
            // colName
            // 
            colName.FillWeight = 150F;
            colName.HeaderText = "Tên người chơi";
            colName.MinimumWidth = 8;
            colName.Name = "colName";
            colName.ReadOnly = true;
            // 
            // colScore
            // 
            colScore.HeaderText = "Tổng điểm";
            colScore.MinimumWidth = 8;
            colScore.Name = "colScore";
            colScore.ReadOnly = true;
            // 
            // colStar
            // 
            colStar.FillWeight = 80F;
            colStar.HeaderText = "Tổng sao";
            colStar.MinimumWidth = 8;
            colStar.Name = "colStar";
            colStar.ReadOnly = true;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.FromArgb(192, 64, 0);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Showcard Gothic", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnBack.ForeColor = Color.FromArgb(255, 220, 80);
            btnBack.Location = new Point(55, 31);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(146, 55);
            btnBack.TabIndex = 3;
            btnBack.Text = "QUAY LẠI";
            btnBack.UseVisualStyleBackColor = false;
            // 
            // frmLeaderboard
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(101, 67, 33);
            ClientSize = new Size(958, 774);
            Controls.Add(btnBack);
            Controls.Add(dgvScore);
            Controls.Add(lblTitle);
            Name = "frmLeaderboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WordQuest - Bảng xếp hạng";
            Load += frmLeaderboard_Load;
            ((System.ComponentModel.ISupportInitialize)dgvScore).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private DataGridView dgvScore;
        private Button btnBack;
        private DataGridViewTextBoxColumn colHang;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colScore;
        private DataGridViewTextBoxColumn colStar;
    }
}
