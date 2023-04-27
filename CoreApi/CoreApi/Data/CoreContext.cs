using CoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreApi.Data;

public class CoreContext : DbContext
{
    public CoreContext(DbContextOptions<CoreContext> opts)
        : base(opts)
    {
        
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Tecnologia> Tecnologias { get; set; }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Candidato> Candidatos { get; set; }
    public DbSet<Vaga> Vagas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Empresa>()
            .HasMany(p => p.Tecnologias)
            .WithMany(c => c.Empresas)
            .UsingEntity(j => j.ToTable("EmpresaTecnologia"));
    }
}
