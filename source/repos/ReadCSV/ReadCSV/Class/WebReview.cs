

namespace ReadCSV.Class
{
    public class WebReview
    {
        public int IdReview { get; set; }
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public DateTime Fecha { get; set; }
        public string? Comentario { get; set; }
        public int Rating { get; set; }
    }
}
