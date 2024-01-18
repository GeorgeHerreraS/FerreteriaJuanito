using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FerreteriaJuanito.Clases
{
    public class MedioPago
    {
        public int Id { get; set; }
        public string DescMedioPago { get; set; } = string.Empty;
    }
}
