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
        private readonly EssoDbContext dbContext;

        public CountryDal(EssoDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;

        }
        public List<Country> Pagination(int pageSize, int pageNumber)
        {
            return dbContext.Countries.OrderBy(x => x.Name).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
        }
    }
}
