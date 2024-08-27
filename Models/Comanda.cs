using System;
using System.Collections.Generic;

namespace LaboAppWebV1._0._0.Models;

public partial class Comanda
{
    public int IdComandas { get; set; }

    public int IdMesa { get; set; }

    public string NombreCliente { get; set; } = null!;

    public virtual Mesa IdMesaNavigation { get; set; } = null!;

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
