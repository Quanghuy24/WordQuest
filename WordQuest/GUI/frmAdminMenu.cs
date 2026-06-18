using System;
using System.Windows.Forms;

namespace WordQuest.GUI
{
    public partial class frmAdminMenu : Form
    {
        private readonly int _playerID;
        private readonly string _username;
        private readonly Form _topics;

        public frmAdminMenu(int playerID, string username, Form topics)
        {
            InitializeComponent();
            _playerID = playerID;
            _username = username;
            _topics = topics;
        }

        private void btnCauHoi_Click(object sender, EventArgs e)
        {
            var admin = new frmAdmin(_topics);
            admin.ShowDialog();
        }

        private void btnNguoiChoi_Click(object sender, EventArgs e)
        {
            var admin = new frmAdminPlayers(_topics);
            admin.ShowDialog();
        }

        private void btnQuyTac_Click(object sender, EventArgs e)
        {
            var admin = new frmAdminRules(_topics);
            admin.ShowDialog();
        }

        private void btnTopics_Click(object sender, EventArgs e)
        {
            var admin = new frmAdminTopics(_topics);
            admin.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
