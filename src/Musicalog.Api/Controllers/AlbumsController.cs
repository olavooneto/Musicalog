using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Musicalog.Domain;
using Musicalog.Models.Dtos;
using Musicalog.Models.Entities;

namespace Musicalog.Api.Controllers
{
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumServices _albumService;
        private readonly IMapper _mapper;

        public AlbumsController(IAlbumServices albumService, IMapper mapper) => (_albumService, _mapper) = (albumService, mapper);

        [MapToApiVersion("1.0")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<AlbumDto>))]
        public async Task<ActionResult<IList<AlbumDto>>> List() => Ok(this._mapper.Map<IList<AlbumDto>>(await this._albumService.ListAll()));
    }
}
