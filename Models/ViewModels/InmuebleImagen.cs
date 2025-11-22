

namespace WebApp.Models.ViewModels
{
    public class InmuebleImagen
    {
        public int IdFoto { get; set; }
        public int IdInmueble { get; set; }
        public byte[] Imagen { get; set; }
    }
}