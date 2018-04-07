using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TableroDeAjedrez
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }
        public  string nombre;
        private void btnACEPTAR_Click(object sender, EventArgs e)
        {
            nombre = txtNOMBRE.Text;
            frmAjedrez ajedrez = new frmAjedrez(nombre);
            ajedrez.ShowDialog();
        }

        private void btnSALIR_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
