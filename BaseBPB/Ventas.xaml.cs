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
//namespaces


namespace BaseBPB
{
    /// <summary>
    /// Lógica de interacción para Ventas.xaml
    /// </summary>
    public partial class Ventas : Window
    {

        public double ayudasuma { get; set; }
        public double total { get; set; }

        public double descuentos { get; set; }

        private ClaseProducto Producto = new ClaseProducto();
        
        private List<ClaseProducto> Productos;

        private ClaseFactura Factura = new ClaseFactura();

        private List<ClaseFactura> Facturas;
        public Ventas()
        {

            InitializeComponent();
            txtCosto.IsEnabled = false;
            txtMonto.IsEnabled = false;
            txtNombre.IsEnabled = false;
            txttotal.IsEnabled = false;
            txtsubtotal.IsEnabled = false;
            txtdescuento.IsEnabled = false;
            txtfactura.IsEnabled = false;
            txtID.Focus();

            //LLenar el listbox
            Llenar();
           LlenarFacturas();

            Limpiar();

            NuevaFactura();

            
            txtfactura.Text = Factura.IdVenta.ToString();
        }

       
        //METODO LLENAR RELLENA EL LISTBOX CON LOS DATOS DE LA DB
        private void Llenar()
        {
            

            Productos = Producto.MostrarProductos();
            lbProducto.DisplayMemberPath = "NombreProducto";
            lbProducto.SelectedValuePath = "IdProducto";
            lbProducto.ItemsSource = Productos;

        }

        private void LlenarFacturas()
        {
           
            Facturas = Factura.MostrarFacturas();
            DGVentas.DisplayMemberPath = "Producto";
            DGVentas.SelectedValuePath = "IdVenta";
            DGVentas.ItemsSource = Facturas;

        }
        //METODO LIMPIAR DEJA EN BLANCO EL FORMULARIO
        private void Limpiar()
        {
            txtID.Text = string.Empty;
            txtCantidad.Text = "1";
            txtNombre.Text = string.Empty;
            txtCosto.Text = string.Empty;
            txtdescuento.Text = "0.00";
            txtsubtotal.Text = string.Empty;
            txttotal.Text = string.Empty;
            txtMonto.Text = string.Empty;
            txtID.Focus();
        }

        private void LimpiarSoloText()
        {
            txtID.Text = string.Empty;
            txtCantidad.Text = "1";
            txtNombre.Text = string.Empty;
            txtCosto.Text = string.Empty;
            txtMonto.Text = string.Empty;
            txtID.Focus();
        }

        private ClaseProducto ObtenerValoresFormulario()
        {
            ClaseProducto ElProducto = new ClaseProducto();
            ElProducto.Cant = Convert.ToDouble(txtCantidad.Text);
            //Revisar Codigo

            //Cambio Realizado 07/10/19 8:47
            ElProducto.IdProducto = (txtID.Text).ToString();
            ElProducto.NombreProducto = txtNombre.Text;
            ElProducto.IdProducto = txtID.Text;
            ElProducto.PrecioCosto = Convert.ToDouble(txtCosto.Text);
            ElProducto.PrecioVenta = Convert.ToDouble(txtCosto.Text);
            ElProducto.Venta = Convert.ToInt32(txtfactura.Text);
            ElProducto.Descuento = Convert.ToDouble(txtdescuento.Text);

            return ElProducto;
        }

        private void ValoresFormularioDesdeObjeto(ClaseProducto producto)
        {
            lbProducto.SelectedValue = producto.IdProducto;
            txtNombre.Text = producto.NombreProducto;
            txtCosto.Text = producto.PrecioVenta.ToString();
            txtID.Text = producto.IdProducto;

        }
        
        private void BtnCompra_Click(object sender, RoutedEventArgs e)
        {
            
                
                try
                {
                    if (txtID.Text==string.Empty)
                    {

                    if (lbProducto.SelectedValue == null)
                        MessageBox.Show("Debes seleccionar un Producto del listado.");
                    else
                    {



                        Producto = Producto.BuscarProducto((lbProducto.SelectedValue).ToString());

                        ValoresFormularioDesdeObjeto(Producto);
                        txtMonto.Text = ((Convert.ToDouble(txtCantidad.Text))* (Convert.ToDouble(txtCosto.Text))).ToString();
                        txtCantidad.Text = "1";
                    }
                    }
                    else
                    {
                        filtro();
                    txtMonto.Text = ((Convert.ToDouble(txtCantidad.Text)) * (Convert.ToDouble(txtCosto.Text))).ToString();
                }
                    // Buscar por el id
                   


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            
        }
        

        private void BtnAgregar_click(object sender, RoutedEventArgs e)
        { 
            double cantidad, precio, total;

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
                    LlenarFacturas();
                    Limpiar();
                
                    try
                    {
                        Producto.ModificarExistenciasVenta(Producto);
                        ayudasuma = ayudasuma + total;
                        txtsubtotal.Text = ayudasuma.ToString();
                        txttotal.Text = ayudasuma.ToString();
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                //tambien se puede mostrar la excepcion cambio 24/10/19
                //MessageBox.Show("<<Error>> No se pudo Realizar la venta <<Error>>");
                MessageBox.Show(ex.ToString(), "<<Error>> No se pudo Realizar la Venta <<Error>>");
            }

        }

        private void NuevaCompra()
        {
            // Verificar que todos los valores sean ingresados

            try
            {
                // Obtener los valores para la Venta
                Producto = ObtenerValoresFormulario();

                // Insertar los datos de la habitación
                Producto.Vender(Producto);

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

        private void NuevaFactura()
        {
           
            Factura.CFactura(Factura);
           Factura.MostrarNumeroFactura();
            txtfactura.Text = Factura.IdVenta.ToString();
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


        //Boton terminar ventas
        private void Agregaralcarrito_Copy_Click(object sender, RoutedEventArgs e)
        {
            NuevaFactura();
            LlenarFacturas();
            txtsubtotal.Text = string.Empty;
            txttotal.Text = string.Empty;
            ayudasuma = 0;
        }




        private void BtnVentas_Copy2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
            BtnVentas_Copy.Focus();
            }
            else if (e.Key== Key.RightCtrl)
            {
                Agregaralcarrito.Focus();
            }
            else
            {
               
            }
        }

        private void imprimir()
        {
           
        }

        private void LbProducto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Agregaralcarrito_Copy1_Click(object sender, RoutedEventArgs e)
        {
            LimpiarSoloText();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void iMPRIMIRFACTURA()
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Imprimir_Click(object sender, RoutedEventArgs e)
        {
            Impresion_de_Factura IF = new Impresion_de_Factura();
            IF.Show();
        }
    }
}
