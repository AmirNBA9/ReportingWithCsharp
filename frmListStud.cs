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
    public partial class frmListStud : Form
    {
        public frmListStud()
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
            adp.SelectCommand.CommandText = "Select * from Stud";
            adp.Fill(ds, "Stud");
            dgvStud.DataSource = ds;
            dgvStud.DataMember = "Stud";
            //*******************************
            dgvStud.Columns[0].HeaderText = "کد";
            dgvStud.Columns[1].HeaderText = "نام";
            dgvStud.Columns[2].HeaderText = "نام خانوادگی";
            dgvStud.Columns[3].HeaderText = "نام پدر";
            dgvStud.Columns[4].HeaderText = "تاریخ تولد";
            dgvStud.Columns[5].HeaderText = "کد ملی";
            dgvStud.Columns[6].HeaderText = "تلفن";
            dgvStud.Columns[7].HeaderText = "پایه تحصیلی";
            dgvStud.Columns[8].HeaderText = "معدل";
            dgvStud.Columns[9].HeaderText = "تاریخ ثبت نام";
            dgvStud.Columns[10].HeaderText = "تصویر دانش آموز";

        }

        void DisplayTarikh()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "Select * from Stud where Tarikh Between '" + mskTarikh1.Text + "' AND '" + mskTarikh2.Text + "'";
            adp.Fill(ds, "Stud");
            dgvStud.DataSource = ds;
            dgvStud.DataMember = "Stud";
            //*******************************
            dgvStud.Columns[0].HeaderText = "کد";
            dgvStud.Columns[1].HeaderText = "نام";
            dgvStud.Columns[2].HeaderText = "نام خانوادگی";
            dgvStud.Columns[3].HeaderText = "نام پدر";
            dgvStud.Columns[4].HeaderText = "تاریخ تولد";
            dgvStud.Columns[5].HeaderText = "کد ملی";
            dgvStud.Columns[6].HeaderText = "تلفن";
            dgvStud.Columns[7].HeaderText = "پایه تحصیلی";
            dgvStud.Columns[8].HeaderText = "معدل";
            dgvStud.Columns[9].HeaderText = "تاریخ ثبت نام";
            dgvStud.Columns[10].HeaderText = "تصویر دانش آموز";

        }
        private void frmListStud_Load(object sender, EventArgs e)
        {
            Display();
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            mskTarikh1.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");
            mskTarikh2.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");
        }

        private void txtPayeh_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "Select * from Stud where NameKh like '%' + @S + '%'";
            adp.SelectCommand.Parameters.AddWithValue("@S", txtNameKh.Text + "%");
            adp.Fill(ds, "Stud");
            dgvStud.DataSource = ds;
            dgvStud.DataMember = "Stud";
        }

        private void txtNameKh_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter adp = new SqlDataAdapter();
            adp.SelectCommand = new SqlCommand();
            adp.SelectCommand.Connection = con;
            adp.SelectCommand.CommandText = "Select * from Stud where Payeh like '%' + @S + '%'";
            adp.SelectCommand.Parameters.AddWithValue("@S", txtPayeh.Text + "%");
            adp.Fill(ds, "Stud");
            dgvStud.DataSource = ds;
            dgvStud.DataMember = "Stud";
        }
        private void mskTarikh1_TextChanged(object sender, EventArgs e)
        {
            DisplayTarikh();
        }

        private void mskTarikh2_TextChanged(object sender, EventArgs e)
        {
            DisplayTarikh();
        }
    }
}
