using Esso.DataAccess.Abstract;
using Esso.DataAccess.EfCore;
using Esso.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esso.DataAccess.Concrete
{
    public class CountryDal : EfRepository<Country>, ICountryDal
    {
        public CountryDal(EssoDbContext dbContext) : base(dbContext)
        {
        }
    }
}
