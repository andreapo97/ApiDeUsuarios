using System.Numerics;

namespace ApiDeUsuarios.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public Int64 Telefono { get; set; }
        public int IdSex { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? SexName { get; set; }

    }
}
