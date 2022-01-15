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

        [MapToApiVersion("1.0")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlbumDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var album = await this._albumService.GetIdAsync(id);

            if (album is null)
                return NotFound();

            return Ok(this._mapper.Map<AlbumDto>(album));
        }


        [MapToApiVersion("1.0")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<ActionResult<int>> Create(AlbumDto AlbumDto)
        {
            var album = this._mapper.Map<Album>(AlbumDto);
            await this._albumService.AddAsync(album);

            await this._albumService.CommitAsync();

            return Ok(album.Id);
        }

        [MapToApiVersion("1.0")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(int id, AlbumDto AlbumDto)
        {
            var album = await this._albumService.GetIdAsync(id);

            if (album is null)
                return NotFound();

            album.Title = AlbumDto.Title;
            album.AlbumType = AlbumDto.AlbumType;
            
            this._albumService.Update(album);
            await this._albumService.CommitAsync();

            return Ok();
        }

        [MapToApiVersion("1.0")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var album = await this._albumService.GetIdAsync(id);

            if (album is null)
                return NotFound();

            this._albumService.Remove(album);
            await this._albumService.CommitAsync();

            return Ok();
        }
    }
}
