using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Musicalog.Domain.Exceptions;
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
        private readonly ILogger<ArtistsController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtistsController"/> class.
        /// </summary>
        /// <param name="artistService">The artist service.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="logger">The logger.</param>
        public ArtistsController(IArtistServices artistService, IMapper mapper, ILogger<ArtistsController> logger) => (_artistService, _mapper, _logger) = (artistService, mapper, logger);

        /// <summary>
        /// Lists this instance.
        /// </summary>
        /// <returns>ActionResult&lt;IList&lt;ArtistDto&gt;&gt;.</returns>
        [MapToApiVersion("1.0")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<ArtistDto>))]
        public async Task<ActionResult<IList<ArtistDto>>> List() => Ok(this._mapper.Map<IList<ArtistDto>>(await this._artistService.ListAll()));

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IActionResult.</returns>
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

        /// <summary>
        /// Creates the specified artist dto.
        /// </summary>
        /// <param name="artistDto">The artist dto.</param>
        /// <returns>ActionResult&lt;System.Int32&gt;.</returns>
        [MapToApiVersion("1.0")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<ActionResult<int>> Create(ArtistDto artistDto)
        {
            var artist = this._mapper.Map<Artist>(artistDto);
            await this._artistService.AddAsync(artist);

            return Ok(artist.Id);
        }

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="artistDto">The artist dto.</param>
        /// <returns>ActionResult.</returns>
        [MapToApiVersion("1.0")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(int id, ArtistDto artistDto)
        {
            try
            {
                await this._artistService.Update(id, this._mapper.Map<Artist>(artistDto));

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

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IActionResult.</returns>
        [MapToApiVersion("1.0")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this._artistService.Delete(id);

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
