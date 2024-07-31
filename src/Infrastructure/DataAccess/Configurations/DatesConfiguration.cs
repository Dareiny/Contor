using Domain;
using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configurations
{
    public class DatesConfiguration : IEntityTypeConfiguration<Domain.Dates>
    {
        public void Configure(EntityTypeBuilder<Domain.Dates> builder)
        {
            builder.ToTable("Dates").HasKey(t => t.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
            builder.Property(x => x.BirthDate).IsRequired();
            builder.Property(x => x.FotoName);

        }
    }
}
