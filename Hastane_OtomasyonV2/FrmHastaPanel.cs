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
    public partial class FrmHastaPanel : Form
    {
        public FrmHastaPanel()
        {
            InitializeComponent();
        }
       
        sqlbaglanti baglanti = new sqlbaglanti();
        private void FrmHastaPanel_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Hastalar", baglanti.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    // Seçilen satırın Duyuruid değerini al
                    int hastaid = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Hastaid"].Value);

                    // Veritabanı bağlantısını aç
                    using (SqlConnection connection = baglanti.baglanti())
                    {
                        // DELETE sorgusunu tanımla ve çalıştır
                        string query = "DELETE FROM Tbl_Hastalar WHERE Hastaid = @hastaid";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Hastaid", hastaid);
                            command.ExecuteNonQuery();
                        }
                    }

                    // DataGridView'den seçilen satırı kaldır
                    dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);

                    MessageBox.Show("Hasta başarıyla silindi.");
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
