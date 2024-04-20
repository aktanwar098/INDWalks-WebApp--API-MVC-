using AutoMapper;
using INDWalks.API.CustomActionFilters;
using INDWalks.API.Models.Domain;
using INDWalks.API.Models.DTO;
using INDWalks.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace INDWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {

        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper , IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        // CREATE Walk
        // POST: /api/walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto) 
        {
            //Map AddWalkRequestDto DTO  to Walk Domain Model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            await walkRepository.CreateAsync(walkDomainModel);

            //Map Walk Domain Model Back to WalkDto  DTO to send it to user back
            return Ok(mapper.Map<WalkDto>(walkDomainModel));

        }

        // GET All Walks
        // GET: /api/walks
        // GET: /api/walks? filterOn = Name & filterQuery = Track & sortBy = Name & isAscending = true
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn , [FromQuery] string? filterQuery , [FromQuery] string? sortBy  , [FromQuery] bool? isAscending )
        {
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn , filterQuery , sortBy , isAscending ?? true);

            // Map Domain Model to Walk Dto and Return

            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
            
        }

        //Get a Walk
        //Get:/api/walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            if(walkDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain Model To WalkDto
            return Ok(mapper.Map<WalkDto> (walkDomainModel));
        }

        // Update Walk By Id
        // PUT: /api/Walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            //Map Dto To Domain Model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        // Delete a Walk By Id
        // DELETE: /api/Walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedWalkDomainModel = await walkRepository.DeleteAsync(id);

            if (deletedWalkDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to DTO
            return Ok(mapper.Map<WalkDto>(deletedWalkDomainModel));
        }

    }
}
