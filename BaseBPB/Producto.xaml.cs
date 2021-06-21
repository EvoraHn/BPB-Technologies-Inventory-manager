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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BaseBPB
{
    /// <summary>
    /// Lógica de interacción para Producto.xaml
    /// </summary>
    public partial class Producto : Window
    {
        private ClaseProducto Inventario = new ClaseProducto();
        private List<ClaseProducto> Productos;

        public Producto(string text)
        {
            InitializeComponent();
            txtproveedor.Text = text;
            txtcategoria.Text = text;
            Llenar();
            limpiar();
            OcultarBotonesSecundarios(Visibility.Hidden);
        }

        private void Llenar()
        {

            Productos = Inventario.MostrarProductos();
            lblproducto.DisplayMemberPath = "NombreProducto";
            lblproducto.SelectedValuePath = "IdProducto";
            lblproducto.ItemsSource = Productos;

        }
        private ClaseProducto ObtenerValoresFormularioAlternativo()
        {
            ClaseProducto ElProducto = new ClaseProducto();


            //chequear esto
            ElProducto.IdProducto = (lblproducto.SelectedValue).ToString();
            //txtId.Text= lblproducto.SelectedValue.ToString();
           
            ElProducto.NombreProducto = (txtnombre.Text).ToUpper();
            ElProducto.PrecioCosto = Convert.ToDouble(txtCosto.Text);
            ElProducto.PrecioVenta = Convert.ToDouble(txtventa.Text);
            ElProducto.Cantidad = Convert.ToInt32(txtCantidad.Text);
            ElProducto.Proveedor = Convert.ToInt32(txtproveedor.Text);
            ElProducto.Categoria = Convert.ToInt32(txtcategoria.Text);


            return ElProducto;
        }


        private ClaseProducto ObtenerValoresFormulario()
        {
            ClaseProducto ElProducto = new ClaseProducto();


            //chequear esto
            //ElProducto.IdProducto = (lblproducto.SelectedValue).ToString();
            //txtId.Text= lblproducto.SelectedValue.ToString();
            ElProducto.IdProducto = txtId.Text;
            ElProducto.NombreProducto = (txtnombre.Text).ToUpper();
            ElProducto.PrecioCosto = Convert.ToDouble(txtCosto.Text);
            ElProducto.PrecioVenta = Convert.ToDouble(txtventa.Text);
            ElProducto.Cantidad = Convert.ToInt32(txtCantidad.Text);
            ElProducto.Proveedor = Convert.ToInt32(txtproveedor.Text);
            ElProducto.Categoria = Convert.ToInt32(txtcategoria.Text);


            return ElProducto;
        }

        private void ValoresFormularioDesdeObjeto(ClaseProducto producto)
        {
            lblproducto.SelectedValue = producto.IdProducto;
            txtId.Text = producto.IdProducto;
            txtnombre.Text = producto.NombreProducto;
            txtCosto.Text = producto.PrecioCosto.ToString();
            txtventa.Text = producto.PrecioVenta.ToString();
            txtcategoria.Text = producto.Categoria.ToString();
            txtproveedor.Text = producto.Proveedor.ToString();
            txtCantidad.Text = producto.Cantidad.ToString();


        }

        private void OcultarBotonesPrincipales(Visibility valor)
        {
            BtnAgregar.Visibility = valor;
            BtnEditar.Visibility = valor;
            BtnEliminar.Visibility = valor;
            //btnRegresar.Visibility = valor;
        }
        private void OcultarBotonesSecundarios(Visibility valor1)
        {
            BtnAceptar.Visibility = valor1;
            BtnCancelar.Visibility = valor1;
          
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que todos los valores sean ingresados
            if (txtId.Text == string.Empty || txtnombre.Text == string.Empty)
                MessageBox.Show("Favor ingresar todos los valores en las cajas de texto.");
            else
            {
                try
                {
                    // Obtener los valores para la habitación
                    Inventario = ObtenerValoresFormulario();

                    // Insertar los datos de la habitación
                    Inventario.InsertarProducto(Inventario);

                    // Llenar el ListBox de habitaciones
                    Llenar();

                    // Limpiar los valores ingresados
                    ClaseProducto ElInventario = new ClaseProducto();
                    ValoresFormularioDesdeObjeto(ElInventario);

                    // Mensaje de inserción realizada
                    MessageBox.Show("¡Datos insertados correctamente!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que todos los valores sean ingresados
           
                try
                {
                    // Obtener los valores para la habitación
                    Inventario = ObtenerValoresFormularioAlternativo();

                    // Insertar los datos de la habitación
                    Inventario.EliminarProducto(Inventario);

                    // Llenar el ListBox de habitaciones
                    Llenar();

                    // Limpiar los valores ingresados
                    ClaseProducto ElInventario = new ClaseProducto();
                    ValoresFormularioDesdeObjeto(ElInventario);

                    // Mensaje de inserción realizada
                    MessageBox.Show("¡Datos Borrados correctamente!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            
        }

        private void BtnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (lblproducto.SelectedValue == null)
                MessageBox.Show("Debes seleccionar un Producto del listado.");
            else
            {
                try
                {
                    // Buscar la habitación por el id
                    Inventario = Inventario.BuscarProducto((lblproducto.SelectedValue).ToString());

                    // Pasar los valores al formulario
                    ValoresFormularioDesdeObjeto(Inventario);

                    // Ocultar los botones normales
                    OcultarBotonesPrincipales(Visibility.Hidden);
                    OcultarBotonesSecundarios(Visibility.Visible);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            //este no es
            ClaseProducto ElInventario = new ClaseProducto();
            OcultarBotonesPrincipales(Visibility.Visible);
            OcultarBotonesSecundarios(Visibility.Hidden);
            ValoresFormularioDesdeObjeto(ElInventario);
        }


        private void BtnAceptar_Click(object sender, RoutedEventArgs e)
        {
            // Verificar que todos los valores sean ingresados
            if (txtId.Text == string.Empty || txtnombre.Text == string.Empty)
                MessageBox.Show("Favor ingresar todos los valores en las cajas de texto.");
           
            else
            {
                try
                {
                    // Obtener los valores para la habitación
                    Inventario = ObtenerValoresFormulario();

                    // Insertar los datos de la habitación
                    Inventario.ModificarProducto(Inventario);

                    // Llenar el ListBox de habitaciones
                    Llenar();

                    // Mensaje de inserción realizada
                    MessageBox.Show("¡Registro modificado correctamente!");

                    // Mostrar los botones
                    OcultarBotonesPrincipales(Visibility.Visible);
                    OcultarBotonesSecundarios(Visibility.Hidden);
                    // Reestablecer los valores del formulario
                    ClaseProducto ElInventario = new ClaseProducto();
                    ValoresFormularioDesdeObjeto(ElInventario);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void iraproveedor(object sender, RoutedEventArgs e)
        {
            Proveedor ventana = new Proveedor();
            ventana.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Categoria ventana = new Categoria();
            ventana.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

       
        private void limpiar()
        {
            txtnombre.Text = string.Empty;
            txtId.Text = string.Empty;
            txtCantidad.Text = string.Empty;
            txtCosto.Text = string.Empty;
            txtventa.Text = string.Empty;
            //quitar estas 2 lineas si se desea usar el enlace
            txtcategoria.Text = string.Empty;
            txtproveedor.Text = string.Empty;
        }

        private void BtnEliminar_Copy_Click(object sender, RoutedEventArgs e)
        {
            InventarioCompleto Formulario = new InventarioCompleto();
            Formulario.ShowDialog();
        }

        private void Lblproducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
