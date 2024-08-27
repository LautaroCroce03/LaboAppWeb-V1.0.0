using System;
using System.Collections.Generic;

namespace LaboAppWebV1._0._0.Models;

public partial class Sectore
{
    public int IdSector { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
