using System;
using System.Collections.Generic;

namespace Database.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Cita = new HashSet<Citum>();
        }

        public string? NombreCompleto { get; set; }
        public string Cedula { get; set; } = null!;
        public DateTime? FechaDeNacimientoC { get; set; }
        public string Usuario { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public int? Puntos { get; set; }

        public virtual ClienteDireccion? ClienteDireccion { get; set; }
        public virtual ClienteTelefono? ClienteTelefono { get; set; }
        public virtual ICollection<Citum> Cita { get; set; }
    }
}
