using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaseBPB
{
    public partial class Impresion_de_Factura : Form
    {
        public Impresion_de_Factura()
        {
            InitializeComponent();
        }

        private void Impresion_de_Factura_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'BPBDataSet1.Imprimir_Facturas' Puede moverla o quitarla según sea necesario.
            this.Imprimir_FacturasTableAdapter.Fill(this.BPBDataSet1.Imprimir_Facturas);

            this.reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
