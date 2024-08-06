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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Hastane_OtomasyonV2
{
    public partial class FrmDoktorGiris : Form
    {
        public FrmDoktorGiris()
        {
            InitializeComponent();
            TxtSifre.PasswordChar = '*';

            MskTc.TabIndex = 0;
            TxtSifre.TabIndex = 1;
            checkBox1.TabIndex = 2;
            BtnGiris.TabIndex = 3;

            this.AcceptButton = BtnGiris;
           

        }
        sqlbaglanti bgldoktor = new sqlbaglanti();

        private void BtnGiris_Click(object sender, EventArgs e)
        {
            if (MskTc.Text == "" || TxtSifre.Text == "" || checkBox1.Checked == false)
            {
                MessageBox.Show("Boş alanları lütfen doldurunuz .", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand komut = new SqlCommand("Select * From Tbl_Doktorlar where Doktortc=@p1 and Doktorsifre=@p2", bgldoktor.baglanti());
                komut.Parameters.AddWithValue("@p1", MskTc.Text);
                komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
                SqlDataReader dr = komut.ExecuteReader();
                if (dr.Read())
                {
                    FrmDoktorDetay frmdoktor = new FrmDoktorDetay();
                    frmdoktor.doktortc = MskTc.Text;
                    frmdoktor.Show();
                    this.Hide();


                }
                else
                {
                    MessageBox.Show("Tc Kimlik No veya şifre hatalı lütfen tekrar deneyiniz .", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                bgldoktor.baglanti().Close();
            }

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





