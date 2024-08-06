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
    public partial class FrmDoktorDuyuru : Form
    {
        public FrmDoktorDuyuru()
        {
            InitializeComponent();
        }
        sqlbaglanti baglan = new sqlbaglanti();

        private void FrmDoktorDuyuru_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select Duyuruid ,Duyuru,tarih From Tbl_Duyurular", baglan.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            // Duyuru ID sütununun başlığını ve genişliğini ayarla
            dataGridView1.Columns["Duyuruid"].HeaderText = "Duyuru ID";
            dataGridView1.Columns["Duyuruid"].Width = 30; // Genişlik piksel cinsinden
                                                          // Duyuru ID sütununun başlığını ve genişliğini ayarla
            dataGridView1.Columns["tarih"].HeaderText = "Tarih";
            dataGridView1.Columns["tarih"].Width = 130; // Genişlik piksel cinsinden
            // Duyuru sütununun başlığını ve genişliğini ayarla
            dataGridView1.Columns["Duyuru"].HeaderText = "Duyuru";
            dataGridView1.Columns["Duyuru"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // Otomatik genişlik
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Sadece "Duyuru" sütununa tıklanıldığında işlem yap
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Duyuru"].Index)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                string duyuruMesaji = selectedRow.Cells["Duyuru"].Value.ToString();
                MessageBox.Show(duyuruMesaji, "Duyuru Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
