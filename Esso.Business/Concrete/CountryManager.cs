using Esso.Business.Abstract;
using Esso.DataAccess.Abstract;
using Esso.Entity;
using Esso.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esso.Business.Concrete
{
    public class CountryManager : ICountryService
    {
        private readonly ICountryDal countryDal;

        public CountryManager(ICountryDal _countryDal)
        {
            countryDal = _countryDal;
        }
        public async Task<Country> AddAsync(Country entity)
        {
            return await countryDal.AddAsync(entity);
        }

        public async Task DeleteAsync(Country entity)
        {
            await countryDal.DeleteAsync(entity);
        }

        public async Task<Country> GetByIdAsync(int id)
        {
            return await countryDal.GetByIdAsync(id);
        }

        public async Task<List<Country>> ListAllAsync()
        {
            return await countryDal.ListAllAsync();
        }

        public async Task UpdateAsync(Country entity)
        {
            await countryDal.UpdateAsync(entity);
        }
    }
}
