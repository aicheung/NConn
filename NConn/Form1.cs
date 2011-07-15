using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace NConn
{
    public partial class Form1 : Form
    {
        private bool validIP, validPort;

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textPort_Validating(object sender, CancelEventArgs e)
        {
            string err = null;
            validPort = true;

            int n;
            bool valid = int.TryParse(textPort.Text, out n);

            if (!valid)
            {
                err = "Please enter a valid number.";
                validPort = false;
            }

            errorProvider1.SetError((Control)sender, err);
        }

        private void textIP_Validating(object sender, CancelEventArgs e)
        {
            string err = null;
            validIP = true;

            string pattern = @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}";
            bool valid = Regex.IsMatch(textIP.Text, pattern);

            if (!valid)
            {
                err = "Please enter a valid IP.";
                validIP = false;
            }

            errorProvider1.SetError((Control)sender, err);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!validIP || !validPort)
                return;


        }
    }
}
