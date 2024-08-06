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
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        sqlbaglanti bgldetay = new sqlbaglanti();

        public string hastatc;

        void temizle()
        {
            CmbBrans.Text = "";
            CmbDoktor.Text = "";
            RchSikayet.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
        }
        void randevugecmislistele()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Tbl_Randevular WHERE Randevuhastatc=@Tc AND Randevudurum=0", bgldetay.baglanti());
            cmd.Parameters.AddWithValue("@Tc", LblTc.Text);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void aktifrandevular()
        {
            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Tbl_Randevular WHERE Randevuhastatc=@Tc AND Randevudurum=1 AND Randevutarih > @today", bgldetay.baglanti()))
            {
                // TC kimlik numarası ve bugünün tarihini doğru formatta parametre olarak ekliyoruz
                cmd.Parameters.AddWithValue("@Tc", LblTc.Text);
                cmd.Parameters.AddWithValue("@today", DateTime.Today.ToString("yyyy-MM-dd"));

                // SqlDataAdapter kullanarak verileri DataTable'a dolduruyoruz
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }

            // DataTable'ı DataGridView'e bağlıyoruz
            dataGridView2.DataSource = dt;


        }
        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            LblTc.Text = hastatc;

            // Ad Soyad çekme
            SqlCommand komut = new SqlCommand("Select Hastaad,Hastasoyad From Tbl_Hastalar where Hastatc=@d1", bgldetay.baglanti());
            komut.Parameters.AddWithValue("@d1", hastatc);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdsoyad.Text = dr[0].ToString() + " " + dr[1].ToString();
            }
            bgldetay.baglanti().Close();

            randevugecmislistele();
            aktifrandevular();



            //Branşları comboboxa aktarma
            SqlCommand komut2 = new SqlCommand("Select Bransad From Tbl_Branslar", bgldetay.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0].ToString());
            }
            bgldetay.baglanti().Close();
        }
        // Branş seçtikten sonra combobox'a o branşdaki doktorları ekledik
        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {

            CmbDoktor.Text = "";
            CmbDoktor.Items.Clear();

            SqlCommand komut3 = new SqlCommand("Select Doktorad,Doktorsoyad From Tbl_Doktorlar where Doktorbrans=@b1", bgldetay.baglanti());
            komut3.Parameters.AddWithValue("@b1", CmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                CmbDoktor.Items.Add(dr3[0].ToString() + " " + dr3[1].ToString());
            }
            bgldetay.baglanti().Close();
        }

        // aktif randevular
        private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*  DataTable dt = new DataTable();
              SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular where Randevubrans='" + CmbBrans.Text + "'" + " and Randevudoktor='" + CmbDoktor.Text + "' and Randevudurum=0", bgldetay.baglanti());
              da.Fill(dt);
              dataGridView2.DataSource = dt; */
        }
        private void LnkGuncelle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaGuncelle frmguncelle = new FrmHastaGuncelle();
            frmguncelle.tcno = LblTc.Text;
            frmguncelle.Show();
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;

            //  Txtid.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }

        private void BtnRandevu_Click(object sender, EventArgs e)
        {
            DateTime secilenTarih2 = tarih2.Value;
            string tarih = secilenTarih2.ToString("dd.MM.yyyy");
            string saat2 = comboBox1.Text;
            string dakika2 = comboBox2.Text;
            string doktor = CmbDoktor.Text;

            // Seçilen tarih ve saati birleştiriyoruz
            DateTime secilenTarihSaat = secilenTarih2.Date.AddHours(Convert.ToDouble(saat2)).AddMinutes(Convert.ToDouble(dakika2));

            // Bugünün tarih ve saati
            DateTime bugun = DateTime.Now;

            if (CmbDoktor.Text == "" || CmbBrans.Text == "" || RchSikayet.Text == "" || secilenTarih2 == DateTime.MinValue || string.IsNullOrEmpty(saat2) || string.IsNullOrEmpty(dakika2))
            {
                MessageBox.Show("Boş kalan alanları lütfen doldurunuz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (secilenTarihSaat < bugun)
            {
                // Seçilen tarih ve saat bugünden küçükse uyarı veriyoruz
                MessageBox.Show("Geçmiş bir tarih ve saat için randevu alamazsınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string randevuSaati = saat2 + ":" + dakika2;
                if (RandevuVarMi(tarih, randevuSaati, doktor))
                {
                    DialogResult result = MessageBox.Show("Seçtiğiniz saatte zaten bir randevu bulunmaktadır. Kontenjanı artırmak istiyor musunuz?", "Kontenjan Dolu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        // Kontenjanı artırma işlemi
                        SqlCommand komutkaydet = new SqlCommand("INSERT INTO Tbl_Randevular (Randevubrans, Randevudoktor, Randevutarih, Randevusaat, Randevuhastasikayet, Randevuhastatc, Randevudurum, Randevuekkontejan) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8)", bgldetay.baglanti());
                        komutkaydet.Parameters.AddWithValue("@p1", CmbBrans.Text);
                        komutkaydet.Parameters.AddWithValue("@p2", CmbDoktor.Text);
                        komutkaydet.Parameters.AddWithValue("@p3", tarih);
                        komutkaydet.Parameters.AddWithValue("@p4", randevuSaati);
                        komutkaydet.Parameters.AddWithValue("@p5", RchSikayet.Text);
                        komutkaydet.Parameters.AddWithValue("@p6", LblTc.Text);
                        komutkaydet.Parameters.AddWithValue("@p7", true);
                        komutkaydet.Parameters.AddWithValue("@p8", "ek kontejan");
                        komutkaydet.ExecuteNonQuery();
                        bgldetay.baglanti().Close();
                        MessageBox.Show("Kontenjan başarıyla artırıldı ve randevu oluşturuldu.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        temizle();
                        aktifrandevular();
                    }
                }
                else
                {
                    SqlCommand komutrandevu = new SqlCommand("INSERT INTO Tbl_Randevular (Randevubrans, Randevudoktor, Randevutarih, Randevusaat, Randevuhastatc, Randevuhastasikayet, Randevudurum) VALUES (@r1, @r2, @r3, @r4, @r5, @r6, @r7)", bgldetay.baglanti());
                    komutrandevu.Parameters.AddWithValue("@r1", CmbBrans.Text);
                    komutrandevu.Parameters.AddWithValue("@r2", CmbDoktor.Text);
                    komutrandevu.Parameters.AddWithValue("@r3", tarih);
                    komutrandevu.Parameters.AddWithValue("@r4", randevuSaati);
                    komutrandevu.Parameters.AddWithValue("@r5", LblTc.Text);
                    komutrandevu.Parameters.AddWithValue("@r6", RchSikayet.Text);
                    komutrandevu.Parameters.AddWithValue("@r7", "1");
                    komutrandevu.ExecuteNonQuery();
                    bgldetay.baglanti().Close();
                    MessageBox.Show(CmbDoktor.Text + " 'den " + CmbBrans.Text + " için randevu alındı.", "Tebrikler " + LblAdsoyad.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CmbBrans.Text = "";
                    CmbDoktor.Text = "";
                    RchSikayet.Text = "";
                    randevugecmislistele();
                    aktifrandevular();
                }
            }

        }
        private bool RandevuVarMi(string tarih, string saat, string doktor)
        {
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Tbl_Randevular WHERE Randevutarih = @tarih AND Randevudoktor = @doktor AND Randevusaat = @saat AND Randevudurum = @durum", bgldetay.baglanti());
            command.Parameters.AddWithValue("@tarih", tarih);
            command.Parameters.AddWithValue("@saat", saat);
            command.Parameters.AddWithValue("@doktor", doktor);
            command.Parameters.AddWithValue("@durum", true); // false değeri eklendi
            int count = Convert.ToInt32(command.ExecuteScalar());
            bgldetay.baglanti().Close();
            return count > 0;
        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void LnkGuncelle_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaGuncelle frmguncelle = new FrmHastaGuncelle();
            frmguncelle.tcno = LblTc.Text;
            frmguncelle.Show();
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void LblAdsoyad_Click(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Sadece satırın içindeki hücreye çift tıklama olayını dinlemek için kontrol edin
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Seçilen satırın randevu durumu hücresini alın
                DataGridViewCell cell = dataGridView2.Rows[e.RowIndex].Cells["Randevudurum"];
                if (cell.Value != null && cell.Value is bool)
                {
                    bool randevuDurumu = (bool)cell.Value;

                    // Eğer randevuDurumu true ise, kullanıcıya uyarı mesajı gösterin
                    if (randevuDurumu)
                    {
                        DialogResult result = MessageBox.Show("Randevuya gittiğinizi onaylıyor musunuz ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                        // Kullanıcının seçimine göre işlem yapın
                        if (result == DialogResult.Yes)
                        {
                            // Kullanıcının emin olduğunu doğruladıysa, randevu durumunu false olarak güncelleyin
                            dataGridView2.Rows[e.RowIndex].Cells["Randevudurum"].Value = false;

                            // Veritabanında güncelleme işlemi yapın
                            int randevuID = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["Randevuid"].Value);
                            // Burada randevu durumunu veritabanında güncellemelisiniz
                            // Örnek olarak, bir SQL sorgusu kullanarak güncelleme işlemi yapabilirsiniz:
                            using (bgldetay.baglanti())
                            {
                                string query = "UPDATE Tbl_Randevular SET Randevudurum = @Randevudurum  WHERE Randevuid = @randevuID";
                                SqlCommand command = new SqlCommand(query, bgldetay.baglanti());
                                command.Parameters.AddWithValue("@Randevudurum", false);
                                command.Parameters.AddWithValue("@randevuID", randevuID);

                                aktifrandevular();
                                randevugecmislistele();
                                bgldetay.baglanti();
                                command.ExecuteNonQuery();
                            }


                            // Veritabanı bağlantısını açın
                            using (SqlConnection connection = bgldetay.baglanti())
                            {
                                // FrmDegerlendirme formunu göster ve kullanıcının seçimini alın
                                FrmDegerlendirme frmDegerlendirme = new FrmDegerlendirme();
                                DialogResult dialogResult = frmDegerlendirme.ShowDialog();

                                // Eğer kullanıcı bir puan seçtiyse ve onayladıysa
                                if (dialogResult == DialogResult.OK)
                                {
                                    // Seçilen puanı alın
                                    string doktorpuan = frmDegerlendirme.GetPuan();

                                    // Veritabanında güncelleme işlemi için sorguyu tanımlayın
                                    string query = "UPDATE Tbl_Randevular SET Randevupuan = @Randevupuan WHERE Randevuid = @randevuID";

                                    // SqlCommand nesnesini oluşturun ve parametreleri ekleyin
                                    using (SqlCommand command = new SqlCommand(query, connection))
                                    {
                                        command.Parameters.AddWithValue("@Randevupuan", doktorpuan);
                                        command.Parameters.AddWithValue("@randevuID", randevuID);
                                        aktifrandevular();
                                        randevugecmislistele();
                                        // Bağlantıyı açın ve sorguyu yürütün
                                        command.ExecuteNonQuery();
                                    }
                                }
                            }


                        }
                    }
                }
            }
        }


        private void Txtid_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            randevugecmislistele();
            aktifrandevular();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            randevugecmislistele();        }

        private void tarih2_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
