using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using TiendaApi.Conexion;
using TiendaApi.Modelo;
namespace TiendaApi.Datos
{
    public class Dproductos
    {
        Conexionbd cn = new Conexionbd();
        public async Task <List<Mproductos>> Mostrarproductos()
        {
            var lista = new List<Mproductos>();
            //PARA LA CONEXION
            using(var sql = new SqlConnection(cn.cadenaSQL()))
            {
                //EJECUTAR EL PROCEDIMIENTO ALMACENADO
                using(var cmd = new SqlCommand("mostrarProductos", sql))
                {
                    await sql.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    //PARA HACER EL RECORRIDO DE DATOS
                    using(var item = await cmd.ExecuteReaderAsync())
                    {
                        while(await item.ReadAsync())
                        {
                            var mproductos = new  Mproductos();
                            mproductos.id = (int)item["id"];
                            mproductos.decripcion = (string)item["decripcion"];
                            mproductos.precio = (decimal)item["precio"];
                            mproductos.nombre = (string)item["nombre"];
                            lista.Add(mproductos);
                        }
                    }
                }
            }
            return lista;
        }
        public async Task InsertarProductos(Mproductos parametros)
        {
            using(var sql = new SqlConnection (cn.cadenaSQL()))
            {
                using(var cmd = new SqlCommand("insertarProductos", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue
                        ("@decripcion", parametros.decripcion);
                    cmd.Parameters.AddWithValue("@precio", parametros.precio);
                    cmd.Parameters.AddWithValue("@nombre", parametros.nombre);
                    await sql.OpenAsync();
                    await cmd.ExecuteReaderAsync();
                }
            }
        }
        public async Task editarProductos(Mproductos parametros)
        {
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("editarProductos", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", parametros.id);
                    cmd.Parameters.AddWithValue("precio", parametros.precio);
                    cmd.Parameters.AddWithValue("@nombre", parametros.nombre);
                    await sql.OpenAsync();
                    await cmd.ExecuteReaderAsync();
                }
            }
        }
        public async Task eliminarProductos(Mproductos parametros)
        {
            using (var sql = new SqlConnection(cn.cadenaSQL()))
            {
                using (var cmd = new SqlCommand("eliminarProductos", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("id", parametros.id);
                    await sql.OpenAsync();
                    await cmd.ExecuteReaderAsync();
                }
            }
        }
    }
}

