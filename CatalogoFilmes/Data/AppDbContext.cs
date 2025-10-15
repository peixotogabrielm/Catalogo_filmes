using CatalogoFilmes.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoFilmes.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Filme> Filmes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Filme>(entity =>
            {
                entity.Property(f => f.Id)
                    .HasDefaultValueSql("NEWID()");

                entity.Property(f => f.Titulo)
                .IsRequired();

                entity.Property(f => f.Genero)
                .IsRequired();

                entity.Property(f => f.Ano)
                .IsRequired();

                entity.HasIndex(f => f.Titulo)
                .IsUnique();
            });
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(u => u.Role)
                    .HasDefaultValue("User");

                entity.Property(u => u.DataCriacao)
                    .HasDefaultValueSql("GETUTCDATE()"); 
                entity.Property(u => u.Id)
                    .HasDefaultValueSql("NEWID()");

                entity.Property(u => u.Nome)
                    .IsRequired();

                entity.Property(u => u.Email)
                    .IsRequired();

                entity.Property(u => u.SenhaHash)
                .IsRequired();

                entity.HasIndex(u => u.Email)
                .IsUnique();
            });
        }
    }
}
