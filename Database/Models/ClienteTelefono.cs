﻿using System;
using System.Collections.Generic;

namespace Database.Models
{
    public partial class ClienteTelefono
    {
        public string Cedula { get; set; } = null!;
        public string Telefono { get; set; } = null!;

        public virtual Cliente CedulaNavigation { get; set; } = null!;
    }
}
