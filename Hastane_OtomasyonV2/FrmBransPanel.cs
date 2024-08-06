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
    public partial class FrmBransPanel : Form
    {
        public FrmBransPanel()
        {
            InitializeComponent();
        }
        sqlbaglanti bgl = new sqlbaglanti();
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * From Tbl_Branslar", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void temizle()
        {
            Txtid.Text = "";
            TxtBrans.Text = "";
        }


        private void BtnEkle_Click(object sender, EventArgs e)
        {
            if (TxtBrans.Text == "")
            {
                MessageBox.Show("Boş kalan alanları lütfen doldurunuz .", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand komutekle = new SqlCommand("insert into Tbl_Branslar (Bransad) values (@e1)", bgl.baglanti());
                komutekle.Parameters.AddWithValue("@e1", TxtBrans.Text);
                komutekle.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Branş eklendi .", "Tebrikler", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();
                listele();
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {

            if (TxtBrans.Text == "" || Txtid.Text == "")
            {
                MessageBox.Show("Boş kalan alanları lütfen doldurunuz .", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult sec = MessageBox.Show(TxtBrans.Text + " " + "Silinsin mi ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (sec == DialogResult.Yes)
                {
                    SqlCommand komutsil = new SqlCommand("Delete From Tbl_Branslar where Bransid=@s1", bgl.baglanti());
                    komutsil.Parameters.AddWithValue("@s1", Txtid.Text);
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

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            if (Txtid.Text == "" || TxtBrans.Text == "")
            {
                MessageBox.Show("Boş kalan alanları lütfen doldurunuz .", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult sec = MessageBox.Show("Branş ismi güncellensin mi ?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (sec == DialogResult.Yes)
                {
                    SqlCommand komutguncelle = new SqlCommand("Update Tbl_Branslar set Bransad=@g1 where Bransid=@g2", bgl.baglanti());
                    komutguncelle.Parameters.AddWithValue("@g1", TxtBrans.Text);
                    komutguncelle.Parameters.AddWithValue("@g2", Txtid.Text);
                    komutguncelle.ExecuteNonQuery();
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

        private void FrmBransPanel_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void Txtid_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // İlgili hücrelerin değerlerini alıp ilgili metin kutularına ve maskelenmiş metin kutusuna doldur
                Txtid.Text = Convert.ToString(selectedRow.Cells["Bransid"].Value);
                TxtBrans.Text = Convert.ToString(selectedRow.Cells["Bransad"].Value);
      
            }
        }
    }
}