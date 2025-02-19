using Microsoft.AspNetCore.Mvc;

using Api.Requests;
using Application.Interfaces;
using Application.Dtos;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatsController(ILogger<CatsController> logger, ICatServices catServices) : ControllerBase
    {
        private readonly ILogger<CatsController> _logger = logger;
        private readonly ICatServices _catsService = catServices;


        [HttpPost("fetch")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> FetchCats()
        {
            bool ok = await _catsService.FetchCatsAsync();
            if (!ok)
            {
                return BadRequest("Fetch error.");
            }

            return CreatedAtAction(nameof(GetCatsByTagWithPaging), null);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CatDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCatById(int id)
        {
            if (id <= 0)
                return NotFound("Id needs to be greater than 0.");

            var cat = await _catsService.GetCatByIdAsync(id);

            if (cat == null)
                return NotFound("Cat with that id does not exist.");

            return Ok(cat);
        }


        [HttpGet]
        [ProducesResponseType(typeof(CatDto[]), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCatsByTagWithPaging([FromQuery] SearchByTagRequest request)
        {
            var pagedCats = await _catsService.GetCatsByTagPagedAsync(request.Page, request.PageSize,request.TagName);

            if (pagedCats == null || !pagedCats.Items.Any())
                return NotFound("No results found");

            return Ok(pagedCats);
        }
    }
}
