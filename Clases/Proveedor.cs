using Microsoft.EntityFrameworkCore;

namespace FerreteriaJuanito.Clases
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string NombreProveedor { get; set; } = string.Empty;
        public string DireccionProveedor { get; set; } = string.Empty;
        public string TelefonoProveedor { get; set; } = string.Empty;
    }
}
