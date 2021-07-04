using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReportingWithCsharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnKarbar_Click(object sender, EventArgs e)
        {
            new frmKarbar().ShowDialog();
        }

        private void btnStud_Click(object sender, EventArgs e)
        {
            new frmStu().ShowDialog();
        }

        private void btnListStud_Click(object sender, EventArgs e)
        {
            new frmListStud().ShowDialog();
        }
    }
}
