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
using System.Windows.Navigation;
using System.Windows.Shapes;
//AGREGANDO NAME SPACES PARA SQL
using System.Data;
using System.Data.SqlClient;
using System.Configuration;



namespace BaseBPB
{
    /// <summary>
    /// Lógica de interacción para Proveedor.xaml
    /// </summary>
    public partial class Proveedor : Window
    {
        SqlConnection sqlConnection;

       /* private ClasePro usuario = new ClasePro();
        private List<ClasePro> usuario;*/

        //primer constructor
        public Proveedor()
        {
            InitializeComponent();

            //METODO DE CONEXION A SQL
            string connectionString = ConfigurationManager.ConnectionStrings["BaseBPB.Properties.Settings.BPBConnectionString"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);

           

        MostrarProveedores();

            limpiar();



        }


        private void MostrarProveedores()
        {
            try
            {
                // El query de consulta hacia la base de datos
                string query = "SELECT * FROM Productos.Proveedor";

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
                    lbProveedor.DisplayMemberPath = "Empresa";
                   
                    // ¿Qué valor debe ser entregado cuando un elemento de nuestro ListBox es seleccionado?
                    lbProveedor.SelectedValuePath = "IdProveedor";
                    // ¿Quién es la referencia de los datos para el ListBox (popular)?
                    lbProveedor.ItemsSource = tabla.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void ATRAS(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void limpiar()
        {
            // Limpiar el valor del texto en txtInformacion
            txtEmpresa.Text = String.Empty;
            txtVendedor.Text = String.Empty;
            txtContacto.Text = String.Empty;
            txtContacto1.Text = String.Empty;
            txtDireccion.Text = String.Empty;
        }

        private void AGREGARPROVEEDOR(object sender, RoutedEventArgs e)
        {
            if (txtEmpresa.Text == String.Empty)
            {
                MessageBox.Show("El nombre de la empresa no puede estar vacio");
                txtEmpresa.Focus();
            }
            else
            {
                try
                {
                    // Query de inserción
                    string query = @"INSERT INTO Productos.Proveedor (Empresa,Vendedor,Contacto,Contacto1,Descripcion)
                                 VALUES (@Empresa,@Vendedor,@Contacto,@Contacto1,@Descripcion)";

                    // Abrir la conexión
                    sqlConnection.Open();

                    // Definir un SQL Command dado que vamos a utilizar parámetros
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                    // Reemplazar el parámetro con su valor respectivo
                    sqlCommand.Parameters.AddWithValue("@Empresa", txtEmpresa.Text);
                    sqlCommand.Parameters.AddWithValue("@Vendedor", txtVendedor.Text);
                    sqlCommand.Parameters.AddWithValue("@Contacto", txtContacto.Text);
                    sqlCommand.Parameters.AddWithValue("@Contacto1", txtContacto1.Text);
                    sqlCommand.Parameters.AddWithValue("@Descripcion", txtDireccion.Text);

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
                    MostrarProveedores();
                }
            }
        }

        private void ELIMINARPROVEEDOR(object sender, RoutedEventArgs e)
        {
            if (lbProveedor.SelectedValue == null)
                MessageBox.Show("Debes seleccionar un Proveedor antes de eliminarlo");
            else
            {
                try
                {
                    // Query de eliminación
                    string query = @"DELETE FROM Productos.Proveedor
                                     WHERE IdProveedor = @IdProveedor";

                    // Abrir la conexión
                    sqlConnection.Open();

                    // Crear el comando sql
                    SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                    // Establecer los parámetros con sus valores actuales
                    sqlCommand.Parameters.AddWithValue("@IdProveedor",lbProveedor.SelectedValue);

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
                    MostrarProveedores();
                }
            }
        }

        /* private void BTNEDITAR_Click(object sender, RoutedEventArgs e)
         {
             if (txtEmpresa.Text == string.Empty)
             {
                 MessageBox.Show("Debes ingresar el nuevo nombre del animal en la caja de texto");
                 txtEmpresa.Focus();
             }
             else if (lbProveedor.SelectedValue == null)
                 MessageBox.Show("Debes seleccionar el animal a ser actualizado");
             else
             {
                 try
                 {
                     // Query de actualización
                     string query = @"UPDATE Zoo.Animal
                                      SET nombre = @nombreAnimal
                                      WHERE id = @idAnimal";

                     // Abrir la conexión
                     sqlConnection.Open();

                     // Comando sql
                     SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                     // Reemplazar los parámetros por sus valores reales
                     sqlCommand.Parameters.AddWithValue("@nombreAnimal", txtInformacion.Text);
                     sqlCommand.Parameters.AddWithValue("@idAnimal", lbAnimales.SelectedValue);

                     // Ejecutar el query de actualización
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
                     // Limpiar el TextBox
                     txtInformacion.Text = string.Empty;
                     // Actualizar el listado de animales
                     MostrarAnimales();
                 }

             }
             
    }
    */

        /* private void ValoresFormularioDesdeObjeto(ClasePro usuario)
         {
             lbProveedor.SelectedValue = usuario.IdProveedor;
             txtEmpresa.Text = usuario.Empresa.ToString();
             //txtNombre.Clear();
             txtVendedor.Text = usuario.Vendedor.ToString();
             //txtContra.Clear();
         }

         private void EDITARPROVEEDOR(object sender, RoutedEventArgs e)
         {



             ValoresFormularioDesdeObjeto(usuario);
         }*/

            //codigo para enviar el codigo de proveedor desde el formularios
       
        private void BTNEDITAR_Copy_Click(object sender, RoutedEventArgs e)
        {
            string valor;

            if (lbProveedor.SelectedValue == null)
            {
                MessageBox.Show("Debe Selecionar un proveedor");
            }
            else
            {
                valor = lbProveedor.SelectedValue.ToString();
              
                Producto ventana = new Producto(valor);
                MessageBox.Show(valor);
                //ventana.ShowDialog();
                this.Close();
            }

        }
    }
}
