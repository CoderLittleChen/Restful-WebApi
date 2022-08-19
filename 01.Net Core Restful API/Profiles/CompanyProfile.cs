using _01.Net_Core_Restful_API.Entities;
using _01.Net_Core_Restful_API.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _01.Net_Core_Restful_API.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDto>()
                        .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Name));
            CreateMap<CompanyAddDto, Company>();
            CreateMap<Company, CompanyFullDto>();
        }
    }
}
