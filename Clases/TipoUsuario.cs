using Microsoft.EntityFrameworkCore;

namespace FerreteriaJuanito.Clases
{
    public class TipoUsuario
    {
        public int Id { get; set; }
        public string DescTipoUsuario { get; set; } = string.Empty;
    }
}
