using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Order2GoV2.Models
{
    public partial class Proyecto1Context : DbContext
    {
        public Proyecto1Context()
        {
        }

        public Proyecto1Context(DbContextOptions<Proyecto1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Comercio> Comercio { get; set; }
        public virtual DbSet<ComercioUsuario> ComercioUsuario { get; set; }
        public virtual DbSet<DetalleVenta> DetalleVenta { get; set; }
        public virtual DbSet<Inventario> Inventario { get; set; }
        public virtual DbSet<Perfil> Perfil { get; set; }
        public virtual DbSet<Productos> Productos { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Venta> Venta { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-KIL0C3U;Database=Proyecto1;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comercio>(entity =>
            {
                entity.HasKey(e => e.IdComercio)
                    .HasName("PK__Comercio__0E0A81A8462AC763");

                entity.Property(e => e.IdComercio).HasColumnName("Id_comercio");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.NumeroTelefono).HasColumnName("Numero_Telefono");
            });

            modelBuilder.Entity<ComercioUsuario>(entity =>
            {
                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnName("fechaRegistro")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdComercio).HasColumnName("idComercio");

                entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

                entity.HasOne(d => d.IdComercioNavigation)
                    .WithMany(p => p.ComercioUsuario)
                    .HasForeignKey(d => d.IdComercio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ComercioU__idCom__2D27B809");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.ComercioUsuario)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ComercioU__idUsu__2C3393D0");
            });

            modelBuilder.Entity<DetalleVenta>(entity =>
            {
                entity.ToTable("detalleVenta");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Producto).HasColumnName("producto");

                entity.Property(e => e.Subtotal).HasColumnName("subtotal");

                entity.Property(e => e.Venta).HasColumnName("venta");

                entity.HasOne(d => d.ProductoNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.Producto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__detalleVe__produ__3B75D760");

                entity.HasOne(d => d.VentaNavigation)
                    .WithMany(p => p.DetalleVenta)
                    .HasForeignKey(d => d.Venta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__detalleVe__venta__3C69FB99");
            });

            modelBuilder.Entity<Inventario>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Producto).HasColumnName("producto");

                entity.HasOne(d => d.ComercioNavigation)
                    .WithMany(p => p.Inventario)
                    .HasForeignKey(d => d.Comercio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventari__Comer__34C8D9D1");

                entity.HasOne(d => d.ProductoNavigation)
                    .WithMany(p => p.Inventario)
                    .HasForeignKey(d => d.Producto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventari__produ__33D4B598");
            });

            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.HasKey(e => e.IdPerfil)
                    .HasName("PK__Perfil__2CDD94196BF57848");

                entity.Property(e => e.IdPerfil)
                    .HasColumnName("Id_Perfil")
                    .ValueGeneratedNever();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Productos>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__Producto__1D8EFF01A9A85661");

                entity.Property(e => e.IdProducto).HasColumnName("Id_producto");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Imagen).HasColumnType("image");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuarios__63C76BE2FD7B4704");

                entity.HasIndex(e => e.UserName)
                    .HasName("UQ__Usuarios__66DCF95C7478B934")
                    .IsUnique();

                entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasColumnName("apellidos")
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasColumnName("clave")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdPerfil).HasColumnName("Id_Perfil");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("userName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPerfilNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdPerfil)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Usuarios__Id_Per__276EDEB3");
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PK__Venta__40F9A2073019C726");

                entity.Property(e => e.Codigo).HasColumnName("codigo");

                entity.Property(e => e.Comercio).HasColumnName("comercio");

                entity.Property(e => e.Fecha)
                    .HasColumnName("fecha")
                    .HasColumnType("datetime");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.HasOne(d => d.ComercioNavigation)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.Comercio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Venta__comercio__38996AB5");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
