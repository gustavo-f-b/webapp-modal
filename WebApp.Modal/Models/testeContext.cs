using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApp.Modal.ViewModels;

namespace WebApp.Modal.Models
{
    public partial class testeContext : DbContext
    {
        public testeContext()
        {
        }

        public testeContext(DbContextOptions<testeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Servico> Servicos { get; set; } = null!;
        public virtual DbSet<ServicosCliente> ServicosClientes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=PC;Database=teste;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Servico>(entity =>
            {
                entity.Property(e => e.Descricao)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ServicosCliente>(entity =>
            {
                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.ServicosClientes)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServicosClientes_Clientes");

                entity.HasOne(d => d.Servico)
                    .WithMany(p => p.ServicosClientes)
                    .HasForeignKey(d => d.ServicoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ServicosClientes_Servicos");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<WebApp.Modal.ViewModels.ClienteViewModel>? ClienteViewModel { get; set; }

        public DbSet<WebApp.Modal.ViewModels.ServicosClientesViewModel>? ServicosClientesViewModel { get; set; }

        public DbSet<WebApp.Modal.ViewModels.ServicoViewModel>? ServicoViewModel { get; set; }
    }
}
