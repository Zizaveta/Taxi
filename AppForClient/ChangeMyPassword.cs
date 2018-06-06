using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using BLL;
namespace AppForClient
{
    public partial class ChangeMyPassword : Form
    {
        public ChangeMyPassword()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (Form1.PersonWork.ChangePassword(textBox1.Text) == true)
                    this.Close();
                else
                {
                    MessageBox.Show("Password must be longer 8 symvols");
                }
            }
            catch { }
        }
    }
}
