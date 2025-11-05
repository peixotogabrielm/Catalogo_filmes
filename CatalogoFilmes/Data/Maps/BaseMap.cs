using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoFilmes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogoFilmes.Data.Maps
{
    public class BaseMap<T> : IEntityTypeConfiguration<T> where T : Base
    {
        private readonly string _tableName;
        public BaseMap(string tableName)
        {
            _tableName = tableName;
        }
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            if (!string.IsNullOrEmpty(_tableName)) builder.ToTable(_tableName);

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id).HasColumnName("Id")
                .HasDefaultValueSql("NEWID()");

            builder.Property(b => b.DataCriacao)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}