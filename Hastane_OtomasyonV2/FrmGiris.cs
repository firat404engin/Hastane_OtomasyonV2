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
    public partial class FrmGiris : Form
    {
        public FrmGiris()
        {
            InitializeComponent();
        }
        private void FormGetir(Form frm)
        {
            panel1.Controls.Clear();
            frm.MdiParent = this;
            frm.FormBorderStyle = FormBorderStyle.None;
            panel1.Controls.Add(frm);
            frm.Show();

        }
        private void BtnHasta_Click(object sender, EventArgs e)
        {
            FrmHastaGiris frmHastaGiris = new FrmHastaGiris();
            FormGetir(frmHastaGiris);
            
        }

        private void FrmGiris_Load(object sender, EventArgs e)
        {

        }

        private void BtnDoktor_Click_1(object sender, EventArgs e)
        {
            FrmDoktorGiris frmdoktor = new FrmDoktorGiris();
            FormGetir(frmdoktor);
       
        }

        private void BtnSekreter_Click_1(object sender, EventArgs e)
        {
            FrmSekreterGiris frmsekreter = new FrmSekreterGiris();
            FormGetir(frmsekreter);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
