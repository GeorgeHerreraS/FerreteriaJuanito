using Microsoft.EntityFrameworkCore;

namespace FerreteriaJuanito.Clases
{
    public class CarroCompra
    {
        public int Id { get; set; }
        public int IdProducto { get; set; } = 1;
        public int IdUsuario { get; set; } = 1;
        public int IdMedioPago { get; set; } = 1;
        public int IdDespacho { get; set; } = 1;
        public int Cantidad { get; set; } = 1;
        public int Total { get; set; } = 0;
    }
}
