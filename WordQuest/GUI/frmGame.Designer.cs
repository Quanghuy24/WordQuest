namespace WordQuest.GUI
{
    partial class frmGame
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
            components = new System.ComponentModel.Container();
            btnExit = new Button();
            lblTopic = new Label();
            pnlHeader = new Panel();
            lblStreak = new Label();
            lblLives = new Label();
            lblQuestion = new Label();
            pbTime = new ProgressBar();
            lblTimer = new Label();
            picWord = new PictureBox();
            pnlànswer = new Panel();
            pnlLetters = new Panel();
            btnHint = new Button();
            gameTimer = new System.Windows.Forms.Timer(components);
            lblEnglish = new Label();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picWord).BeginInit();
            SuspendLayout();
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.FromArgb(255, 128, 0);
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Showcard Gothic", 11F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnExit.ForeColor = Color.White;
            btnExit.Location = new Point(27, 19);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(146, 56);
            btnExit.TabIndex = 1;
            btnExit.Text = "Quay lại";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += BtnExit_Click;
            // 
            // lblTopic
            // 
            lblTopic.AutoSize = true;
            lblTopic.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTopic.ForeColor = Color.Yellow;
            lblTopic.Location = new Point(362, 30);
            lblTopic.Name = "lblTopic";
            lblTopic.Size = new Size(204, 32);
            lblTopic.TabIndex = 2;
            lblTopic.Text = "Động vật - Màn 1";
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.Transparent;
            pnlHeader.BackgroundImageLayout = ImageLayout.Stretch;
            pnlHeader.Controls.Add(lblStreak);
            pnlHeader.Controls.Add(lblLives);
            pnlHeader.Controls.Add(lblTopic);
            pnlHeader.Controls.Add(btnExit);
            pnlHeader.Location = new Point(4, 4);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(953, 105);
            pnlHeader.TabIndex = 3;
            // 
            // lblStreak
            // 
            lblStreak.AutoSize = true;
            lblStreak.ForeColor = Color.FromArgb(255, 128, 0);
            lblStreak.Location = new Point(853, 30);
            lblStreak.Name = "lblStreak";
            lblStreak.Size = new Size(58, 25);
            lblStreak.TabIndex = 4;
            lblStreak.Text = "Điểm:";
            // 
            // lblLives
            // 
            lblLives.AutoSize = true;
            lblLives.ForeColor = Color.Red;
            lblLives.Location = new Point(698, 30);
            lblLives.Name = "lblLives";
            lblLives.Size = new Size(87, 25);
            lblLives.TabIndex = 3;
            lblLives.Text = "❤️❤️❤️";
            // 
            // lblQuestion
            // 
            lblQuestion.AutoSize = true;
            lblQuestion.ForeColor = Color.Yellow;
            lblQuestion.Location = new Point(80, 155);
            lblQuestion.Name = "lblQuestion";
            lblQuestion.Size = new Size(84, 25);
            lblQuestion.TabIndex = 4;
            lblQuestion.Text = "Câu 1/15";
            // 
            // pbTime
            // 
            pbTime.BackColor = SystemColors.ActiveCaption;
            pbTime.ForeColor = SystemColors.ControlText;
            pbTime.Location = new Point(527, 146);
            pbTime.Name = "pbTime";
            pbTime.Size = new Size(219, 34);
            pbTime.TabIndex = 5;
            // 
            // lblTimer
            // 
            lblTimer.AutoSize = true;
            lblTimer.ForeColor = Color.White;
            lblTimer.Location = new Point(765, 155);
            lblTimer.Name = "lblTimer";
            lblTimer.Size = new Size(40, 25);
            lblTimer.TabIndex = 6;
            lblTimer.Text = "60s";
            // 
            // picWord
            // 
            picWord.BackColor = Color.Transparent;
            picWord.Location = new Point(235, 207);
            picWord.Name = "picWord";
            picWord.Size = new Size(484, 331);
            picWord.SizeMode = PictureBoxSizeMode.StretchImage;
            picWord.TabIndex = 7;
            picWord.TabStop = false;
            // 
            // pnlànswer
            // 
            pnlànswer.BackColor = Color.Transparent;
            pnlànswer.Location = new Point(109, 606);
            pnlànswer.Name = "pnlànswer";
            pnlànswer.Size = new Size(752, 55);
            pnlànswer.TabIndex = 8;
            // 
            // pnlLetters
            // 
            pnlLetters.BackColor = Color.Transparent;
            pnlLetters.Location = new Point(112, 685);
            pnlLetters.Name = "pnlLetters";
            pnlLetters.Size = new Size(749, 68);
            pnlLetters.TabIndex = 9;
            // 
            // btnHint
            // 
            btnHint.BackColor = Color.FromArgb(255, 128, 0);
            btnHint.FlatAppearance.BorderSize = 0;
            btnHint.FlatStyle = FlatStyle.Flat;
            btnHint.ForeColor = Color.White;
            btnHint.Location = new Point(244, 150);
            btnHint.Name = "btnHint";
            btnHint.Size = new Size(112, 34);
            btnHint.TabIndex = 10;
            btnHint.Text = "Gợi ý";
            btnHint.UseVisualStyleBackColor = false;
            btnHint.Click += BtnHint_Click;
            // 
            // gameTimer
            // 
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;
            // 
            // lblEnglish
            // 
            lblEnglish.BackColor = Color.Transparent;
            lblEnglish.ForeColor = Color.FromArgb(150, 255, 150);
            lblEnglish.Location = new Point(401, 550);
            lblEnglish.Name = "lblEnglish";
            lblEnglish.Size = new Size(219, 44);
            lblEnglish.TabIndex = 11;
            lblEnglish.Text = "Phiên âm và nghĩa";
            lblEnglish.Visible = false;
            // 
            // frmGame
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(15, 12, 41);
            ClientSize = new Size(958, 774);
            Controls.Add(lblEnglish);
            Controls.Add(btnHint);
            Controls.Add(pnlLetters);
            Controls.Add(pnlànswer);
            Controls.Add(picWord);
            Controls.Add(lblTimer);
            Controls.Add(pbTime);
            Controls.Add(lblQuestion);
            Controls.Add(pnlHeader);
            Name = "frmGame";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WordQuest";
            Load += frmGame_Load;
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picWord).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnExit;
        private Label lblTopic;
        private Panel pnlHeader;
        private Label lblStreak;
        private Label lblLives;
        private Label lblQuestion;
        private ProgressBar pbTime;
        private Label lblTimer;
        private PictureBox picWord;
        private Panel pnlànswer;
        private Panel pnlLetters;
        private Button btnHint;
        private System.Windows.Forms.Timer gameTimer;
        private Label lblEnglish;
    }
}
