using Esso.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esso.DataAccess.Abstract
{
    public interface ICityDal : IAsyncRepository<City>
    {
        List<City> GetByCountryID(int CountryID);
    }
}
