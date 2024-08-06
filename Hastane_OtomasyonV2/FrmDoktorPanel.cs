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
    public partial class FrmDoktorPanel : Form
    {
        public FrmDoktorPanel()
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
            CmbBrans.Text = "";
            TxtSifre.Text = "";
        }

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Doktorlar", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void FrmDoktorPanel_Load(object sender, EventArgs e)
        {
            listele();

            // branşları comboboxa aktarma
            SqlCommand komut = new SqlCommand("Select Bransad From Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                CmbBrans.Items.Add(dr[0].ToString());
            }
            bgl.baglanti().Close();
        }
        private void BtnEkle_Click_1(object sender, EventArgs e)
        {
            if (TxtAd.Text == "" || TxtSoyad.Text == "" || MskTc.Text == "" || CmbBrans.Text == "" || TxtSifre.Text == "")
            {
                MessageBox.Show("Boş kalan alanları lütfen doldurunuz .", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand komutekle = new SqlCommand("insert into Tbl_Doktorlar (Doktorad,Doktorsoyad,Doktortc,Doktorbrans,Doktorsifre) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                komutekle.Parameters.AddWithValue("@p1", TxtAd.Text);
                komutekle.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                komutekle.Parameters.AddWithValue("@p3", MskTc.Text);
                komutekle.Parameters.AddWithValue("@p4", CmbBrans.Text);
                komutekle.Parameters.AddWithValue("@p5", TxtSifre.Text);
                komutekle.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Doktor eklendi .", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();
                listele();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            TxtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            MskTc.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            CmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtSifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }

        // delete
        private void BtnSil_Click_1(object sender, EventArgs e)
        {
            if (TxtAd.Text == "" || TxtSoyad.Text == "" || MskTc.Text == "" || CmbBrans.Text == "" || TxtSifre.Text == "")
            {
                MessageBox.Show("Boş alanlar varken silme işlemi yapılmaz .", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult secenek = MessageBox.Show("Bu işlem geri alınamaz . Doktor silinsin mi ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (secenek == DialogResult.Yes)
                {
                    SqlCommand komutsil = new SqlCommand("Delete From Tbl_Doktorlar where Doktortc=@p1", bgl.baglanti());
                    komutsil.Parameters.AddWithValue("@p1", MskTc.Text);
                    komutsil.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    temizle();
                    listele();
                }
                else
                {
                    temizle();
                }
            }
        }

        // update
        private void BtnGuncelle_Click_1(object sender, EventArgs e)
        {
            if (TxtAd.Text == "" || TxtSoyad.Text == "" || MskTc.Text == "" || CmbBrans.Text == "" || TxtSifre.Text == "")
            {
                MessageBox.Show("Boş alanlar varken güncelleme işlemi yapılmaz .", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult secenek = MessageBox.Show("Doktor bilgileri güncellensin mi ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (secenek == DialogResult.Yes)
                {
                    SqlCommand komutguncelle = new SqlCommand("Update Tbl_Doktorlar set Doktorad=@p1,Doktorsoyad=@p2,Doktorbrans=@p3,Doktorsifre=@p4 where Doktortc=@p5", bgl.baglanti());
                    komutguncelle.Parameters.AddWithValue("@p1", TxtAd.Text);
                    komutguncelle.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                    komutguncelle.Parameters.AddWithValue("@p5", MskTc.Text);
                    komutguncelle.Parameters.AddWithValue("@p3", CmbBrans.Text);
                    komutguncelle.Parameters.AddWithValue("@p4", TxtSifre.Text);
                    komutguncelle.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    temizle();
                    listele();
                }
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

        private void button2_Click(object sender, EventArgs e)
        {
            TxtAd.Clear();
            TxtSoyad.Clear();
            TxtSifre.Clear();
            MskTc.Clear();
            CmbBrans.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // İlgili hücrelerin değerlerini alıp ilgili metin kutularına ve maskelenmiş metin kutusuna doldur
                TxtAd.Text = Convert.ToString(selectedRow.Cells["Doktorad"].Value);
                TxtSoyad.Text = Convert.ToString(selectedRow.Cells["Doktorsoyad"].Value);
                MskTc.Text = Convert.ToString(selectedRow.Cells["Doktortc"].Value);
                CmbBrans.Text = Convert.ToString(selectedRow.Cells["Doktorbrans"].Value);
                TxtSifre.Text = Convert.ToString(selectedRow.Cells["Doktorsifre"].Value);
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            if (TxtAd.Text == "" || TxtSoyad.Text == "" || MskTc.Text == "" || CmbBrans.Text == "" || TxtSifre.Text == "")
            {
                MessageBox.Show("Boş alanlar varken güncelleme işlemi yapılmaz .", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult secenek = MessageBox.Show("Doktor bilgileri güncellensin mi ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (secenek == DialogResult.Yes)
                {
                    SqlCommand komutguncelle = new SqlCommand("Update Tbl_Doktorlar set Doktorad=@p1,Doktorsoyad=@p2,Doktorbrans=@p3,Doktorsifre=@p4 where Doktortc=@p5", bgl.baglanti());
                    komutguncelle.Parameters.AddWithValue("@p1", TxtAd.Text);
                    komutguncelle.Parameters.AddWithValue("@p2", TxtSoyad.Text);
                    komutguncelle.Parameters.AddWithValue("@p5", MskTc.Text);
                    komutguncelle.Parameters.AddWithValue("@p3", CmbBrans.Text);
                    komutguncelle.Parameters.AddWithValue("@p4", TxtSifre.Text);
                    komutguncelle.ExecuteNonQuery();
                    bgl.baglanti().Close();
                    temizle();
                    listele();
                }
            }
        }
    }
}
