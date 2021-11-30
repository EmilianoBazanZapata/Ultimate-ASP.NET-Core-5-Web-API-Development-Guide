using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Models;
using AutoMapper;

namespace Api.Configurations
{
    public class Mapperinitializer : Profile
    {
        public Mapperinitializer()
        {
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Country, CreateCountryDTO>().ReverseMap();

            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Country, CreateHotelDTO>().ReverseMap();
        }
    }
}