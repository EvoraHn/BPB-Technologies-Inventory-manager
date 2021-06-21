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
    /// Lógica de interacción para Compras.xaml
    /// </summary>
    public partial class Compras : Window
    {
        public double ayudasuma { get; set; }
        public double total { get; set; }
        
        public double descuentos { get; set; }
     
        //Variables miembro
        private ClaseProducto Producto = new ClaseProducto();
        private List<ClaseProducto> Productos;
        public Compras()
        {

            InitializeComponent();
            txtCosto.IsEnabled = false;
            txtMonto.IsEnabled = false;
            txtNombre.IsEnabled = false;
            txttotal.IsEnabled = false;
            txtsubtotal.IsEnabled = false;
            txtID.Focus();

            //LLenar el listbox
            Llenar();

            Limpiar();



            /*do
            {


                try
                {
                    // Buscar la habitación por el id
                    Producto = Producto.BuscarProducto((txtID).ToString());
                    X++;


                    // Pasar los valores al formulario
                    ValoresFormularioDesdeObjeto(Producto);

                    // Ocultar los botones normales
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            } while (X < 99999);*/
        }

        private void Llenar()
        {
          
            Productos = Producto.MostrarProductos();
            lbProducto.DisplayMemberPath = "NombreProducto";
            lbProducto.SelectedValuePath = "IdProducto";
            lbProducto.ItemsSource = Productos;
        
        }
        private ClaseProducto ObtenerValoresFormulario()
        {
            ClaseProducto ElProducto = new ClaseProducto();
            ElProducto.Cant = Convert.ToDouble(txtCantidad.Text);
            //Revisar Codigo

            //Cambio Realizado 07/10/19 8:47
            ElProducto.IdProducto =(txtID.Text).ToString();
            ElProducto.NombreProducto = txtNombre.Text;
            ElProducto.IdProducto = txtID.Text;
            ElProducto.PrecioCosto = Convert.ToDouble(txtCosto.Text);
           

            return ElProducto;
        }

        private void ValoresFormularioDesdeObjeto(ClaseProducto producto)
        {
            lbProducto.SelectedValue = producto.IdProducto;
            txtNombre.Text = producto.NombreProducto;
            txtCosto.Text = producto.PrecioCosto.ToString();
            txtID.Text = producto.IdProducto;
            
        }

        private void BtnCompra_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                if (txtID.Text == string.Empty)
                {

                    if (lbProducto.SelectedValue == null)
                        MessageBox.Show("Debes seleccionar un Producto del listado.");
                    else
                    {



                        Producto = Producto.BuscarProducto((lbProducto.SelectedValue).ToString());

                        ValoresFormularioDesdeObjeto(Producto);
                        txtMonto.Text = string.Empty;
                        txtCantidad.Text = "1";
                    }
                }
                else
                {
                    filtro();
                }
                // Buscar por el id



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnVentas_Copy.Focus();
            }
            else if (e.Key == Key.RightCtrl)
            {
                Agregaralcarrito.Focus();
            }
            else
            {

            }
        }

        private void Limpiar()
        {
            txtID.Text = string.Empty;
            txtCantidad.Text ="1";
            txtNombre.Text = string.Empty;
            txtCosto.Text = string.Empty;
            txtdescuento.Text = string.Empty;
            txtsubtotal.Text = string.Empty;
            txttotal.Text = string.Empty;
            txtMonto.Text = string.Empty;
            txtID.Focus();
        }

      
        
        private void BtnVentas_Copy2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

      
        //Boton Para Ventas Individuales
        private void BtnAgregar_click(object sender, RoutedEventArgs e)
        {
           
            
            double cantidad,precio,total;
          
            try
            {
                if (txtCantidad.Text == string.Empty)
                {
                    MessageBox.Show("Debe Seleccionar una cantidad");
                }
                else
                {
                    cantidad = Convert.ToDouble(txtCantidad.Text);
                    precio = Convert.ToDouble(txtCosto.Text);
                    total = cantidad * precio;
                    txtMonto.Text = total.ToString();
                    NuevaCompra();
                    Limpiar();
                   try
                    {
                        Producto.ModificarExistenciasCompra(Producto);
                        ayudasuma = ayudasuma + total;
                        txtsubtotal.Text = ayudasuma.ToString();
                        txttotal.Text = ayudasuma.ToString();
                    }
                   
                    catch (Exception ex)
                     {
                        // MessageBox.Show(ex.ToString());
                        MessageBox.Show("<<Error>> No se pudo Realizar la Compra <<Error>>", ex.ToString());
                       
                    }
        }

            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.ToString(),"<<Error>> No se pudo Realizar la Compra <<Error>>");
            }

        }

        private void LbProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        /*private void MostrarComprasHechas()
         {
             try
             {
                 // El query de consulta hacia la base de datos
                 string query = "SELECT * FROM Compras.Compra";

                 // SqlDataAdapter es una interfaz entre las tablas de la
                 // base de datos y los objetos utilizables en C#
                 SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, sqlConnection);

                 // Utilizar el SqlDataAdapter
                 using (sqlDataAdapter)
                 {
                     // Objeto en C# que refleje una tabla de una BD
                     DataTable tablaZoologico = new DataTable();

                     // Llenar el objeto de tipo DataTable con los valores que contiene el SqlDataAdapter
                     sqlDataAdapter.Fill(tablaZoologico);

                     // ¿Cuál es la información de la tabla en el DataTable que se debería desplegar al usuario?
                     lbCompra.DisplayMemberPath = "ciudad";
                     // ¿Qué valor debe ser entregado cuando un elemento de nuestro ListBox es seleccionado?
                     lbCompra.SelectedValuePath = "IdProducto";
                     // ¿Quién es la referencia de los datos para el ListBox (popular)?
                     lbCompra.ItemsSource = tablaZoologico.DefaultView;
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.ToString());
             }
         }
         */
       

        private void NuevaCompra()
        {
            // Verificar que todos los valores sean ingresados
          
                try
                {
                    // Obtener los valores para la habitación
                    Producto = ObtenerValoresFormulario();

                    // Insertar los datos de la habitación
                    Producto.Comprar(Producto);

                    // Llenar el ListBox de habitaciones
                 

                    // Limpiar los valores ingresados
                    ClaseProducto ElInventario = new ClaseProducto();
                    ValoresFormularioDesdeObjeto(ElInventario);

                    // Mensaje de inserción realizada
                    //MessageBox.Show("¡Venta Realizada correctamente!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
               
            }
        }

        private void Compra(object sender, RoutedEventArgs e)
        {
            if (lbProducto.SelectedValue == null)
                MessageBox.Show("Debes seleccionar un Producto del listado.");
            else
            {
                try
                {
                    // Buscar la habitación por el id
                    Producto = Producto.BuscarProducto((lbProducto.SelectedValue).ToString());

                    // Pasar los valores al formulario
                    ValoresFormularioDesdeObjeto(Producto);

                    // Ocultar los botones normales
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void TxtID_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void filtro()
        {

            // arreglo para capturar letras o numeros ingresados // o escaner
            string[] cadena = this.txtID.Text.Split(' ');
            try
            {
                // Buscar la habitación por el id
                Producto = Producto.BuscarProducto(txtID.Text);



                // Pasar los valores al formulario
                ValoresFormularioDesdeObjeto(Producto);

                // Ocultar los botones normales
                Llenar();
               // Agregaralcarrito.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //Agregaralcarrito.Focus();
        }

        private void TxtID_KeyUp(object sender, KeyEventArgs e)
        {
           /* // arreglo para capturar letras o numeros ingresados // o escaner
            string[] cadena = this.txtID.Text.Split(' ');
            try
            {
                // Buscar la habitación por el id
                Producto = Producto.BuscarProducto(txtID.Text);



                // Pasar los valores al formulario
                ValoresFormularioDesdeObjeto(Producto);

                // Ocultar los botones normales
                Llenar();
                //Agregaralcarrito.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }*/

        }

        private void Agregaralcarrito_Copy_Click(object sender, RoutedEventArgs e)
        {
            txtsubtotal.Text = string.Empty;
            txttotal.Text = string.Empty;
            ayudasuma = 0;
        }

        private void Txttotal_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Agregaralcarrito_Copy1_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        /*private void TxtID_KeyDown(object sender, KeyEventArgs e)
        {
            filtro();
        }*/
    }
   
}
