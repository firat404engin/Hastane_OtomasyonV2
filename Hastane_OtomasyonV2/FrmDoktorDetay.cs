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
    public partial class FrmDoktorDetay : Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }
        sqlbaglanti bgl = new sqlbaglanti();
        public string doktortc;

        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            LblTc.Text = doktortc;

            // ad soyad çekme
            SqlCommand komut = new SqlCommand("Select Doktorad,Doktorsoyad From Tbl_Doktorlar where Doktortc=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", doktortc);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdsoyad.Text = dr[0].ToString() + " " + dr[1].ToString();
            }
            bgl.baglanti().Close();

            // doktorun aktif randevuları
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular where Randevudoktor='" + LblAdsoyad.Text + "' and Randevudurum=1", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void BtnBilgiDuzenle_Click_1(object sender, EventArgs e)
        {
            FrmDoktorGuncelle frmdoktorguncelle = new FrmDoktorGuncelle();
            frmdoktorguncelle.doktortc = LblTc.Text;
            frmdoktorguncelle.Show();
        }

        private void BtnDuyuru_Click(object sender, EventArgs e)
        {
            FrmDuyurular frm = new FrmDuyurular();
            frm.Show();
        }

        private void BtnCıkıs_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // şikayetleri richtextboxa çekme
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            RchSikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // İlgili hücrelerin değerlerini alıp ilgili metin kutularına ve maskelenmiş metin kutusuna doldur
                RchSikayet.Text = Convert.ToString(selectedRow.Cells["Randevuhastasikayet"].Value);

            }
        }
    }
}
