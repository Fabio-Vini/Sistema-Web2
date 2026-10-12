/*
Nome da dupla:

Fábio Vinicius Santos de Freitas

Kauã Felipe da Silva Jenior
*/

using Microsoft.EntityFrameworkCore;
using TP02.Models;

namespace TP02.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Bl> Bls => Set<Bl>();
    public DbSet<Container> Containers => Set<Container>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Bl>()
            .HasIndex(bl => bl.Numero)
            .IsUnique();

        modelBuilder.Entity<Container>()
            .HasIndex(container => container.Numero)
            .IsUnique();

        modelBuilder.Entity<Container>()
            .HasOne(container => container.Bl)
            .WithMany(bl => bl.Containers)
            .HasForeignKey(container => container.BlId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Bl>().HasData(
            new Bl
            {
                Id = 1,
                Numero = "BL-IMPORTACAO-2026",
                Consignee = "Logística Cubatão LTDA",
                Navio = "Santos Express"
            },
            new Bl
            {
                Id = 2,
                Numero = "BL-EXPORTACAO-9988",
                Consignee = "Alimentos Brasil S/A",
                Navio = "Oceanic Clipper"
            });
    }
}
