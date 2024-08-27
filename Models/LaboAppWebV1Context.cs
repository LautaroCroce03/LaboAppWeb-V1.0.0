using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LaboAppWebV1._0._0.Models;

public partial class LaboAppWebV1Context : DbContext
{
    public LaboAppWebV1Context()
    {
    }

    public LaboAppWebV1Context(DbContextOptions<LaboAppWebV1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Comanda> Comandas { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<EstadoMesa> EstadoMesas { get; set; }

    public virtual DbSet<EstadoPedido> EstadoPedidos { get; set; }

    public virtual DbSet<Mesa> Mesas { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sectore> Sectores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=WIN10ESTUDIANTE; Database=LaboAppWebV1; User id=sa; Password=Bostero10; Trust Server Certificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comanda>(entity =>
        {
            entity.HasKey(e => e.IdComandas);

            entity.ToTable("comandas");

            entity.Property(e => e.IdComandas).HasColumnName("id_comandas");
            entity.Property(e => e.IdMesa).HasColumnName("id_mesa");
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nombre_cliente");

            entity.HasOne(d => d.IdMesaNavigation).WithMany(p => p.Comanda)
                .HasForeignKey(d => d.IdMesa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_comandas_mesas");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado);

            entity.ToTable("empleados");

            entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.IdSector).HasColumnName("id_sector");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("usuario");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_empleados_roles");

            entity.HasOne(d => d.IdSectorNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdSector)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_empleados_sectores");
        });

        modelBuilder.Entity<EstadoMesa>(entity =>
        {
            entity.HasKey(e => e.IdEstado);

            entity.ToTable("estado_mesas");

            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<EstadoPedido>(entity =>
        {
            entity.HasKey(e => e.IdEstado);

            entity.ToTable("estado_pedidos");

            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Mesa>(entity =>
        {
            entity.HasKey(e => e.IdMesa);

            entity.ToTable("mesas");

            entity.Property(e => e.IdMesa).HasColumnName("id_mesa");
            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Mesas)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_mesas_estado_mesas");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.IdPedido);

            entity.ToTable("pedidos");

            entity.Property(e => e.IdPedido)
                .ValueGeneratedNever()
                .HasColumnName("id_pedido");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaFinalizacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_finalizacion");
            entity.Property(e => e.IdComanda).HasColumnName("id_comanda");
            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.IdProducto).HasColumnName("id_producto");

            entity.HasOne(d => d.IdComandaNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdComanda)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pedidos_comandas");

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pedidos_estado_pedidos");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pedidos_productos");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto);

            entity.ToTable("productos");

            entity.Property(e => e.IdProducto).HasColumnName("id_producto");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdSector).HasColumnName("id_sector");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(15, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.HasOne(d => d.IdSectorNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdSector)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_productos_sectores");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol);

            entity.ToTable("roles");

            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Sectore>(entity =>
        {
            entity.HasKey(e => e.IdSector);

            entity.ToTable("sectores");

            entity.Property(e => e.IdSector).HasColumnName("id_sector");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
