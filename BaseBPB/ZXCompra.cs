using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//AGREGANDO NAME SPACES PARA SQL
using System.Data;
using System.Data.SqlClient;
using System.Configuration;



namespace BaseBPB
{

    class ZXCompra
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["BaseBPB.Properties.Settings.BPBConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        public string IdProducto { get; set; }
        public string NombreProducto { get; set;}
       public string CantidadProducto { get; set; }
        public string PrecioCosto { get; set; }

        // Constructores
        public ZXCompra() { }

        public ZXCompra(string Nombre, String Id)
        {
            IdProducto = Id;
            NombreProducto = Nombre;
           
        } 



        public List<ZXCompra> MostrarPrecio()
        {
            List<ZXCompra> Productos = new List<ZXCompra>();
            try
            {
                // Query de selección
                string query = @"SELECT Idproducto,Nombre,Cantidad,PrecioVenta FROM Productos.Producto";

                // Abrir la conexión
                sqlConnection.Open();

                // Establecer el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        Productos.Add(new ZXCompra { IdProducto = rdr["IdProducto"].ToString(), NombreProducto = rdr["Nombre"].ToString(),CantidadProducto=rdr["Cantidad"].ToString()});
                    }
                }

                return Productos;
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
