using AutoMapper;
using Musicalog.Models.Dtos;
using Musicalog.Models.Entities;

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
