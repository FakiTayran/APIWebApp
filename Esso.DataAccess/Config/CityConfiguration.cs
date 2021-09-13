using Esso.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esso.DataAccess.Config
{
    public class CityConfiguration
    {
        public class EventConfiguration : IEntityTypeConfiguration<City>
        {
            public void Configure(EntityTypeBuilder<City> builder)
            {
                builder.Property(x => x.Name).IsRequired().HasMaxLength(25);
                builder.Property(x => x.CountryID).IsRequired();
            }

        }
    }
}
