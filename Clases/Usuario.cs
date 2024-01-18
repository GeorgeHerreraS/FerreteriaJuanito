using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FerreteriaJuanito.Clases
{
    public class Usuario
    {
        public int Id { get; set; }
        public int IdTipoUsuario { get; set; } = 1;
        public string NombreUsuario { get; set; } = string.Empty;
        public string DireccionUsuario { get; set; } = string.Empty;
        public string TelefonoUsuario { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = "Ferreteria2024".ToString();
        

    }
}
