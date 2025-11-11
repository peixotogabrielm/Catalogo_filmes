using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoFilmes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogoFilmes.Data.Maps
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public UsuarioMap() { }
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

         
            builder.Property(u => u.Nome)
                .IsRequired().HasMaxLength(100);

            builder.Property(u => u.Email)
                .IsRequired().HasMaxLength(100);

            builder.Property(u => u.Celular)
                .IsRequired().HasMaxLength(20);

            builder.Property(u => u.CPF)
                .IsRequired().HasMaxLength(11);

            builder.HasIndex(u => u.CPF).IsUnique();

            builder.Property(u => u.DataCriacao)
                .HasDefaultValueSql("GETUTCDATE()");


            builder.HasIndex(u => u.Email)
            .IsUnique();

            builder.HasMany(u => u.Catalogos)
                .WithOne(c => c.Usuario)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
} 