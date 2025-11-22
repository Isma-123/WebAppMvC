using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApp.Metodos
{
    public class InmueblesMetodo
    {
        private static InmueblesMetodo _instance = null;

        public InmueblesMetodo() { }

        public static InmueblesMetodo Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InmueblesMetodo();
                }

                return _instance;
            }
        }


        public List<Inmueble> Listar()
        {
            var rs = new List<Inmueble>();


            using (SqlConnection sql = new SqlConnection(cnn.connection)) /// abro la conexion
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ListarInMueble", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        // open conextion 
                        sql.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                rs.Add(new Inmueble()
                                {
                                    IdInmueble = reader.GetInt32(reader.GetOrdinal("IdInmueble")),
                                    IdCiudad = reader.GetInt32(reader.GetOrdinal("IdCiudad")),
                                    IdCondicion = reader.GetInt32(reader.GetOrdinal("IdCondicion")),
                                    Descripcion = reader["Descripcion"]?.ToString(),
                                    Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                                    Habitacion = reader.GetInt32(reader.GetOrdinal("Habitacion")),
                                    Baños = reader.GetInt32(reader.GetOrdinal("Baños")),
                                    TipoNegocio = reader["Tipodenegocio"]?.ToString(),
                                    NombreInmueble = reader["NombreInmueble"]?.ToString(),
                                    Direccion = reader["Direccion"]?.ToString(),
                                    IdTipoPropiedad = reader.GetInt32(reader.GetOrdinal("IdTipoPropiedad")),
                                    Activo = reader.GetBoolean(reader.GetOrdinal("Activo"))

                                });
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {

                    rs = null;
                    throw new Exception($"Error Type {ex.Message} listing the Inmuebles ");

                }
                catch (Exception ex2)
                {
                    rs = null;
                    throw new Exception($"Error Type {ex2.Message} listing the Inmuebles ");

                }
            }

            return rs;
        }

        public bool Register(Inmueble obj)
        {
            bool rs = true;

            using (SqlConnection sql = new SqlConnection(cnn.connection))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_RegisterInmueble", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Abrir conexión
                        sql.Open();

                        // Parámetros
                        cmd.Parameters.AddWithValue("@NombreInmueble", obj.NombreInmueble);
                        cmd.Parameters.AddWithValue("@Direccion", obj.Direccion);
                        cmd.Parameters.AddWithValue("@IdTipoPropiedad", obj.IdTipoPropiedad);
                        cmd.Parameters.AddWithValue("@IdCondicion", obj.IdCondicion);
                        cmd.Parameters.AddWithValue("@IdCiudad", obj.IdCiudad);
                        cmd.Parameters.AddWithValue("@Precio", obj.Precio);
                        cmd.Parameters.AddWithValue("@Habitacion", obj.Habitacion);
                        cmd.Parameters.AddWithValue("@Baños", obj.Baños);
                        cmd.Parameters.AddWithValue("@Descripcion", (object)obj.Descripcion ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@TipoNegocio", obj.TipoNegocio);


                        cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                        // Ejecutar
                        cmd.ExecuteNonQuery();

                        rs = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                        

                    }
                }
                catch (Exception ex)
                {
                    rs = false;
                    throw new Exception($"Error al registrar el inmueble: {ex.Message}");
                }
            }

            return rs;
        }

        public bool Modify(Inmueble obj)
        {
            bool rs = true;

            using (SqlConnection sql = new SqlConnection(cnn.connection))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ModificarInmueble", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        sql.Open();

                        // Parámetros normales
                        cmd.Parameters.AddWithValue("@IdInmueble", obj.IdInmueble);
                        cmd.Parameters.AddWithValue("@NombreInmueble", obj.NombreInmueble);
                        cmd.Parameters.AddWithValue("@Direccion", obj.Direccion);
                        cmd.Parameters.AddWithValue("@IdTipoPropiedad", obj.IdTipoPropiedad);
                        cmd.Parameters.AddWithValue("@IdCondicion", obj.IdCondicion);
                        cmd.Parameters.AddWithValue("@IdCiudad", obj.IdCiudad);
                        cmd.Parameters.AddWithValue("@Precio", obj.Precio);
                        cmd.Parameters.AddWithValue("@Habitacion", obj.Habitacion);
                        cmd.Parameters.AddWithValue("@Baños", obj.Baños);

                        if (obj.Descripcion == null)
                            cmd.Parameters.AddWithValue("@Descripcion", DBNull.Value);
                        else
                            cmd.Parameters.AddWithValue("@Descripcion", obj.Descripcion);

                        cmd.Parameters.AddWithValue("@TipoNegocio", obj.TipoNegocio);

                        // PARAM OUTPUT
                        cmd.Parameters.Add("@Resultado", SqlDbType.Bit)
                                      .Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery(); 

                        // Recuperar OUTPUT
                        rs = Convert.ToBoolean(cmd.Parameters["@Resultado"].Value);
                    }
                }
                catch (Exception ex)
                {
                    rs = false;
                    throw new Exception("Error al modificar inmueble: " + ex.Message);
                }
            }

            return rs;
        }


        public bool Remove(int id)
        {
            bool rs = false;


            using (SqlConnection sql = new SqlConnection(cnn.connection)) {
                try
                {   
                    sql.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarInMuebles", sql);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("IdInmueble", id); 

                    cmd.ExecuteNonQuery();
                }
                catch
                (Exception ex)
                {
                    rs = false; 
                    throw new Exception($"Error type {ex.Message }");    

                }
            }

            return rs; 

        }
    }
}
