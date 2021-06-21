namespace BaseBPB
{
    partial class Impresion_de_Factura
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.BPBDataSet1 = new BaseBPB.BPBDataSet1();
            this.Imprimir_FacturasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Imprimir_FacturasTableAdapter = new BaseBPB.BPBDataSet1TableAdapters.Imprimir_FacturasTableAdapter();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.BPBDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Imprimir_FacturasBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // BPBDataSet1
            // 
            this.BPBDataSet1.DataSetName = "BPBDataSet1";
            this.BPBDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Imprimir_FacturasBindingSource
            // 
            this.Imprimir_FacturasBindingSource.DataMember = "Imprimir_Facturas";
            this.Imprimir_FacturasBindingSource.DataSource = this.BPBDataSet1;
            // 
            // Imprimir_FacturasTableAdapter
            // 
            this.Imprimir_FacturasTableAdapter.ClearBeforeFill = true;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "dsImprimirFactura";
            reportDataSource1.Value = this.Imprimir_FacturasBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "BaseBPB.Informefactura.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(829, 833);
            this.reportViewer1.TabIndex = 0;
            // 
            // Impresion_de_Factura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 833);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Impresion_de_Factura";
            this.Text = "Impresion_de_Factura";
            this.Load += new System.EventHandler(this.Impresion_de_Factura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BPBDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Imprimir_FacturasBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource Imprimir_FacturasBindingSource;
        private BPBDataSet1 BPBDataSet1;
        private BPBDataSet1TableAdapters.Imprimir_FacturasTableAdapter Imprimir_FacturasTableAdapter;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}