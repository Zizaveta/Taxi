using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
namespace AppForClient
{
    public partial class Form1 : Form
    {
        public static WorkPerson PersonWork;
        public Form1()
        {
            InitializeComponent();
            PersonWork = new WorkPerson();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Enter_Click(object sender, EventArgs e)
        {
            if (PersonWork.Authoriz(mail.Text, pass.Text) == true)
            {
                MainClientWindow w = new MainClientWindow();
                w.ShowDialog();
            }
            else
                MessageBox.Show("Wrong email or password");
        }

        private void Registr_Click(object sender, EventArgs e)
        {
            Regist w = new Regist();
            w.ShowDialog();
        }

        private void ForgetPass_Click(object sender, EventArgs e)
        {
            if (mail.Text.Length == 0)
                MessageBox.Show("Input email");
            MessageBox.Show(PersonWork.ForgetPassword(mail.Text));
        }
    }
}
