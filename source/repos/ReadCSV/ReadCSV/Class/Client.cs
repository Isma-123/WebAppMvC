using CsvHelper.Configuration.Attributes;
using CsvHelper;

namespace ReadCSV.Class
{
    public class Client
    { 
        public int IdCliente { get; set; }
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;

    }
}
