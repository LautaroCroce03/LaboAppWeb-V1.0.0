using Microsoft.EntityFrameworkCore;

namespace LaboAppWebV1._0._0.DataAccess
{


    public partial class LaboAppWebV1Context : DbContext
    {
        public LaboAppWebV1Context()
        {
        }

        public LaboAppWebV1Context(DbContextOptions<LaboAppWebV1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Models.Comanda> Comandas { get; set; }

        public virtual DbSet<Models.Empleado> Empleados { get; set; }

        public virtual DbSet<Models.EstadoMesa> EstadoMesas { get; set; }

        public virtual DbSet<Models.EstadoPedido> EstadoPedidos { get; set; }

        public virtual DbSet<Models.Mesa> Mesas { get; set; }

        public virtual DbSet<Models.Pedido> Pedidos { get; set; }

        public virtual DbSet<Models.Producto> Productos { get; set; }

        public virtual DbSet<Models.Role> Roles { get; set; }

        public virtual DbSet<Models.Sectore> Sectores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Comanda>(entity =>
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

            modelBuilder.Entity<Models.Empleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado);

                entity.ToTable("empleados");

                entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");
                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasDefaultValueSql("((1))")
                    .HasColumnName("estado");
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

            modelBuilder.Entity<Models.EstadoMesa>(entity =>
            {
                entity.HasKey(e => e.IdEstado);

                entity.ToTable("estado_mesas");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");
                entity.Property(e => e.Descripcion)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<Models.EstadoPedido>(entity =>
            {
                entity.HasKey(e => e.IdEstado);

                entity.ToTable("estado_pedidos");

                entity.Property(e => e.IdEstado).HasColumnName("id_estado");
                entity.Property(e => e.Descripcion)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<Models.Mesa>(entity =>
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

            modelBuilder.Entity<Models.Pedido>(entity =>
            {
                entity.HasKey(e => e.IdPedido);

                entity.ToTable("pedidos");

                entity.Property(e => e.IdPedido).HasColumnName("id_pedido");
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

            modelBuilder.Entity<Models.Producto>(entity =>
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

            modelBuilder.Entity<Models.Role>(entity =>
            {
                entity.HasKey(e => e.IdRol);

                entity.ToTable("roles");

                entity.Property(e => e.IdRol).HasColumnName("id_rol");
                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<Models.Sectore>(entity =>
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

}
