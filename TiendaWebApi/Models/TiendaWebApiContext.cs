using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TiendaWebApi.Models
{
    public partial class TiendaWebApiContext : DbContext
    {
        public TiendaWebApiContext()
        {
        }

        public TiendaWebApiContext(DbContextOptions<TiendaWebApiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; } = null!;
        public virtual DbSet<Marca> Marcas { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;
        public virtual DbSet<UsuariosRoles> UsuariosRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("categoria");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.ToTable("marca");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("producto");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CategoriaId).HasColumnName("categoriaId");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaCreacion");

                entity.Property(e => e.MarcaId).HasColumnName("marcaId");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Precio)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("precio");

                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.CategoriaId)
                    .HasConstraintName("FK_producto_categoria");

                entity.HasOne(d => d.Marca)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.MarcaId)
                    .HasConstraintName("FK_producto_marca");
            });


            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("rol");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ApellidoMaterno)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("apellidoMaterno");

                entity.Property(e => e.ApellidoPaterno)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("apellidoPaterno");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Nombres)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("nombres");

                entity.Property(e => e.Password)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.UserName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("userName");

                entity.HasMany(p => p.Roles)
                    .WithMany(p => p.Usuarios)
                    .UsingEntity<UsuariosRoles>(
                        j => j
                            .HasOne(pt => pt.Rol)
                            .WithMany(t => t.UsuariosRoles)
                            .HasForeignKey(pt => pt.RolId),
                        j => j
                            .HasOne(pt => pt.Usuario)
                            .WithMany(p => p.UsuariosRoles)
                            .HasForeignKey(pt => pt.UsuarioId),
                        j =>
                        {
                            j.HasKey(t => new { t.UsuarioId, t.RolId });
                        });
                        });

            modelBuilder.Entity<UsuariosRoles>(entity =>
            {
                entity.ToTable("usuariosRoles");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RolId).HasColumnName("rolId");

                entity.Property(e => e.UsuarioId).HasColumnName("usuarioId");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.UsuariosRoles)
                    .HasForeignKey(d => d.RolId)
                    .HasConstraintName("FK_usuarioRol_rol");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.UsuariosRoles)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK_usuarioRol_usuario");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
