using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace FerreteriaJuanito.Clases
{
    public class Producto
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; } = 1;
        public string DescProducto { get; set; } = string.Empty;
        public int ValorProducto { get; set; } = 0;
        public int CantidadProducto { get; set; } = 0;
    }
}
