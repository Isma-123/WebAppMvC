

namespace ReadCSV.Class
{
    public class Survey
    {
        public int IdOpinion { get; set; }
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public DateTime Fecha { get; set; }
        public string? Comentario { get; set; }
        public string? Clasificación { get; set; }
        public int PuntajeSatisfacción { get; set; }
        public string? Fuente { get; set; }
    }
}
