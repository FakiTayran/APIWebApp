using Esso.Business.Abstract;
using Esso.Entity.Dtos;
using Esso.Entity.Models;
using Esso.Entity.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Esso.API.Controller
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize(Policy = "City")]
    public class CityController : ControllerBase
    {
        private readonly ICityService cityService;

        public CityController(ICityService _cityService)
        {
            cityService = _cityService;
        }
        [HttpGet("GetCities/{CountryID}/{pageSize}/{pageNumber}")]
        public async Task<IActionResult> GetCities(int CountryID, int pageSize, int pageNumber)
        {
            var cities = cityService.GetByCountryID(CountryID);
            var pagination = new Pagination<City>
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalRecords = cities.ToList().Count,
                Content = cityService.Pagination(CountryID, pageSize, pageNumber),
            };

            return Ok(new GeneralResponse<Pagination<City>>
            {
                Result = pagination,
                Code = 1,
                IsError = false
            });

        }

        [HttpPost("CreateCity")]
        public async Task<IActionResult> CreateCity(CityDto cityDto)
        {
            try
            {
                if (!string.IsNullOrEmpty(cityDto.Name))
                {
                    City city = new City
                    {
                        Name = cityDto.Name,
                        CountryID = cityDto.CountryID
                    };
                    var result = await cityService.AddAsync(city);
                    if (result != null)
                    {
                        return Ok(new GeneralResponse<City>
                        {
                            Result = result,
                            Code = 1,
                            IsError = false
                        });
                    }
                    else
                    {
                        return Ok(new GeneralResponse<string>
                        {
                            Result = "Şehir Eklenemedi.",
                            Code = 1,
                            IsError = true
                        });
                    }
                }
                else
                {
                    return Ok(new GeneralResponse<string>
                    {
                        Result = "Şehir İsmi Boş Geçilemez",
                        Code = 1,
                        IsError = true
                    });
                }
            }
            catch (Exception ex)
            {
                //log(ex);
                return Ok(new GeneralResponse<string>
                {
                    Result = "Şehir Eklenirken Bir Hata Oluştu",
                    Code = 1,
                    IsError = true
                });
            }

        }

        [HttpDelete("DeleteCity/{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            try
            {
                if (id > 0)
                {
                    var deletableCity = await cityService.GetByIdAsync(id);
                    await cityService.DeleteAsync(deletableCity);
                    return Ok(new GeneralResponse<string>
                    {
                        Result = "Şehir Başarıyla Silindi",
                        Code = 1,
                        IsError = false
                    });
                }
                else
                {
                    return Ok(new GeneralResponse<string>
                    {
                        Result = "Seçili Şehir Yok",
                        Code = 1,
                        IsError = true
                    });
                }
            }
            catch (Exception ex)
            {
                //log(ex)
                return Ok(new GeneralResponse<string>
                {
                    Result = "Şehir Silinirken Bir Hata Oluştu",
                    Code = 1,
                    IsError = true
                });
            }
        }

        [HttpGet("UpdateCity/{id}/{name}/{countryID}")]
        public async Task<IActionResult> UpdateCity(int id, string name, int countryID)
        {
            try
            {
                var city = await cityService.GetByIdAsync(id);
                city.Name = name;
                city.CountryID = countryID;
                await cityService.UpdateAsync(city);
                return Ok(new GeneralResponse<string>
                {
                    Result = "Şehir Başarıyla Güncellendi",
                    Code = 1,
                    IsError = false
                });
            }
            catch (Exception ex)
            {
                return Ok(new GeneralResponse<string>
                {
                    Result = "Şehir Güncellenirken Bir Hata Oluştu",
                    Code = 1,
                    IsError = true
                });
            }

        }


    }
}
