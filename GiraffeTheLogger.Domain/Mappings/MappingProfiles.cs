using System;
using AutoMapper;
using GiraffeTheLogger.Domain.Dtos;
using GiraffeTheLogger.Domain.Entities;
using GiraffeTheLogger.Domain.Extensions;

namespace GiraffeTheLogger.Domain.Mappings {
    public class MappingProfiles : Profile {
        public MappingProfiles () {
            CreateMap<MessageDto, LogEntity> ()
                .ForMember (dest => dest.CreationDateTime,
                    opt => opt.MapFrom (src => src.CreationDateTime.ToUnixTimestampInSeconds ()))
                .ForMember (dest => dest.ReceiveDateTime,
                    opt => opt.MapFrom (src => DateTime.Now.ToUnixTimestampInSeconds ()));

        }
    }
}