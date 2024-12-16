using System;
using System.Collections.Generic;

namespace LaboAppWebV1._0._0.Models;

public partial class Pedido
{
    public int IdPedido { get; set; }

    public int IdComanda { get; set; }

    public int IdProducto { get; set; }

    public int Cantidad { get; set; }

    public int IdEstado { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaFinalizacion { get; set; }

    public int TiempoEstimado { get; set; }

    public string? Observaciones { get; set; }

    public string? CodigoCliente { get; set; }

    public virtual Comanda IdComandaNavigation { get; set; } = null!;

    public virtual EstadoPedido IdEstadoNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
