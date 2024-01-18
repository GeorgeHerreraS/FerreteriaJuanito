namespace FerreteriaJuanito.Clases.Utils
{
    public class Logs
    {
        public int Id { get; set; }
        public string Clase { get; set; } = string.Empty;
        public DateTime FechaLog { get; set; }
        public string LogName { get; set; } = string.Empty;
        public string LogDescription { get; set; } = string.Empty;
        public int IdUsuario { get; set; } 
    }
}
