

using System.Collections.Generic;
using WebApp.Models.ViewModels;
using WebApp.Metodos;


using WebApp.Controllers;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions;
using System.Data;


namespace WebApp.Metodos
{
    public class TipoPropiedadMetodo // metodo
    {
        private static TipoPropiedadMetodo _instance = null; 


        public TipoPropiedadMetodo() { }


        public static TipoPropiedadMetodo Instance
        {
            get { 
                 
                if(_instance == null )
                {
                    _instance = new TipoPropiedadMetodo();
                }
                
               return _instance;
            }
        }


        public List<TipoPropiedad> Listar()
        {
            var lista = new List<TipoPropiedad>();

            try
            {
                using (SqlConnection sql = new SqlConnection(cnn.connection))
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerTipoPropiedad", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    sql.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new TipoPropiedad
                            {
                                IdTipoPropiedad = dr.GetInt32(dr.GetOrdinal("IdTipoPropiedad")),
                                Descripcion = dr["Descripcion"]?.ToString(),
                                Activo = dr.GetBoolean(dr.GetOrdinal("Activo")),
                                FechaRegistro = dr.GetDateTime(dr.GetOrdinal("FechaRegistro"))

                            });
                        }
                    }
                }

                return lista;
            }
            catch (SqlException ex)
            {
                
                throw new Exception("Error SQL al obtener los tipos de propiedad: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                // Cualquier otro error inesperado
                throw new Exception("Ocurrió un error inesperado al listar los tipos de propiedad: " + ex.Message, ex);
            }
        }



        public bool Register(TipoPropiedad propiedad)
        {
            bool rs = true;


            using (SqlConnection sql = new SqlConnection(cnn.connection))
            {
                try
                {
                      using(SqlCommand cmd = new SqlCommand("sp_RegisterProperty", sql))
                    {

                        cmd.Parameters.AddWithValue("Descripsion", propiedad.Descripcion);
                        cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.CommandType = CommandType.StoredProcedure;

                        sql.Open();
                        cmd.ExecuteNonQuery();

                        rs = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);    
                      
                    }
                }
                catch (Exception ex)
                {
                    rs = false;
                    throw new Exception($"Error type {ex.Message} ");
                }


                return rs;
            } 
        }


        public bool Modificar(TipoPropiedad obj)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection sql = new SqlConnection(cnn.connection))
                using (SqlCommand cmd = new SqlCommand("sp_ModificarCondicion", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros que envías al SP
                    cmd.Parameters.AddWithValue("IdPropiedad", obj.IdTipoPropiedad);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Activo",obj.Activo);
                    cmd.Parameters.AddWithValue("FechaRegistro", obj.FechaRegistro);



                    // Parámetro OUTPUT
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    sql.Open();
                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al modificar la condición: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al modificar la condición: " + ex.Message, ex);
            }

            return resultado;
        }



        public bool Eliminar(int id)
        {
            bool resultado = true;



            using(SqlConnection sql = new SqlConnection(cnn.connection))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_EliminarCondicion", sql))
                    {

                        cmd.Parameters.AddWithValue("IdPropiedad", id);
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        cmd.BeginExecuteNonQuery();


                        resultado = true; 
                    }   
                }
                catch (SqlException ex)
                {
                    resultado = false;
                    throw new Exception($"Connection expception {ex.Message} ");
                }
                catch (Exception ex2)
                {
                    resultado = false;
                }
            } 

            return resultado; 

        }
    }
}
    


