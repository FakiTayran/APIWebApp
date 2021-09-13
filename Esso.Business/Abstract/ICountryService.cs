using Esso.DataAccess.Abstract;
using Esso.Entity;
using Esso.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esso.Business.Abstract
{
    public interface ICountryService : IAsyncRepository<Country>
    {
    }
}
