using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace ReportingWithCsharp
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }


        SqlConnection con = new SqlConnection("Data Source = .\\MSSQLSERVER2019; initial catalog = ReportDB;integrated security=true");
        SqlCommand cmd = new SqlCommand();
        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            int i = 0;
            cmd = new SqlCommand("select count(1) from Karbar where UName = @N AND Password=@F", con);
            cmd.Parameters.AddWithValue("@N", txtUName.Text);
            cmd.Parameters.AddWithValue("@F", txtPassword.Text);
            con.Open();
            i = (int)cmd.ExecuteScalar();
            con.Close();
            if (i>0)
            {
                new Form1().ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("نام کاربری یا کلمه عبور صحیح نیست!");
            }
        }
    }
}
