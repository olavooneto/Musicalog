using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Musicalog.Domain;
using Musicalog.Domain.Exceptions;
using Musicalog.Domain.Services;
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
        private readonly ILogger<AlbumsController> _logger;

        public AlbumsController(IAlbumServices albumService, IMapper mapper, ILogger<AlbumsController> logger) => (_albumService, _mapper, _logger) = (albumService, mapper, logger);

        [MapToApiVersion("1.0")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<AlbumDto>))]
        public async Task<ActionResult<IList<AlbumDto>>> List() => Ok(this._mapper.Map<IList<AlbumDto>>(await this._albumService.ListAll()));

        [MapToApiVersion("1.0")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlbumDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(this._mapper.Map<AlbumDto>(await this._albumService.GetIdAsync(id)));
            }
            catch (NotFoundException nfException)
            {
                return NotFound(nfException.Message);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);

                return BadRequest(ex.Message);
            }
        }


        [MapToApiVersion("1.0")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Create(AlbumForCreationUpdateDto AlbumDto)
        {
            try
            {
                var album = this._mapper.Map<Album>(AlbumDto);
                await this._albumService.AddAsync(album);

                return Ok(album.Id);
            }
            catch (NotFoundException nfException)
            {
                return NotFound(nfException.Message);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [MapToApiVersion("1.0")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(int id, AlbumForCreationUpdateDto AlbumDto)
        {
            try
            {
                await this._albumService.Update(id, this._mapper.Map<Album>(AlbumDto));
                await this._albumService.CommitAsync();

                return Ok();
            }
            catch (NotFoundException nfException)
            {
                return NotFound(nfException.Message);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [MapToApiVersion("1.0")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this._albumService.Delete(id);

                return Ok();
            }
            catch (NotFoundException nfException)
            {
                return NotFound(nfException.Message);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}

