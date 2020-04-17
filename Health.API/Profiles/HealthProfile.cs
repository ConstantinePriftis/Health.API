using AutoMapper;
using Health.API.Dtos;
using Health.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health.API.Profiles
{
    public class HealthProfile : Profile
    {
        public HealthProfile()
        {
            CreateMap<UserCreateDto, User>().ReverseMap();
            CreateMap<TemperatureCreateDto, Temperature>().ReverseMap();
            CreateMap<FeverCreateDto, Fever>().ReverseMap();

            CreateMap<Temperature, TemperatureDto>().ReverseMap();
        }
    }
}
