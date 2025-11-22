

using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Security.Cryptography;

using WebApp.Models;
using WebApp.Models.ViewModels;


namespace WebApp.Metodos
{
    public class CondicionMetodo
    {


        public static CondicionMetodo _intance = null;
        public CondicionMetodo() { }


        public static CondicionMetodo Instance
        {


            get
            {
                if (_intance is null)
                {
                    _intance = new CondicionMetodo();

                }

                return _intance;
            }

        }


        public List<Condicion> Listar()
        {
            var lista = new List<Condicion>();

            try
            {
                using (SqlConnection sql = new SqlConnection(cnn.connection))
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerCondicion", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    sql.Open();

                    using (SqlDataReader rs = cmd.ExecuteReader())
                    {
                        while (rs.Read())
                        {
                            lista.Add(new Condicion
                            {
                                IdCondicion = rs.GetInt32(rs.GetOrdinal("IdCondicion")),
                                Descripcion = rs["Descripcion"]?.ToString(),
                                IsActive = rs.GetBoolean(rs.GetOrdinal("IsActive"))
                            });
                        }
                    }
                }

                return lista;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error SQL al obtener las condiciones: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al listar las condiciones: " + ex.Message, ex);
            }
        }



        public bool Register(Condicion obj)
        {

            bool rs = true;


            using (SqlConnection sql = new SqlConnection(cnn.connection))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_RegistrarCondicion", sql))
                    {
                        cmd.Parameters.AddWithValue("Descripsion", obj.Descripcion.ToString());
                        cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.CommandType = CommandType.StoredProcedure;

                        sql.Open();
                        cmd.BeginExecuteNonQuery();


                        rs = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                    }
                }
                catch (SqlException e)
                {
                    rs = false;
                    throw new Exception($"Error type {e.Message} ");

                }
                catch (Exception ex)
                {
                    rs = false;
                    throw new Exception($"Error Type {ex.Message}");

                }


                return rs;
            }

        }

          public bool Modificar(Condicion obj)
        {
            bool resultado = true;

            try
            {
                using (SqlConnection sql = new SqlConnection(cnn.connection))
                using (SqlCommand cmd = new SqlCommand("sp_ModificarCondicion", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Parámetros que envías al SP
                    cmd.Parameters.AddWithValue("IdCondicion", obj.IdCondicion);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Activo", obj.IsActive);

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


        public bool Eliminar(int Id)
        {


            bool rs = true;



            using (SqlConnection sql = new SqlConnection(cnn.connection))
            {
                try
                {
                    using (SqlCommand cmd = new SqlCommand("sp_EliminiarCondicion", sql))
                    {
                        cmd.Parameters.AddWithValue("IdCondicion", Id);
                        cmd.CommandType = CommandType.StoredProcedure;

                        
                        cmd.ExecuteNonQuery();

                        // resultado

                        rs = true;    
                    }
                }
                catch (SqlException ex)
                {   

                    rs = false;
                    throw new Exception($"Error type {ex.Message}");
                }
                catch (Exception ex)
                {
                    rs = false; 
                    throw new Exception($"Error type {ex.Message}");
                }
            }
            
            return rs;


        }

    }
}