using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoFilmes.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogoFilmes.Data.Maps
{
    public class ClassificaoMap : BaseMap<Classificacoes>
    {
         public ClassificaoMap() : base("Classificacoes") { }
        public override void Configure(EntityTypeBuilder<Classificacoes> builder)
        {
            base.Configure(builder);

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Fonte).HasMaxLength(100);

            builder.Property(c => c.Nota).HasMaxLength(50);

            builder.HasOne(c => c.Filme)
                .WithMany(f => f.Classificacoes)
                .HasForeignKey(c => c.FilmeId);
        }
    }
}