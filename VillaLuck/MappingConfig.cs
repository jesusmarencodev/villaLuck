﻿using AutoMapper;
using VillaLuck.Modelos;
using VillaLuck.Modelos.Dto;

namespace VillaLuck
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDto>();
            CreateMap<VillaDto, Villa>();

            CreateMap<Villa, VillaCreateDto>().ReverseMap();
            CreateMap<Villa, VillaUpdateDto>().ReverseMap();


        }
    }
}
