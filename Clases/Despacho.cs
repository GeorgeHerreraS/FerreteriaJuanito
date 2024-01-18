using Microsoft.EntityFrameworkCore;

namespace FerreteriaJuanito.Clases
{
    public class Despacho
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; } = 1;
        public String DescDespacho { get; set; } = String.Empty;
    }
}
