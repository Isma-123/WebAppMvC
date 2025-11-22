

using Antlr.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.UI;
using WebApp.Models.ViewModels;

namespace WebApp.Metodos
{
    public class CiudadMetodo
    {

        private static CiudadMetodo _instance = null;  


        public CiudadMetodo() {  }
 

       public static CiudadMetodo Instance
        {
            get
            {
                if(_instance is null) _instance = new CiudadMetodo(); 

                return _instance;

            }
        }


        public List<Ciudad> Listar()
        {
            var rs = new List<Ciudad>(); 


            try
            {
                using (SqlConnection sql = new SqlConnection(cnn.connection))
                {
                    using(SqlCommand cmd = new SqlCommand("sp_ListarCiudades", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        sql.Open(); // conexion open 


                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {

                                rs.Add(new Ciudad()
                                {
                                    IdCiudad = rdr.GetInt32(rdr.GetOrdinal("IdCiudad")), 
                                    Nombre = rdr["Nombre"]?.ToString()

                                });
                               

                            }
                        }


                    }
                }
            } catch(Exception ex)
            {
                rs = null;
                throw new Exception($"Error type {ex.Message.ToString()} ");
            }


            return rs;

        }
    }
}