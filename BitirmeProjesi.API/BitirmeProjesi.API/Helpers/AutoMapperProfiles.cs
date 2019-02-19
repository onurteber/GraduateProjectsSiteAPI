using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BitirmeProjesi.API.Dtos;
using BitirmeProjesi.API.Models;

namespace BitirmeProjesi.API.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<University, UniversityForListDto>();
            
        }
    }
}
