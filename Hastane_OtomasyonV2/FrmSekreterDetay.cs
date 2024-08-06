using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hastane_OtomasyonV2
{
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        sqlbaglanti bgl = new sqlbaglanti();

        public string sekretertc;

        void temizle()
        {
            CmbBrans.Text = "";
            CmbDoktor.Text = "";
        }

        private void FrmSekreterDetay_Load_1(object sender, EventArgs e)
        {
            LblTc.Text = sekretertc;

            // ad soyad çekme
            SqlCommand komut = new SqlCommand("Select Sekreteradsoyad From Tbl_Sekreterler where Sekretertc=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", LblTc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdsoyad.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();

            // branşları datagride aktarma
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select * From Tbl_Branslar", bgl.baglanti());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;

            // doktorları datagride aktarma
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select (Doktorad + ' ' + Doktorsoyad) as 'Doktorlar',Doktorbrans From Tbl_Doktorlar", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

          

            // branşları comboboxa aktarma
            SqlCommand komut2 = new SqlCommand("Select Bransad From Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0].ToString());
            }
            bgl.baglanti().Close();
            CmbDoktor.Text = "";
            CmbDoktor.Items.Clear();

        }
        private void BtnKaydet_Click_1(object sender, EventArgs e)
        {

            DateTime secilenTarih = tarih1.Value;
            string tarih = secilenTarih.ToString("dd.MM.yyyy");
            string saat = comboBox1.Text;
            string dakika = comboBox2.Text;
            MessageBox.Show(tarih);

            if (CmbBrans.Text == "" || CmbDoktor.Text == "" || secilenTarih == DateTime.MinValue || string.IsNullOrEmpty(saat) || string.IsNullOrEmpty(dakika))
            {
                MessageBox.Show("Boş kalan alanları lütfen doldurunuz .", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand komutkaydet = new SqlCommand("insert into Tbl_Randevular (Randevubrans,Randevudoktor,Randevutarih,Randevusaat) values (@p1,@p2,@p3,@p4)", bgl.baglanti());
                komutkaydet.Parameters.AddWithValue("@p1", CmbBrans.Text);
                komutkaydet.Parameters.AddWithValue("@p2", CmbDoktor.Text);
                komutkaydet.Parameters.AddWithValue("@p3", tarih);
                komutkaydet.Parameters.AddWithValue("@p4", saat + ":"+ dakika);
                komutkaydet.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Randevu oluşturuldu .", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();
            }
        }
        // Branş seçtikten sonra combobox'a o branşdaki doktorları ekledik
        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();

            SqlCommand komut = new SqlCommand("Select (Doktorad+' '+Doktorsoyad) From Tbl_Doktorlar where Doktorbrans=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbDoktor.Items.Add(dr[0].ToString());
            }
            bgl.baglanti().Close();
        }
        private void BtnOluştur_Click(object sender, EventArgs e)
        {
            if (RchDuyuru.Text == "")
            {
                MessageBox.Show("Lütfen metin giriniz .", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular (Duyuru) values (@d1)", bgl.baglanti());
                komut.Parameters.AddWithValue("@d1", RchDuyuru.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Duyuru oluşturuldu .", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RchDuyuru.Text = "";
            }
        }

        private void BtnBranşPanel_Click_1(object sender, EventArgs e)
        {
            FrmBransPanel frm = new FrmBransPanel();
            frm.Show();
        }
        private void BtnDuyuru_Click(object sender, EventArgs e)
        {
            FrmDuyurular frm = new FrmDuyurular();
            frm.Show();
        }

        private void BtnDoktorPanel_Click_1(object sender, EventArgs e)
        {
            FrmDoktorPanel frm = new FrmDoktorPanel();
            frm.Show();
        }

        private void BtnRandevuListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListe frm = new FrmRandevuListe();
            frm.Show();
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void Txtid_TextChanged(object sender, EventArgs e)
        {

        }

        private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular where Randevubrans='" + CmbBrans.Text + "'" + " and Randevudoktor='" + CmbDoktor.Text + "' and Randevudurum=0", bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void MskTarih_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void CmbBrans_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            SqlCommand komut3 = new SqlCommand("Select Doktorad,Doktorsoyad From Tbl_Doktorlar where Doktorbrans=@b1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@b1", CmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                CmbDoktor.Items.Add(dr3[0].ToString() + " " + dr3[1].ToString());
            }
            bgl.baglanti().Close();
        }
    }
}
