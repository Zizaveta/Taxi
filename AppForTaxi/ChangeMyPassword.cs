using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppForTaxi
{
    public partial class ChangeMyPassword : Form
    {
        public ChangeMyPassword()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool isPasswordChange = Form1.WorkTaxi.ChangePassword(textBox1.Text);
            if (isPasswordChange == true)
                this.Close();
            else MessageBox.Show("Password must be longer 8 symvols");
        }
    }
}
