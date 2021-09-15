using Esso.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esso.DataAccess.Abstract
{
    public interface ICountryDal : IAsyncRepository<Country>
    {
        List<Country> Pagination(int pageSize, int pageNumber);
    }
}
