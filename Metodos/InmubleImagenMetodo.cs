

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebApp.Models.ViewModels;

namespace WebApp.Metodos
{
    public class InmubleImagenMetodo
    {
        private static InmubleImagenMetodo instance = null; 

        public static InmubleImagenMetodo Instance
        {
            get
            {

                if (instance == null)
                {
                    instance = new InmubleImagenMetodo();
                }

                return instance;


            } 
        }

        public List<InmuebleImagen> ListarImagen()
        {
            var ls = new List<InmuebleImagen>();

            try
            {
                using (SqlConnection sql = new SqlConnection(cnn.connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ListarImagenInmueble", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        sql.Open();

                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            ls.Add(new InmuebleImagen()
                            {
                                IdFoto = reader.GetInt32(reader.GetOrdinal("IdFoto")),
                                IdInmueble = reader.GetInt32(reader.GetOrdinal("IdInmueble")),
                                Imagen = (byte[])reader["Imagen"]  // leo las imagenes
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al listar imágenes: {ex.Message}");
            }

            return ls;
        }


        public bool Register(InmuebleImagen imagen)
        {
            bool rs = true; 


            try
            {

                using (SqlConnection Sql = new SqlConnection(cnn.connection))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_RegisterImagen", Sql))
                    {
                        cmd.CommandType= CommandType.StoredProcedure;
                        Sql.Open();

                        cmd.Parameters.AddWithValue("@IdInmeubleFoto", imagen.IdInmueble);
                        cmd.Parameters.AddWithValue("@IdFoto", imagen.IdFoto);
                        cmd.Parameters.AddWithValue("@Imagen", imagen.Imagen);


                        cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;


                        rs = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                        cmd.ExecuteNonQuery();
                    }

                }


            } catch(Exception ex)
            {
                rs = false;
                throw new Exception($"Error type {ex.Message}");
            }


            return rs;
        } 


        public bool EditImagen(InmuebleImagen imagen)
        {
            bool rs = true;

            try
            {
                using (SqlConnection sql = new SqlConnection(cnn.connection))
                {
                    using(SqlCommand cmd = new SqlCommand("sp_ImagenEdit", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        sql.Open();
                        cmd.Parameters.AddWithValue("@IdInmueble", imagen.IdInmueble);
                        cmd.Parameters.AddWithValue("@IdFoto", imagen.IdFoto);
                        cmd.Parameters.AddWithValue("@Imagen", imagen.Imagen);

                        cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;

                        rs = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                        cmd.ExecuteNonQuery();
                        
                    }
                }
            }
            catch (Exception ex)
            {
                rs = false;
                throw new Exception($"Error type {ex.Message}");
            }

            return rs;
        } 

        public bool Remove(int id)
        {
            bool rs = true; 

            try
            {
                using(SqlConnection sql = new SqlConnection(cnn.connection))
                {
                    using(SqlCommand cmd = new SqlCommand("sp_RemoveImagen", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure; 
                        sql.Open();

                        cmd.Parameters.AddWithValue("IdInmueble", id);

                        cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction= ParameterDirection.Output;

                        rs = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                        cmd.ExecuteNonQuery();

                    }
                }


                
            } catch(Exception ex)
            {
                rs = false;
                throw new Exception($"Error {ex.Message}");
            }
            return rs;
        }
    }
}