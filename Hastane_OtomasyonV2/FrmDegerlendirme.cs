using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hastane_OtomasyonV2
{
    public partial class FrmDegerlendirme : Form
    {
        public FrmDegerlendirme()
        {
            InitializeComponent();
        }
        sqlbaglanti baglanti = new sqlbaglanti();

        public string puan;

        public string GetPuan()
        { 
            return puan; 
        }


            private void label1_Click(object sender, EventArgs e)
        {
            puan = "1";
            MessageBox.Show(puan + " puan verdiniz");
            this.DialogResult = DialogResult.OK; // Kullanıcı bir puan seçtiğinde DialogResult değerini OK olarak ayarlayın
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            puan = "2";
            MessageBox.Show(puan + " puan verdiniz");
            this.DialogResult = DialogResult.OK; // Kullanıcı bir puan seçtiğinde DialogResult değerini OK olarak ayarlayın
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            puan = "3";
            MessageBox.Show(puan + " puan verdiniz");
            this.DialogResult = DialogResult.OK; // Kullanıcı bir puan seçtiğinde DialogResult değerini OK olarak ayarlayın
            this.Hide();
        }

        private void FrmDegerlendirme_Load(object sender, EventArgs e)
        {

        }
    }
}
