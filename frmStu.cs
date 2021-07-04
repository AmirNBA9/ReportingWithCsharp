using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace ReportingWithCsharp
{
    public partial class frmStu : Form
    {
        public frmStu()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source = .\\MSSQLSERVER2019; initial catalog = ReportDB;integrated security=true");
        
        SqlCommand cmd = new SqlCommand();

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxX7_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "All Pictures(*.*)|*.jpg;*.bmp;*.png;*.gif";
                op.ShowDialog();
                pictureBox1.ImageLocation = op.FileName;
            }
            catch (Exception)
            {

                MessageBox.Show("مشکلی بوجود آمده است!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox1.Image == null)
                {
                    MessageBox.Show("تصویری انتخاب نشده است");
                    return;
                }
                con.Open();
                byte[] ar = File.ReadAllBytes(pictureBox1.ImageLocation);
                SqlCommand cmd = new SqlCommand("insert into Stud (NameS,NameKh,NameP,Tavalod,CodeM,Tel,Payeh,Moadel,Tarikh,Pic)values(@a,@b,@c,@d,@e,@f,@g,@h,@i,@Pic)", con);
                cmd.Parameters.AddWithValue("@a", txtName.Text);
                cmd.Parameters.AddWithValue("@b", txtNameKh.Text);
                cmd.Parameters.AddWithValue("@c", txtNameP.Text);
                cmd.Parameters.AddWithValue("@d", mskTavalod.Text);
                cmd.Parameters.AddWithValue("@e", txtCodeM.Text);
                cmd.Parameters.AddWithValue("@f", txtTel.Text);
                cmd.Parameters.AddWithValue("@g", txtPayeh.Text);
                cmd.Parameters.AddWithValue("@h", txtMoadel.Text);
                cmd.Parameters.AddWithValue("@i", mskTarikh.Text);
                cmd.Parameters.AddWithValue("@Pic",SqlDbType.VarBinary).Value = ar;
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("ثبت با موفقیت انجام شد");
                //*****
                txtCode.Text = "";
                txtName.Text = "";
                txtNameKh.Text = "";
                txtNameP.Text = "";
                mskTavalod.Text = "";
                txtCodeM.Text = "";
                txtTel.Text = "";
                txtPayeh.Text = "";
                txtMoadel.Text = "";
                mskTarikh.Text = "";
                pictureBox1.Image = null;
            }   
            catch (Exception)
            {

                MessageBox.Show("مشکلی بوجود آمده است!");
            }
        }

        private void frmStu_Load(object sender, EventArgs e)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            mskTarikh.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");
            mskTavalod.Text = p.GetYear(DateTime.Now).ToString() + p.GetMonth(DateTime.Now).ToString("0#") + p.GetDayOfMonth(DateTime.Now).ToString("0#");
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                cmd.Parameters.Clear();
                cmd.Connection = con;
                cmd.CommandText = "Delete from Stud Where id = " + txtCode.Text;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("حذف انجام شد.");
            }
            catch (Exception)
            {

                MessageBox.Show("مشکلی بوجود آمده است!");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            try
            {
                pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            }
            catch (Exception)
            {
            }
            byte[] arpic = ms.GetBuffer();
            ms.Close();
            cmd.Connection = con;
            cmd.Parameters.Clear();
            string UpdateStud = "update Stud set NameS='" + txtName.Text + "',NameKh='" + txtNameKh.Text + "',NameP='" + txtNameP.Text + "',Tavalod='" + mskTavalod.Text + "',CodeM='" + txtCodeM.Text + "',Tel='" + txtTel.Text + "',Payeh='" + txtPayeh.Text + "',Moadel='" + txtMoadel.Text + "',Tarikh='" + mskTarikh.Text + "',Pic=@Pic where id=" + txtCode.Text;
            SqlCommand com = new SqlCommand(UpdateStud, con);
            com.Parameters.AddWithValue("@Pic", arpic);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("بروزرسانی انجام شد");
        }

        Image Imagebyte(byte[] bytes)
        {
            System.IO.MemoryStream m = new MemoryStream(bytes);
            return Image.FromStream(m);
        }
        private void buttonX2_Click(object sender, EventArgs e)
        {
            SqlDataReader dr;
            cmd = new SqlCommand();
            cmd.Parameters.Clear();
            cmd.Connection = con;
            cmd.CommandText = "select * from stud where id = @S";
            cmd.Parameters.AddWithValue("@S",txtCode.Text);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtCode.Text = dr["id"].ToString();
                txtName.Text = dr["NameS"].ToString();
                txtNameKh.Text = dr["NameKh"].ToString();
                txtNameP.Text = dr["NameP"].ToString();
                mskTavalod.Text = dr["Tavalod"].ToString();
                txtCodeM.Text = dr["CodeM"].ToString();
                txtTel.Text = dr["Tel"].ToString();
                txtPayeh.Text = dr["Payeh"].ToString();
                txtMoadel.Text = dr["Moadel"].ToString();
                mskTarikh.Text = dr["Tarikh"].ToString();
                pictureBox1.Image = Imagebyte((byte[])dr[10]);
            }
            else
            {
                MessageBox.Show("برای کد وارد شده مقداری یافت نشد");
                txtCode.Text = "";
                txtCode.Focus();
            }
            con.Close();
        }
    }
}
