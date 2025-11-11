using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoFilmes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogoFilmes.Data.Maps
{
    public class CatalogoMap : BaseMap<Catalogo>
    {
        public CatalogoMap() : base("Catalogos") { }
         public override void Configure(EntityTypeBuilder<Catalogo> builder)
        {
            base.Configure(builder);

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome).IsRequired().HasMaxLength(200);

            builder.Property(c => c.Descricao).HasMaxLength(1000);

            builder.Property(c => c.Categoria).IsRequired();

            builder.Property(c => c.Visualizacoes).HasDefaultValue(0);
            
            builder.Property(c => c.Likes).HasDefaultValue(0);

            builder.Property(c => c.Dislikes).HasDefaultValue(0);

            builder.Property(c => c.NumeroFavoritos).HasDefaultValue(0);


            builder.HasOne(c => c.Usuario)
                .WithMany(u => u.Catalogos)
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Filmes)
                .WithMany(f => f.Catalogos)
                .UsingEntity(j => j.ToTable("CatalogoFilmes"));
        }

    }
}