using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class JrodriguezExamenPracticoMarzamContext : DbContext
{
    public JrodriguezExamenPracticoMarzamContext()
    {
    }

    public JrodriguezExamenPracticoMarzamContext(DbContextOptions<JrodriguezExamenPracticoMarzamContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Medicamento> Medicamentos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<VentaMedicamento> VentaMedicamentos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database= JRodriguezExamenPracticoMarzam; User ID=sa; TrustServerCertificate=True; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Medicamento>(entity =>
        {
            entity.HasKey(e => e.IdMedicamento).HasName("PK__Medicame__AC96376E0EF29073");

            entity.ToTable("Medicamento");

            entity.Property(e => e.Imagen).IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__Persona__2EC8D2AC6342190C");

            entity.ToTable("Persona");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VentaMedicamento>(entity =>
        {
            entity.HasKey(e => e.IdVentaMedicamento).HasName("PK__VentaMed__8026E52AAE0F6C71");

            entity.ToTable("VentaMedicamento");

            entity.HasOne(d => d.IdMedicamentoNavigation).WithMany(p => p.VentaMedicamentos)
                .HasForeignKey(d => d.IdMedicamento)
                .HasConstraintName("FK__VentaMedi__IdMed__145C0A3F");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.VentaMedicamentos)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("FK__VentaMedi__IdPer__15502E78");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
