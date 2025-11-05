using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoFilmes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogoFilmes.Data.Maps
{
    public class UsuarioMap : BaseMap<Usuario>
    {
        public UsuarioMap() : base("Usuarios") { }
        public override void Configure(EntityTypeBuilder<Usuario> builder)
        {
            base.Configure(builder);

            builder.Property(u => u.Nome)
                .IsRequired().HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired().HasMaxLength(100);

            builder.Property(u => u.SenhaHash)
            .IsRequired();

            builder.Property(u => u.Celular)
                .IsRequired().HasMaxLength(20);

            builder.Property(u => u.CPF)
                .IsRequired().HasMaxLength(11);

            builder.HasIndex(u => u.CPF).IsUnique();

            builder.HasIndex(u => u.Email)
            .IsUnique();

            builder.HasOne(u => u.Role)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
} 