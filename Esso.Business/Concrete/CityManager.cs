using Esso.Business.Abstract;
using Esso.DataAccess.Abstract;
using Esso.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esso.Business.Concrete
{
    public class CityManager : ICityService
    {
        private readonly ICityDal cityDal;

        public CityManager(ICityDal _cityDal)
        {
            cityDal = _cityDal;
        }
        public async Task<City> AddAsync(City entity)
        {
            return await cityDal.AddAsync(entity);
        }

        public async Task DeleteAsync(City entity)
        {
             await cityDal.DeleteAsync(entity);
        }

        public async Task<City> GetByIdAsync(int id)
        {
            return await cityDal.GetByIdAsync(id);
        }

        public async Task<List<City>> ListAllAsync()
        {
            return await cityDal.ListAllAsync();
        }

        public async Task UpdateAsync(City entity)
        {
             await cityDal.UpdateAsync(entity);
        }
    }
}
