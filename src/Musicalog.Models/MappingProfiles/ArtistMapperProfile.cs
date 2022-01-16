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
    public class ArtistMapperProfile : Profile
    {
        public ArtistMapperProfile()
        {
            CreateMap<Artist, ArtistDto>()
                .ForMember(dest => dest.Albums, opt => opt.MapFrom(src => src.Albums.Select(x => new AlbumDto() { Title = x.Title, Id = x.Id, AlbumType = x.AlbumType })));

            CreateMap<ArtistDto, Artist>();
        }
    }
}
