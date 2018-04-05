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
    public partial class frmAjedrez : Form
    {
        public frmAjedrez()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Dibuja el tablero en el panel de 8x8 en blanco y negro.
        /// </summary>
        private void FormarTablero()
        {
            Size _size = new Size(pnTABLERO.Width / 8, pnTABLERO.Height / 8);
            for (int f = 0; f < 8; f++)
                for (int c = 0; c < 8; c++)
                {
                    var panel = new Panel();
                   panel.Size = _size;
                   panel.Location = new Point(c * _size.Width, f * _size.Height);
                   panel.BackColor = (c + f) % 2 == 0 ? Color.Black : Color.White;
                    pnTABLERO.Controls.Add(panel);
                }
        }

        private void frmAjedrez_Load(object sender, EventArgs e)
        {
            FormarTablero();
        }
    }
}
