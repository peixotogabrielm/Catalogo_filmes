using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoFilmes.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CatalogoFilmes.Data.Maps
{
    public class RoleMap : BaseMap<Roles>
    {
        public RoleMap() : base("Roles") { }

        public override void Configure(EntityTypeBuilder<Roles> builder)
        {
            base.Configure(builder);

            builder.Property(r => r.Role)
                .IsRequired().HasMaxLength(100);

            builder.HasMany(r => r.Usuarios)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}