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
    class ClaseProducto
    {
        //Conexion Sql
        private static string connectionString = ConfigurationManager.ConnectionStrings["BaseBPB.Properties.Settings.BPBConnectionString"].ConnectionString;
        private SqlConnection sqlConnection = new SqlConnection(connectionString);

        //Propiedades

        public string IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public int Venta { get; set; }
        public double PrecioVenta { get; set; }

        public double Descuento { get; set; }
        public double PrecioCosto { get; set; }

        public int Proveedor { get; set; }
        public int Categoria { get; set; }

        public string Producto { get; set; }
        public double Cant { get; set; }

        public double Total { get; set; }



        //Constructores
        public ClaseProducto() { }

        public ClaseProducto(string Id, string nombre, int cantidad, double venta, double costo, int prov, int cat, double cant, double total)
        {
            IdProducto = Id;
            NombreProducto = nombre;
            Cantidad = cantidad;
            PrecioCosto = costo;
            PrecioVenta = venta;
            Proveedor = prov;
            Categoria = cat;
            Cant = cant;
            Total = total;
        }

        //Metodos
        public List<ClaseProducto> MostrarProductos()
        {
            // Inicializar una lista de habitaciones
            List<ClaseProducto> productos = new List<ClaseProducto>();

            try
            {
                // Query de selección
                string query = @" Select * from Productos.Producto  order by Nombre asc";

                // Abrir la conexión
                sqlConnection.Open();

                // Establecer el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        //ojoooooooooooo
                        productos.Add(new ClaseProducto { IdProducto = (rdr["IdProducto"]).ToString(), NombreProducto = rdr["Nombre"].ToString(), PrecioCosto = Convert.ToDouble(rdr["PrecioCosto"]), PrecioVenta = Convert.ToDouble(rdr["PrecioVenta"]), Cantidad = Convert.ToInt32(rdr["Cantidad"]) });
                    }
                }

                return productos;
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



        public void InsertarProducto(ClaseProducto producto)
        {
            try
            {
                // Query de inserción
                string query = @"INSERT INTO Productos.Producto (IdProducto,Nombre, PrecioVenta, PrecioCosto,Cantidad,Proveedor,Categoria)
                                 VALUES (@IdProducto,@Nombre, @PrecioVenta, @PrecioCosto,@Cantidad,@Proveedor,@categoria)";

                // Abrir la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                sqlCommand.Parameters.AddWithValue("@Nombre", producto.NombreProducto);
                sqlCommand.Parameters.AddWithValue("@PrecioVenta", producto.PrecioVenta);
                sqlCommand.Parameters.AddWithValue("@PrecioCosto", producto.PrecioCosto);
                sqlCommand.Parameters.AddWithValue("@Cantidad", producto.Cantidad);
                sqlCommand.Parameters.AddWithValue("@Proveedor", producto.Proveedor);
                sqlCommand.Parameters.AddWithValue("@Categoria", producto.Categoria);
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

        public void ModificarProducto(ClaseProducto producto)
        {
            try
            {
                // Query de inserción
                string query = @"UPDATE Productos.Producto
                                 SET Nombre = @Nombre, PrecioVenta = @PrecioVenta, PrecioCosto = @PrecioCosto, Categoria = @Categoria, Proveedor= @Proveedor, Cantidad = @Cantidad
                                 WHERE IdProducto = @IdProducto";

                // Abrir la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                sqlCommand.Parameters.AddWithValue("@Nombre", producto.NombreProducto);
                sqlCommand.Parameters.AddWithValue("@PrecioCosto", producto.PrecioCosto);
                sqlCommand.Parameters.AddWithValue("@PrecioVenta", producto.PrecioVenta);
                sqlCommand.Parameters.AddWithValue("@Cantidad", producto.Cantidad);
                sqlCommand.Parameters.AddWithValue("@Proveedor", producto.Proveedor);
                sqlCommand.Parameters.AddWithValue("@Categoria", producto.Categoria);

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


        public void EliminarProducto(ClaseProducto producto)
        {
            try
            {
                // Query de inserción
                string query = @"Delete Productos.Producto
                                 WHERE IdProducto = @IdProducto";

                // Abrir la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

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

        public ClaseProducto BuscarProducto(string id)
        {
            ClaseProducto habitacion = new ClaseProducto();

            try
            {
                // Query de inserción
                string query = @"SELECT * FROM Productos.Producto
                                 WHERE IdProducto = @IdProducto";

                // Abrir la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("@IdProducto", id);

                using (SqlDataReader rdr = sqlCommand.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        habitacion.IdProducto = (rdr["IdProducto"]).ToString();
                        habitacion.NombreProducto = rdr["Nombre"].ToString();
                        habitacion.PrecioVenta = Convert.ToDouble(rdr["PrecioVenta"]);
                        habitacion.PrecioCosto = Convert.ToDouble(rdr["PrecioCosto"]);
                        habitacion.Cantidad = Convert.ToInt32(rdr["Cantidad"]);
                        habitacion.Proveedor = Convert.ToInt32(rdr["Proveedor"]);
                        habitacion.Categoria = Convert.ToInt32(rdr["Categoria"]);

                    }
                }

                return habitacion;
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

        //RegistrarNuevaCompra
        public void Comprar(ClaseProducto producto)
        {
            try
            {
                // Query de inserción
                string query = @"INSERT INTO Compras.Compra (Producto, Cantidad, PrecioUnitario,Total)
                                 VALUES (@Producto,@Cantidad, @PrecioUnitario,@Total)";

                // Abrir la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("@Producto", producto.IdProducto);
                sqlCommand.Parameters.AddWithValue("@Cantidad", producto.Cant);
                sqlCommand.Parameters.AddWithValue("@PrecioUnitario", producto.PrecioCosto);
                sqlCommand.Parameters.AddWithValue("@Total", producto.Total);

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




        public void Vender(ClaseProducto producto)
        {
            try
            {
                // Query de inserción
                string query = @"INSERT INTO Ventas.DetalleVentas (Producto, Cantidad, PrecioUnitario,Descuento,Venta)
                                 VALUES (@Producto,@Cantidad, @PrecioUnitario,@descuento,@Venta)";

                // Abrir la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros
                sqlCommand.Parameters.AddWithValue("@Producto", producto.IdProducto);
                sqlCommand.Parameters.AddWithValue("@Cantidad", producto.Cant);
                sqlCommand.Parameters.AddWithValue("@PrecioUnitario", producto.PrecioCosto);
                sqlCommand.Parameters.AddWithValue("@Venta", producto.Venta);
                sqlCommand.Parameters.AddWithValue("@descuento", producto.Descuento);
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



        // SE QUITA UNA CIERTA CANTIDAD DE PRODUCTOS
        public void ModificarExistenciasVenta(ClaseProducto Venta)
        {
            try
            {
                // Query de inserción
                string query = @"update Productos.Producto set Cantidad=Cantidad - @Cantidad Where IdProducto = @IdProducto";

                // Abrir la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros


                sqlCommand.Parameters.AddWithValue("@IdProducto", Venta.IdProducto);
                sqlCommand.Parameters.AddWithValue("@Cantidad", Venta.Cant);


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


        //SE AGREGA UNA CIERTA CANTIDAD DE PRODUCTOS
        public void ModificarExistenciasCompra(ClaseProducto Compra)
        {
            try
            {
                // Query de inserción
                string query = @"update Productos.Producto set Cantidad=Cantidad + @Cantidad Where IdProducto = @IdProducto";

                // Abrir la conexión
                sqlConnection.Open();

                // Crear el comando SQL
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                // Establecer los valores de los parámetros


                sqlCommand.Parameters.AddWithValue("@IdProducto", Compra.IdProducto);
                sqlCommand.Parameters.AddWithValue("@Cantidad", Compra.Cant);


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




    }
}
