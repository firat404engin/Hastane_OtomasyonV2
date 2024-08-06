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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Hastane_OtomasyonV2
{
    public partial class FrmHastaGiris : Form
    {
        public FrmHastaGiris()
        {
            InitializeComponent();
            TxtSifre.PasswordChar = '*';


            MskTc.TabIndex = 0;
            TxtSifre.TabIndex = 1;
            checkBox1.TabIndex = 2;
            BtnGiris.TabIndex = 3;
            button1.TabIndex = 4;

            this.AcceptButton = BtnGiris;
        }
        sqlbaglanti bglgiris = new sqlbaglanti();
        private void BtnGiris_Click(object sender, EventArgs e)
        {

            if (MskTc.Text == "" || TxtSifre.Text == "" || checkBox1.Checked == false)
            {
                MessageBox.Show("Boş kalan alanları lütfen doldurunuz .", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand komutgiris = new SqlCommand("Select * From Tbl_Hastalar where Hastatc=@p1 and Hastasifre=@p2", bglgiris.baglanti());
                komutgiris.Parameters.AddWithValue("@p1", MskTc.Text);
                komutgiris.Parameters.AddWithValue("@p2", TxtSifre.Text);
                SqlDataReader dr = komutgiris.ExecuteReader();
                if (dr.Read())
                {
                    FrmHastaDetay frmhasta = new FrmHastaDetay();
                    frmhasta.hastatc = MskTc.Text;
                    frmhasta.Show();
                    this.Hide();
                    
                }
                else
                {
                    MessageBox.Show("TC Kimlik No veya Şifre hatalı lütfen tekrar deneyiniz .", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                bglgiris.baglanti().Close();
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmHastaKayıt frmkayıt = new FrmHastaKayıt();
            frmkayıt.Show();
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
        bool passwordVisible = false;
        private void button2_Click(object sender, EventArgs e)
        {
            // Eğer şifre gizliyse, metni göster.
            if (!passwordVisible)
            {
                TxtSifre.PasswordChar = '\0'; // Şifreyi görünür hale getir
                button2.Image = Image.FromFile(@"C:\Users\Furka\Desktop\hastaneotomasyonV3\Hastane_OtomasyonV2\Hastane_OtomasyonV2\Resimler\gizligöz1.2.png");
            }
            else // Eğer şifre gösteriliyorsa, metni gizle.
            {
                TxtSifre.PasswordChar = '*'; // Şifreyi gizle
                button2.Image = Image.FromFile(@"C:\Users\Furka\Desktop\hastaneotomasyonV3\Hastane_OtomasyonV2\Hastane_OtomasyonV2\Resimler\göz1.2.png");
            }

            // Durumu tersine çevir
            passwordVisible = !passwordVisible;
        }
    }
}
