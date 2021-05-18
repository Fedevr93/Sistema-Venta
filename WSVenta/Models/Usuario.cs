using System;
using System.Collections.Generic;

#nullable disable

namespace WSVenta.Models
{
    public partial class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }
        public string Nombre { get; set; }
    }
}
