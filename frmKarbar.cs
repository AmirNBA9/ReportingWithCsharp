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
    public partial class frmKarbar : Form
    {
        public frmKarbar()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source = .\\MSSQLSERVER2019; initial catalog = ReportDB;integrated security=true");
        SqlCommand cmd = new SqlCommand();

        void Display()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "Select * from Karbar";
            adp.Fill(ds, "Karbar");
            dgvKarbar.DataSource = ds;
            dgvKarbar.DataMember = "Karbar";
            //**********
            dgvKarbar.Columns[0].HeaderText = "کد";
            dgvKarbar.Columns[1].HeaderText = "نام کاربری";
            dgvKarbar.Columns[2].HeaderText = "پسورد";
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                /*Find key*/
                int x = Convert.ToInt32(dgvKarbar.SelectedCells[0].Value);
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "Delete From Karbar Where id = @N";
                cmd.Parameters.AddWithValue("@N", x);
                con.Open();
                Display();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("حذف کاربر انجام شد");
            }
            catch (Exception)
            {

                MessageBox.Show("مشکلی پیش آمده است");
            }
        }

        private void frmKarbar_Load(object sender, EventArgs e)
        {
            Display();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "insert into Karbar(Uname,Password)values(@a,@b)";
                cmd.Parameters.AddWithValue("@a",txtUName.Text);
                cmd.Parameters.AddWithValue("@b",txtPassword.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Display();
                MessageBox.Show("ثبت کاربر انجام شد");
                //****
                txtPassword.Text = "";
                txtUName.Text = "";
            }
            catch (Exception)
            {

                MessageBox.Show("مشکلی بوجود آماده است");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "Update Karbar set UName='" + txtUName.Text + "',Password='" + txtPassword.Text + "' where id=" + Convert.ToInt32(dgvKarbar.SelectedCells[0].Value);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                Display();
                MessageBox.Show("ویرایش کاربر انجام شد");
                //******************************
                txtPassword.Text = "";
                txtUName.Text = "";
            }
            catch (Exception)
            {
                MessageBox.Show("مشکلی پیش آمده است");
            }
        }

        private void dgvKarbar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvKarbar_MouseUp(object sender, MouseEventArgs e)
        {
            txtUName.Text = dgvKarbar[1, dgvKarbar.CurrentRow.Index].Value.ToString();
            txtPassword.Text = dgvKarbar[2, dgvKarbar.CurrentRow.Index].Value.ToString();
        }
    }
}
