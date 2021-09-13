using Esso.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esso.DataAccess.Config
{
    public class CountryConfiguration
    {
        public class EventConfiguration : IEntityTypeConfiguration<Country>
        {
            public void Configure(EntityTypeBuilder<Country> builder)
            {
                builder.Property(x => x.Name).IsRequired().HasMaxLength(25);
            }
        }
    }
}
