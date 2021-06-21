using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Agregando namespaces necesarios
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BaseBPB
{
    public class ClaseFactura
    {

        //Conexion Sql
        private static string connectionString = ConfigurationManager.ConnectionStrings["BaseBPB.Properties.Settings.BPBConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        //Propiedades    public decimal precio { get; set; }
        public int IdVenta { get; set; }
        public string Producto { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Monto { get; set; }
      
        public DateTime Fecha = DateTime.Now;
        private decimal Total { get; set; }

        //Crear Nueva Factura
        public void CFactura(ClaseFactura Factura)
        {
            try
            {





                // Query de inserción
                string query = @"INSERT INTO Ventas.Venta ( Fecha_Venta,Total_Venta)
                                 VALUES (@Fecha,@Total)";

                // Abrir la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

               // Fecha = DateTime.Now;
                Total = 0;
           

                // Establecer los valores de los parámetros
                //sqlCommand.Parameters.AddWithValue("@ID", Factura.IdVenta);
                sqlCommand.Parameters.AddWithValue("@Fecha", Factura.Fecha);
                sqlCommand.Parameters.AddWithValue("@Total", Factura.Total);
             

                // Ejecutar el comando de inserción
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                // Cerrar la conexión
                sqlConnection.Close();
            }

           


        }

        public ClaseFactura MostrarNumeroFactura()
        {
            ClaseFactura numero = new ClaseFactura();

            try
            {
                // Query de selección
                string query = @"select * from Ventas.Venta WHERE IdVenta = (SELECT MAX(IdVenta) from Ventas.Venta)";

                // Abrir la conexión
                sqlConnection.Open();

                // Establecer el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {

                        IdVenta = Convert.ToInt32(rdr["IdVenta"]);

                    }
                }
                return numero;

               /* Ventas ven = new Ventas();
                ven.txtfactura.Text = IdVenta.ToString();*/
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Cerrar la conexión
                sqlConnection.Close();
            }

        }

        public List<ClaseFactura> MostrarFacturas()
        {
            // Inicializar una lista de habitaciones
            List<ClaseFactura> facturas = new List<ClaseFactura>();

            try
            {
                // Query de selección
                string query = @"select * from Facturas where Factura= @numero";

                
                // Abrir la conexión
                sqlConnection.Open();
                
                // Establecer el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@numero",IdVenta);
                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        //ojoooooooooooo
                        facturas.Add(new ClaseFactura { IdVenta = Convert.ToInt32(rdr["Factura"]), Producto = rdr["Producto"].ToString(), Precio = Convert.ToDecimal(rdr["Precio"]), Cantidad = Convert.ToInt32(rdr["Cantidad"]), Monto = Convert.ToDecimal(rdr["Monto"]) });
                    }
                }
             
      
                return facturas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Cerrar la conexión
                sqlConnection.Close();
            }
        }






    }
}
