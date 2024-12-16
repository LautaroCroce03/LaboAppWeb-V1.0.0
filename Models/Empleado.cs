using System;
using System.Collections.Generic;

namespace LaboAppWebV1._0._0.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string Nombre { get; set; } = null!;

    public string Usuario { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int IdSector { get; set; }

    public int IdRol { get; set; }

    public bool? Estado { get; set; }

    public virtual Role IdRolNavigation { get; set; } = null!;

    public virtual Sectore IdSectorNavigation { get; set; } = null!;
}
