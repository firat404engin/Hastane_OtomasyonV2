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
    public partial class FrmDuyurular : Form
    {
        public FrmDuyurular()
        {
            InitializeComponent();
        }

        sqlbaglanti bgl = new sqlbaglanti();

        private void FrmDuyurular_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select Duyuru From Tbl_Duyurular", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
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

        private void button1_Click(object sender, EventArgs e)
        {
            // Eğer bir satır seçiliyse devam et
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    // DataGridView'den seçilen satırı kaldır
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);

                    MessageBox.Show("Satır başarıyla silindi.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Lütfen silinecek bir satır seçin.");
            }

        }
    }
}

