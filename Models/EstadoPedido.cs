using System;
using System.Collections.Generic;

namespace LaboAppWebV1._0._0.Models;

public partial class EstadoPedido
{
    public int IdEstado { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
