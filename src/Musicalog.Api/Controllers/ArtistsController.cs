using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Musicalog.Domain;
using Musicalog.Models.Dtos;

namespace Musicalog.Api.Controllers
{
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistServices _artistService;
        private readonly IMapper _mapper;

        public ArtistsController(IArtistServices artistService, IMapper mapper) => (_artistService, _mapper) = (artistService, mapper);

        [MapToApiVersion("1.0")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<ArtistDto>))]
        public async Task<ActionResult<IList<ArtistDto>>> List() => Ok(this._mapper.Map<IList<ArtistDto>>(await this._artistService.ListAll()));
    }
}
