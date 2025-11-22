using System.Collections.Generic;
using System.Configuration;


namespace WebApp.Metodos
{
    public class cnn
    {

        public static string connection => ConfigurationManager
            .ConnectionStrings["cnn_conectionString"].ConnectionString;



    }
}