using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
//AGREGANDO NAME SPACES PARA SQL
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace BaseBPB
{
    /// <summary>
    /// Lógica de interacción para Categoria.xaml
    /// </summary>
    public partial class Categoria : Window
    {

        SqlConnection sqlConnection;
        public Categoria()
        {
           string connectionString = ConfigurationManager.ConnectionStrings["BaseBPB.Properties.Settings.BPBConnectionString"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);

            InitializeComponent();
            limpiar();
            MostrarCategorias();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void limpiar()
        {
            txtnombre.Text = string.Empty;
            txtdescripcion.Text = string.Empty;
        }
       

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtnombre.Text == String.Empty)
            {
                MessageBox.Show("El nombre de la Categoria no puede estar vacio");
                txtnombre.Focus();
            }
            else
            {
                try
                {
                    // Query de inserción
                    string query = @"INSERT INTO Productos.Categoria (Nombre,Descripcion)
                                 VALUES (@Nombre,@Descripcion)";

                    // Abrir la conexión
                    sqlConnection.Open();

                    // Definir un SQL Command dado que vamos a utilizar parámetros
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                    // Reemplazar el parámetro con su valor respectivo
                    sqlCommand.Parameters.AddWithValue("@Nombre", txtnombre.Text);
                    sqlCommand.Parameters.AddWithValue("@Descripcion", txtdescripcion.Text);
                  

                    // Ejecutar el query de inserción
                    // https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlcommand.executescalar
                    sqlCommand.ExecuteNonQuery();

                    limpiar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    // Siempre cerrar la conexión
                    sqlConnection.Close();
                    // Actualizar el ListBox de Zoológicos
                    MostrarCategorias();
                }
            }
        }

        private void MostrarCategorias()
        {
            try
            {
                // El query de consulta hacia la base de datos
                string query = "select * from Productos.Categoria order by Nombre asc";

                // SqlDataAdapter es una interfaz entre las tablas de la
                // base de datos y los objetos utilizables en C#
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);

                // Utilizar el SqlDataAdapter
                using (sqlDataAdapter)
                {
                    // Objeto en C# que refleje una tabla de una BD
                    DataTable tabla = new DataTable();

                    // Llenar el objeto de tipo DataTable con los valores que contiene el SqlDataAdapter
                    sqlDataAdapter.Fill(tabla);

                    // ¿Cuál es la información de la tabla en el DataTable que se debería desplegar al usuario?
                    lbcategoria.DisplayMemberPath = "Nombre";
                    // ¿Qué valor debe ser entregado cuando un elemento de nuestro ListBox es seleccionado?
                    lbcategoria.SelectedValuePath = "IdCategoria";
                    // ¿Quién es la referencia de los datos para el ListBox (popular)?
                    lbcategoria.ItemsSource = tabla.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ELIMINARCategoria(object sender, RoutedEventArgs e)
        {
            if (lbcategoria.SelectedValue == null)
                MessageBox.Show("Debes seleccionar una Categoria antes de eliminarla");
            else
            {
                try
                {
                    // Query de eliminación
                    string query = @"DELETE FROM Productos.Categoria
                                     WHERE IdCategoria = @IdCategoria";

                    // Abrir la conexión
                    sqlConnection.Open();

                    // Crear el comando sql
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                    // Establecer los parámetros con sus valores actuales
                    sqlCommand.Parameters.AddWithValue("@IdCategoria", lbcategoria.SelectedValue);

                    // Ejecutar el query de eliminación
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    // Cerrar la conexión
                    sqlConnection.Close();
                    // Actualiza el ListBox de zoológicos
                    MostrarCategorias();
                }
            }
        }

        private void BTNEDITAR_Copy_Click(object sender, RoutedEventArgs e)
        {
            string text2;

            if (lbcategoria.SelectedValue == null)
            {
                MessageBox.Show("Debe Selecionar una Categoria");
            }
            else
            {
                text2 = lbcategoria.SelectedValue.ToString();

                //Producto ventana = new Producto(text2);
                MessageBox.Show(text2);
                
               // ventana.ShowDialog();
                this.Close();
            }
        }
    }
}
