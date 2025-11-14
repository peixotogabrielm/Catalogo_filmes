using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoFilmes.Data.Maps;
using CatalogoFilmes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogoFilmes.Maps
{
    public class FilmeMap : BaseMap<Filme>
    {
        public FilmeMap() : base("Filmes") { }
        public override void Configure(EntityTypeBuilder<Filme> builder)
        {
            base.Configure(builder);
            
            builder.Property(f => f.Titulo)
                .IsRequired().HasMaxLength(255);

            builder.Property(f => f.Genero)
                .IsRequired().HasMaxLength(200);

            builder.Property(f => f.Duracao)
                .IsRequired().HasColumnType("int");

            builder.Property(f => f.Sinopse).HasMaxLength(2000);

            builder.Property(f => f.Ano)
                .IsRequired().HasMaxLength(20);
    
            builder.Property(f => f.Tipo)
            .IsRequired().HasMaxLength(20);

            builder.Property(f => f.ImdbId)
            .IsRequired().HasMaxLength(50);

            builder.Property(f => f.Idioma)
            .IsRequired().HasMaxLength(100);

            builder.Property(f => f.Poster).HasMaxLength(500);

            builder.Property(f => f.NumeroNotas).HasColumnType("int");

            builder.Property(f => f.Notas).HasColumnType("float");

            builder.Property(f => f.MediaNotas).HasColumnType("float");

            builder.Property(f => f.Trailer).HasColumnType("nvarchar(max)");

            builder.HasMany(f => f.Catalogos)
                .WithMany(c => c.Filmes)
                .UsingEntity(j => j.ToTable("FilmesCatalogos"));
            
            builder.HasMany(f => f.Classificacoes)
                .WithOne(c => c.Filme)
                .HasForeignKey(c => c.FilmeId);

            builder.HasMany(f => f.Equipes)
                .WithOne(e => e.Filme)
                .HasForeignKey(e => e.FilmeId);
        }
    }
}