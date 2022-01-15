using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Musicalog.Domain;
using Musicalog.Domain.Services;
using Musicalog.Models.Dtos;
using Musicalog.Models.Entities;

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

        [MapToApiVersion("1.0")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ArtistDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var artist = await this._artistService.GetIdAsync(id);

            if (artist is null)
                return NotFound();

            return Ok(this._mapper.Map<ArtistDto>(artist));
        }


        [MapToApiVersion("1.0")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<ActionResult<int>> Create(ArtistDto artistDto)
        {
            var artist = this._mapper.Map<Artist>(artistDto);
            await this._artistService.AddAsync(artist);

            await this._artistService.CommitAsync();

            return Ok(artist.Id);
        }

        [MapToApiVersion("1.0")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(int id, ArtistDto artistDto)
        {
            var artist = await this._artistService.GetIdAsync(id);

            if (artist is null)
                return NotFound();

            artist.Name = artistDto.Name;

            this._artistService.Update(artist);
            await this._artistService.CommitAsync();

            return Ok();
        }

        [MapToApiVersion("1.0")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var artist = await this._artistService.GetIdAsync(id);

            if (artist is null)
                return NotFound();

            this._artistService.Remove(artist);
            await this._artistService.CommitAsync();

            return Ok();
        }
    }
}
