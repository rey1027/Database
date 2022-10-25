using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Database.Models
{
    public partial class DetailTECContext : DbContext
    {
        public DetailTECContext()
        {
        }

        public DetailTECContext(DbContextOptions<DetailTECContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CitaProductoConsumido> CitaProductoConsumidos { get; set; } = null!;
        public virtual DbSet<Citum> Cita { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<ClienteDireccion> ClienteDireccions { get; set; } = null!;
        public virtual DbSet<ClienteTelefono> ClienteTelefonos { get; set; } = null!;
        public virtual DbSet<InsumoProducto> InsumoProductos { get; set; } = null!;
        public virtual DbSet<Lavado> Lavados { get; set; } = null!;
        public virtual DbSet<Proveedor> Proveedors { get; set; } = null!;
        public virtual DbSet<Sucursal> Sucursals { get; set; } = null!;
        public virtual DbSet<Trabajador> Trabajadors { get; set; } = null!;
        public virtual DbSet<TrabajadorSucursal> TrabajadorSucursals { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CitaProductoConsumido>(entity =>
            {
                entity.HasKey(e => new { e.Placa, e.Fecha, e.Sucursal, e.NombreIP, e.Marca })
                    .HasName("PK__CITA_PRO__CDCFE027A201533C");

                entity.ToTable("CITA_PRODUCTO_CONSUMIDO");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Sucursal)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NombreIP)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NombreI_P");

                entity.Property(e => e.Marca)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.InsumoProducto)
                    .WithMany(p => p.CitaProductoConsumidos)
                    .HasForeignKey(d => new { d.NombreIP, d.Marca })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("procon_cita_fk");

                entity.HasOne(d => d.Citum)
                    .WithMany(p => p.CitaProductoConsumidos)
                    .HasForeignKey(d => new { d.Placa, d.Fecha, d.Sucursal })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cita_inpro_fk");
            });

            modelBuilder.Entity<Citum>(entity =>
            {
                entity.HasKey(e => new { e.Placa, e.Fecha, e.Sucursal })
                    .HasName("cita_pk");

                entity.ToTable("CITA");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Sucursal)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Cedula)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Iva).HasColumnName("IVA");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Tipo)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.CedulaNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.Cedula)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cita_clienteC_fk");

                entity.HasOne(d => d.SucursalNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.Sucursal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cita_sucursal_fk");

                entity.HasOne(d => d.TipoNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.Tipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cita_Lavado_fk");

                entity.HasMany(d => d.Cedulas)
                    .WithMany(p => p.Cita)
                    .UsingEntity<Dictionary<string, object>>(
                        "CitaTrabajador",
                        l => l.HasOne<Trabajador>().WithMany().HasForeignKey("Cedula").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("traba_cita_fk"),
                        r => r.HasOne<Citum>().WithMany().HasForeignKey("PlacaAuto", "FechaCita", "Sucursal").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("cita_trabajador_fk"),
                        j =>
                        {
                            j.HasKey("PlacaAuto", "FechaCita", "Sucursal", "Cedula").HasName("PK__CITA_TRA__088F04F82048B6F3");

                            j.ToTable("CITA_TRABAJADOR");

                            j.IndexerProperty<int>("PlacaAuto").HasColumnName("Placa_Auto");

                            j.IndexerProperty<DateTime>("FechaCita").HasColumnType("datetime").HasColumnName("Fecha_Cita");

                            j.IndexerProperty<string>("Sucursal").HasMaxLength(30).IsUnicode(false);
                        });
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Cedula)
                    .HasName("PK__CLIENTE__B4ADFE39CABDFA6C");

                entity.ToTable("CLIENTE");

                entity.Property(e => e.Cedula)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FechaDeNacimientoC)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_de_NacimientoC");

                entity.Property(e => e.NombreCompleto)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_Completo");

                entity.Property(e => e.Password)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Usuario)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClienteDireccion>(entity =>
            {
                entity.HasKey(e => e.Cedula)
                    .HasName("PK__CLIENTE___B4ADFE39F749FBD4");

                entity.ToTable("CLIENTE_DIRECCION");

                entity.Property(e => e.Cedula)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.CedulaNavigation)
                    .WithOne(p => p.ClienteDireccion)
                    .HasForeignKey<ClienteDireccion>(d => d.Cedula)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cliente_direccion_fk");
            });

            modelBuilder.Entity<ClienteTelefono>(entity =>
            {
                entity.HasKey(e => e.Cedula)
                    .HasName("PK__CLIENTE___B4ADFE3901B83688");

                entity.ToTable("CLIENTE_TELEFONO");

                entity.Property(e => e.Cedula)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.CedulaNavigation)
                    .WithOne(p => p.ClienteTelefono)
                    .HasForeignKey<ClienteTelefono>(d => d.Cedula)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("cliente_telefono_fk");
            });

            modelBuilder.Entity<InsumoProducto>(entity =>
            {
                entity.HasKey(e => new { e.NombreIP, e.Marca })
                    .HasName("PK__INSUMO_P__A626C04DFB17CDDC");

                entity.ToTable("INSUMO_PRODUCTO");

                entity.Property(e => e.NombreIP)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NombreI_P");

                entity.Property(e => e.Marca)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasMany(d => d.Tipos)
                    .WithMany(p => p.InsumoProductos)
                    .UsingEntity<Dictionary<string, object>>(
                        "InsumoProductoLavado",
                        l => l.HasOne<Lavado>().WithMany().HasForeignKey("Tipo").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("inpro_Lavado_fk"),
                        r => r.HasOne<InsumoProducto>().WithMany().HasForeignKey("NombreIP", "Marca").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("Lavado_inpro_fk"),
                        j =>
                        {
                            j.HasKey("NombreIP", "Marca", "Tipo").HasName("PK__INSUMO_P__12A8B661D83AE586");

                            j.ToTable("INSUMO_PRODUCTO_LAVADO");

                            j.IndexerProperty<string>("NombreIP").HasMaxLength(50).IsUnicode(false).HasColumnName("NombreI_P");

                            j.IndexerProperty<string>("Marca").HasMaxLength(30).IsUnicode(false);

                            j.IndexerProperty<string>("Tipo").HasMaxLength(30).IsUnicode(false);
                        });
            });

            modelBuilder.Entity<Lavado>(entity =>
            {
                entity.HasKey(e => e.Tipo)
                    .HasName("PK__LAVADO__8E762CB5C946A7A3");

                entity.ToTable("LAVADO");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PuntosOrtorgados).HasColumnName("Puntos_ortorgados");

                entity.Property(e => e.PuntosRequeridos).HasColumnName("Puntos_requeridos");
            });

            modelBuilder.Entity<Proveedor>(entity =>
            {
                entity.HasKey(e => e.CedulaJuridica)
                    .HasName("PK__PROVEEDO__D2F7F5427A7AEE4F");

                entity.ToTable("PROVEEDOR");

                entity.Property(e => e.CedulaJuridica)
                    .ValueGeneratedNever()
                    .HasColumnName("Cedula_Juridica");

                entity.Property(e => e.Contacto)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.CorreoElectronico)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Correo_electronico");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasMany(d => d.InsumoProductos)
                    .WithMany(p => p.CedulaJuridicas)
                    .UsingEntity<Dictionary<string, object>>(
                        "ProveedorInsumoProducto",
                        l => l.HasOne<InsumoProducto>().WithMany().HasForeignKey("NombreIP", "Marca").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("inpro_pr_fk"),
                        r => r.HasOne<Proveedor>().WithMany().HasForeignKey("CedulaJuridica").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("pr_inpro_fk"),
                        j =>
                        {
                            j.HasKey("CedulaJuridica", "NombreIP", "Marca").HasName("PK__PROVEEDO__1895994651B1ABC0");

                            j.ToTable("PROVEEDOR_INSUMO_PRODUCTO");

                            j.IndexerProperty<int>("CedulaJuridica").HasColumnName("Cedula_Juridica");

                            j.IndexerProperty<string>("NombreIP").HasMaxLength(50).IsUnicode(false).HasColumnName("NombreI_P");

                            j.IndexerProperty<string>("Marca").HasMaxLength(30).IsUnicode(false);
                        });
            });

            modelBuilder.Entity<Sucursal>(entity =>
            {
                entity.HasKey(e => e.Nombre)
                    .HasName("PK__SUCURSAL__75E3EFCEB05ED654");

                entity.ToTable("SUCURSAL");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Canton)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Distrito)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FechaDeApertura)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_de_Apertura");

                entity.Property(e => e.Provincia)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Trabajador>(entity =>
            {
                entity.HasKey(e => e.Cedula)
                    .HasName("PK__TRABAJAD__B4ADFE39EB1DF367");

                entity.ToTable("TRABAJADOR");

                entity.Property(e => e.Cedula).ValueGeneratedNever();

                entity.Property(e => e.Apellido1)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Apellido2)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FechaDeIngreso)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_de_Ingreso");

                entity.Property(e => e.FechaDeNacimiento)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_de_Nacimiento");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordT)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Password_t");

                entity.Property(e => e.Rol)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TipoDePago)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Tipo_de_Pago");
            });

            modelBuilder.Entity<TrabajadorSucursal>(entity =>
            {
                entity.HasKey(e => new { e.Cedula, e.Nombre })
                    .HasName("PK__TRABAJAD__43F3C0C53FFA80C8");

                entity.ToTable("TRABAJADOR_SUCURSAL");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FechaDeInicio)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_de_Inicio");

                entity.HasOne(d => d.CedulaNavigation)
                    .WithMany(p => p.TrabajadorSucursals)
                    .HasForeignKey(d => d.Cedula)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("trabajador_sucursalC_fk");

                entity.HasOne(d => d.NombreNavigation)
                    .WithMany(p => p.TrabajadorSucursals)
                    .HasForeignKey(d => d.Nombre)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sucursal_trabajador_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
