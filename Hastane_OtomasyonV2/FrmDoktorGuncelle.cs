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
    public partial class FrmDoktorGuncelle : Form
    {
        public FrmDoktorGuncelle()
        {
            InitializeComponent();
            TxtSifre.PasswordChar = '*';
        }
        sqlbaglanti bgl = new sqlbaglanti();

        public string doktortc;

        private void FrmDoktorGuncelle_Load_1(object sender, EventArgs e)
        {
            MskTc.Text = doktortc;

            // bilgileri çekme
            SqlCommand komut = new SqlCommand("Select * From Tbl_Doktorlar where Doktortc=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", MskTc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                TxtAd.Text = dr[1].ToString();
                TxtSoyad.Text = dr[2].ToString();
                CmbBrans.Text = dr[3].ToString();
                TxtSifre.Text = dr[5].ToString();
            }
            bgl.baglanti().Close();

            // combobox a veri çekme
            SqlCommand komut2 = new SqlCommand("Select Bransad From Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0].ToString());
            }
            bgl.baglanti().Close();
        }
        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Sayın " + TxtAd.Text + " " + TxtSoyad.Text + " bilgilerinizi güncellensin mi ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (secenek == DialogResult.Yes)
            {
                SqlCommand komutguncelle = new SqlCommand("Update Tbl_Doktorlar set Doktorbrans=@g1,Doktorsifre=@g2 where Doktortc=@g3", bgl.baglanti());
                komutguncelle.Parameters.AddWithValue("@g1", CmbBrans.Text);
                komutguncelle.Parameters.AddWithValue("@g2", TxtSifre.Text);
                komutguncelle.Parameters.AddWithValue("@g3", MskTc.Text);
                komutguncelle.ExecuteNonQuery();
                bgl.baglanti().Close();

                SqlCommand komutguncellebrans = new SqlCommand("Update Tbl_Randevular set Randevubrans=@p1 where Randevudoktor=@p2", bgl.baglanti());
                komutguncellebrans.Parameters.AddWithValue("@p1", CmbBrans.Text);
                komutguncellebrans.Parameters.AddWithValue("@p2", TxtAd.Text + " " + TxtSoyad.Text);
                komutguncellebrans.ExecuteNonQuery();
                bgl.baglanti().Close();
                FrmDoktorDetay frm = new FrmDoktorDetay();
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
    }
}
