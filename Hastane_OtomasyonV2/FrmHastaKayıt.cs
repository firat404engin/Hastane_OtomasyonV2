using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hastane_OtomasyonV2
{
    public partial class FrmHastaKayıt : Form
    {
        public FrmHastaKayıt()
        {
            InitializeComponent();
            TxtSifre.PasswordChar = '*';
        }
        sqlbaglanti bgl = new sqlbaglanti();

        void temizle()
        {
            TxtAd.Text = "";
            TxtSoyad.Text = "";
            MskTc.Text = "";
            maskedTextBox1.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            TxtSifre.Text = "";
            checkBox1.Checked = false;
        }

        private void BtnKayıt_Click(object sender, EventArgs e)
        {
            if (TxtAd.Text == "" || TxtSoyad.Text == "" || MskTc.Text == "" || maskedTextBox1.Text == "" || TxtSifre.Text == "" || checkBox1.Checked == false)
            {
                MessageBox.Show("Boş kalan alanları lütfen doldurunuz .", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand komutkayıt = new SqlCommand("insert into Tbl_Hastalar (Hastaad,Hastasoyad,Hastatc,Hastatel,Hastacinsiyet,Hastasifre) values (@e1,@e2,@e3,@e4,@e5,@e6)", bgl.baglanti());
                komutkayıt.Parameters.AddWithValue("@e1", TxtAd.Text);
                komutkayıt.Parameters.AddWithValue("@e2", TxtSoyad.Text);
                komutkayıt.Parameters.AddWithValue("@e3", MskTc.Text);
                komutkayıt.Parameters.AddWithValue("@e4", maskedTextBox1.Text);
                komutkayıt.Parameters.AddWithValue("@e5", label8.Text);
                komutkayıt.Parameters.AddWithValue("@e6", TxtSifre.Text);
                komutkayıt.ExecuteNonQuery();
                bgl.baglanti().Close();
                if (label8.Text == "True")
                {
                    MessageBox.Show("Kayıt yapılmıştır " + TxtAd.Text + " Bey", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Kayıt yapılmıştır " + TxtAd.Text + " Hanım", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                temizle();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                label8.Text = "False";
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                label8.Text = "True";
            }
        }
        private void FrmHastaKayıt_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }
        bool passwordVisible = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (!passwordVisible)
            {
                TxtSifre.PasswordChar = '\0';
                button1.Image = Image.FromFile(@"C:\Users\Furka\Desktop\sifreSimge\gizligöz1.2.png");
            }
            else
            {
                TxtSifre.PasswordChar = '*';
                button1.Image = Image.FromFile(@"C:\Users\Furka\Desktop\sifreSimge\göz1.2.png");
            }


            passwordVisible = !passwordVisible;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) // Eğer checkbox işaretlendiğinde
            {
                // Doğrulama ekranını aç
                Form1 frmCaptcha = new Form1();
                DialogResult result = frmCaptcha.ShowDialog();

                // Doğrulama yapıldıysa
                if (frmCaptcha.dogrulamaYapildi == true)
                {


                    checkBox1.Checked = true;

                }
                else // Doğrulama yapılmadıysa
                {
                    MessageBox.Show("Doğrulama başarısız!");
                    // Doğrulama işlemi başarılı olduğunda checkBox1'i işaretleyin
                    checkBox1.Checked = false;
                }
            }
            else // Eğer checkbox işareti kaldırıldığında
            {
                // CheckBox'u işaretsiz yap
                checkBox1.Checked = false;
                // Doğrulama yapıldı bayrağını sıfırla

            }
        }
    }
}
