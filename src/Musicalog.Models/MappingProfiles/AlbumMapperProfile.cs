using AutoMapper;
using Musicalog.Models.Dtos;
using Musicalog.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicalog.Models.MappingProfiles
{
    public class AlbumMapperProfile : Profile
    {
        public AlbumMapperProfile()
        {
            CreateMap<Album, AlbumDto>();

            CreateMap<AlbumDto, Album>();
        }
    }
}
