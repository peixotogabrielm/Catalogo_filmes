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
                .IsRequired().HasMaxLength(200);

            builder.Property(f => f.Genero)
                .IsRequired().HasMaxLength(100);

            builder.Property(f => f.Duracao)
                .IsRequired().HasColumnType("time");
            
            builder.Property(f => f.Sinopse)
                .IsRequired().HasMaxLength(2000);

            builder.Property(f => f.Ano)
                .IsRequired().HasColumnType("int");

            builder.HasIndex(f => f.Titulo)
                .IsUnique();


        }
    }
}