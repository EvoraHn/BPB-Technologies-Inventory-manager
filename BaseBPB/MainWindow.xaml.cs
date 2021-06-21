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
using System.Windows.Threading;

namespace BaseBPB
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    /// 
    //RETO TRAINEZ
    //32009319
    //BREISY LICONA 
    //WWW.CBC.CO

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Reloj();
        }

        private void BtnVentas_Copy2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Reloj()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Relojlbl.Content = DateTime.Now.ToLongTimeString();
        }

        private void BtnInventario_Click(object sender, RoutedEventArgs e)
        {
            /*string valor1;
            valor1 = " ";
            Producto ventana = new Producto(valor1);
            ventana.ShowDialog();*/
            InventarioCompleto Formulario = new InventarioCompleto();
            Formulario.ShowDialog();
            //Ventas ventana = new Ventas();
            //ventana.ShowDialog();
        }

        private void BtnCompras_Click(object sender, RoutedEventArgs e)
        {
            Compras ventana = new Compras();
            ventana.ShowDialog();

        }

        private void BtnVentas_Click(object sender, RoutedEventArgs e)
        {
            Ventas ventana = new Ventas();
            ventana.ShowDialog();
        }
        private void BtnProducto_Click(object sender, RoutedEventArgs e)
        {
            string valor1;
            valor1 = " ";
            Producto ventana = new Producto(valor1);
            ventana.ShowDialog();
        }

    }
}
