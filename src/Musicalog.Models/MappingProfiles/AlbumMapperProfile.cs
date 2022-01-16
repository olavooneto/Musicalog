using AutoMapper;
using Musicalog.Models.Dtos;
using Musicalog.Models.Entities;

namespace Musicalog.Models.MappingProfiles
{
    public class AlbumMapperProfile : Profile
    {
        public AlbumMapperProfile()
        {
            CreateMap<Album, AlbumDto>()
                .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.Artists.Select(x => new ArtistDto() { Name = x.Name, Id = x.Id })));

            CreateMap<AlbumDto, Album>();


            CreateMap<AlbumForCreationUpdateDto, Album>()
                .ForMember(dest => dest.Artists, opt => opt.MapFrom(src => src.Artists.Select(x => new Artist() { Id = x })));
        }
    }
}
