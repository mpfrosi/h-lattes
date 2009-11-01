using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Common;

namespace HLattes
{
    public partial class FrmConfig : Form
    {
        public FrmConfig()
        {            
            InitializeComponent();
            tbxTimeout.Text = MyConstants.SchloarTimeout.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MyConstants.SchloarTimeout = Convert.ToInt32(tbxTimeout.Text);
            this.Close();
        }
    }
}