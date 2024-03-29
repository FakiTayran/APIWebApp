﻿using Esso.DataAccess.Abstract;
using Esso.DataAccess.EfCore;
using Esso.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esso.DataAccess.Concrete
{
    public class CityDal : EfRepository<City>, ICityDal
    {
        private readonly EssoDbContext dbContext;

        public CityDal(EssoDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<City> GetByCountryID(int CountryID)
        {
            return dbContext.Cities.Where(x => x.CountryID == CountryID).ToList();
        }

        public List<City> Pagination(int countryID,int pageSize, int pageNumber)
        {
            return dbContext.Cities.Where(x=>x.CountryID==countryID).OrderBy(x => x.Name).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
        }
    }
}
