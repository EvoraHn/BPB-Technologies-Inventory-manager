using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//AGREGANDO NAME SPACES PARA SQL
using System.Data.SqlClient;
using System.Configuration;

namespace BaseBPB
{
    public partial class InventarioCompleto : Form
    {

        private static string connectionString = ConfigurationManager.ConnectionStrings["BaseBPB.Properties.Settings.BPBConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);
        public InventarioCompleto()
        {
            InitializeComponent();
        }
        DataSet resultados = new DataSet();
        DataView mifiltro;

        public void leer_datos(string query,ref DataSet dstprincipal,String tabla)
        {
            try
            {
                //string cadena = " DataSource=(local)/sqlexpress;Database=BPB ; integrate security= true";
                //SqlConnection cn = new SqlConnection(cadena);
                //SqlCommand cmd = new SqlCommand(query, cn);
               // cn.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                sqlConnection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dstprincipal, tabla);
                da.Dispose();
                sqlConnection.Close();
                //cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SqlCommand Comando = new SqlCommand("SELECT * FROM Productos.Producto", sqlConnection);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = Comando;
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            gvInventario.DataSource = tabla;
        }

        /*private void InventarioCompleto_Load(object sender, EventArgs e)
        {
            this.leer_datos("select * from Productos.Producto",ref resultados, "Productos.Producto");
            this.mifiltro = ((DataTable)resultados.Tables["Productos.Producto"]).DefaultView;
            this.gvInventario.DataSource = mifiltro;
        }*/
        private void InventarioCompleto_Load(object sender, EventArgs e)
        {
            this.leer_datos("select * from vista1", ref resultados, "Vista1");
            this.mifiltro = ((DataTable)resultados.Tables["vista1"]).DefaultView;
            this.gvInventario.DataSource = mifiltro;
        }




        /*     private void TextBox1_KeyUp(object sender, KeyEventArgs e)
         {
             string salida_datos = "";
             string[] palabras_busqueda = this.textBox1.Text.Split(' ');
             foreach(string palabra in palabras_busqueda)
             {
                 if(salida_datos.Length == 0)
                 {
                     salida_datos = "(IdProducto LIKE '%" + palabra + "%' OR Nombre LIKE '%" + palabra + "%' )";
                 }
                 else
                 {
                     salida_datos = "AND (IdProducto LIKE '%" + palabra + "%' OR Nombre LIKE '%" + palabra + "%' )";
                 }
                // OR Categoria LIKE '%" + palabra + "%' OR Proveedor LIKE '%" + palabra + "%'
             }
             this.mifiltro.RowFilter = salida_datos;
         }
         */
        private void TextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string salida_datos = "";
            string[] palabras_busqueda = this.textBox1.Text.Split(' ');
            foreach (string palabra in palabras_busqueda)
            {
                if (salida_datos.Length == 0)
                {
                    salida_datos = "(Codigo LIKE '%" + palabra + "%' OR Nombre_de_Producto LIKE '%" + palabra + "%' OR Categoria LIKE '%" + palabra + "%' OR Proveedor LIKE '%" + palabra + "%' )";
                    //salida_datos = "(Codigo LIKE '%" + palabra + "%'  )";
                }
                else
                {
                    salida_datos += "AND(Codigo LIKE '%" + palabra + "%' OR Nombre_de_Producto LIKE '%" + palabra + "%' OR Categoria LIKE '%" + palabra + "%' OR Proveedor LIKE '%" + palabra + "%' )";
                    //salida_datos = "AND (Codigo LIKE '%" + palabra + "%' )";
                }
                // OR Categoria LIKE '%" + palabra + "%' OR Proveedor LIKE '%" + palabra + "%'
            }
            this.mifiltro.RowFilter = salida_datos;
        }







        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void InventarioCompleto_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void PrintDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(this.gvInventario.Width, this.gvInventario.Height);

            gvInventario.DrawToBitmap(bm, new Rectangle(0, 0, this.gvInventario.Width, this.gvInventario.Height));

            e.Graphics.DrawImage(bm, 10, 10);
        }

        private void GvInventario_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
