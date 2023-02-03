using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Carpark.Models;
using AutoMapper;
using Carpark.Data.Entities;

namespace Carpark.Business.Mapper
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Car, CarDTO>().ReverseMap();
            CreateMap<Car, CarCreateDTO>().ReverseMap();
            CreateMap<Car, CarUpdateDTO>().ReverseMap();
        }
    }
}
