using System;
using System.Collections.Generic;

namespace LaboAppWebV1._0._0.Models;

public partial class Mesa
{
    public int IdMesa { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdEstado { get; set; }

    public virtual ICollection<Comanda> Comanda { get; set; } = new List<Comanda>();

    public virtual EstadoMesa IdEstadoNavigation { get; set; } = null!;
}
