using IT.Employer.Domain.Models.User;
using IT.Employer.Entities.Models.Base;
using IT.Employer.Entities.Models.Team;
using IT.Employer.Services.Services.TeamN;
using IT.Employer.WebAPI.Controllers.Base;
using IT.Employer.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IT.Employer.WebAPI.Controllers.TeamN
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(CustomValidateModelAttribute))]
    [Authorize]
    public class TeamController : BaseUserAccessController
    {
        private readonly ITeamService _service;

        public TeamController(ITeamService service, UserManager<AppUser> userManager) : base(userManager)
        {
            _service = service;
        }

        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_service.GetById(id));
        }

        [HttpGet]
        [Route("filter")]
        public async Task<IActionResult> Filter([FromQuery]SearchTeamParameterDTO parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            parameters.CurrentUserCompanyId = (await GetCurrentUser()).CompanyId;
            SearchResponseDTO<TeamSearchItemDTO> result = _service.SearchTeams(parameters);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TeamDTO model)
        {
            await _service.Create(model);

            return Ok(_service.GetById(model.Id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]TeamDTO model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            model.Id = id;
            await _service.Update(model);

            return Ok(_service.GetById(model.Id));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.Delete(id);

            return Ok();
        }
    }
}
