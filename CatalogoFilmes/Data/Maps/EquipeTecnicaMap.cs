using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoFilmes.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogoFilmes.Data.Maps
{
    public class EquipeMap : BaseMap<EquipeTecnica>
    {
        public EquipeMap() : base("Equipes") { }
        public override void Configure(EntityTypeBuilder<EquipeTecnica> builder)
        {
            base.Configure(builder);

            builder.HasKey(e => e.Id);



            builder.Property(e => e.Nome)
                .IsRequired().HasMaxLength(2000);

            builder.Property(e => e.Cargo)
                .IsRequired().HasMaxLength(200);

            builder.HasOne(e => e.Filme)
                .WithMany(f => f.Equipe)
                .HasForeignKey(e => e.FilmeId);

        }
    }
}