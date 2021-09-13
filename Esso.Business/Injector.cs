using Esso.Business.Abstract;
using Esso.Business.Concrete;
using Esso.DataAccess.Abstract;
using Esso.DataAccess.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esso.Business
{
    public static class Injector
    {
        public static void RegisterEntity(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            services.AddScoped<ICityDal, CityDal>();
            services.AddScoped<ICountryDal, CountryDal>();

            services.AddScoped<ICountryService, CountryManager>();
            services.AddScoped<ICityService, CityManager>();
        }
    }
}
