using API.Dtos;
using AutoMapper;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Profiles
{
    public class AutoMapping:Profile
    {
        public AutoMapping()
        {
            //Source to Destination Mapping
            CreateMap<Car, CarReadDto>();
            CreateMap<CarCreateDto, Car>();
            CreateMap<CarUpdateDto, Car>();
        }
    }
}
