using Esso.Business.Abstract;
using Esso.DataAccess.Concrete;
using Esso.Entity;
using Esso.Entity.Models.Dtos;
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
    public class CountryController : ControllerBase
    {
        private readonly ICountryService countryService;

        public CountryController(ICountryService _countryService)
        {
            countryService = _countryService;
        }
        [HttpGet("GetCountries")]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var countries = await countryService.ListAllAsync();
                if (countries.Count > 0)
                {
                    return Ok(new GeneralResponse<List<Country>>
                    {
                        Result = countries,
                        Code = 1,
                        IsError = false
                    });
                }
                else
                {
                    return Ok(new GeneralResponse<string>
                    {
                        Result = "Gösterilecek Ülke Yok",
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
                    Result = "Ülkeler Listelenemedi.",
                    Code = 1,
                    IsError = true
                });
            }

        }

        [HttpPost("CreateCountry")]
        public async Task<IActionResult> CreateCountry(CountryDto countryDto)
        {
            try
            {
                if (!string.IsNullOrEmpty(countryDto.Name))
                {
                    Country country = new Country
                    {
                        Name = countryDto.Name
                    };
                    var result = await countryService.AddAsync(country);
                    if (result != null)
                    {
                        return Ok(new GeneralResponse<Country>
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
                            Result = "Ülke Eklenemedi.",
                            Code = 1,
                            IsError = true
                        });
                    }
                }
                else
                {
                    return Ok(new GeneralResponse<string>
                    {
                        Result = "Ülke İsmi Boş Geçilemez",
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
                    Result = "Ülke Eklenirken Bir Hata Oluştu",
                    Code = 1,
                    IsError = true
                });
            }

        }

        [HttpDelete("DeleteCountry/{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            try
            {
                if (id > 0)
                {
                    var deletableCountry = await countryService.GetByIdAsync(id);
                    await countryService.DeleteAsync(deletableCountry);
                    return Ok(new GeneralResponse<string>
                    {
                        Result = "Ülke Başarıyla Silindi",
                        Code = 1,
                        IsError = false
                    });
                }
                else
                {
                    return Ok(new GeneralResponse<string>
                    {
                        Result = "Seçili Ülke Yok",
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
                    Result = "Ülke Silinirken Bir Hata Oluştu",
                    Code = 1,
                    IsError = true
                });
            }
        }

        [HttpPut("UpdateCountry/{id}")]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] CountryDto countryDto)
        {
            try
            {
                var country = await countryService.GetByIdAsync(id);
                country.Name = countryDto.Name;
                await countryService.UpdateAsync(country);
                return Ok(new GeneralResponse<string>
                {
                    Result = "Ülke Başarıyla Güncellendi",
                    Code = 1,
                    IsError = false
                });
            }
            catch (Exception ex)
            {
                return Ok(new GeneralResponse<string>
                {
                    Result = "Ülke Güncellenirken Bir Hata Oluştu",
                    Code = 1,
                    IsError = true
                });
            }

        }

    }
}
