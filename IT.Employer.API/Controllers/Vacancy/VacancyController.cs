using IT.Employer.Domain.Models.User;
using IT.Employer.Entities.Models.Base;
using IT.Employer.Entities.Models.Vacancy;
using IT.Employer.Entities.Models.VacancyN;
using IT.Employer.Services.Services.VacancyN;
using IT.Employer.WebAPI.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IT.Employer.WebAPI.Controllers.VacancyN
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(CustomValidateModelAttribute))]
    public class VacancyController : Controller
    {
        private readonly IVacancyService _service;
        private readonly UserManager<AppUser> _userManager;

        public VacancyController(IVacancyService service, UserManager<AppUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_service.GetById(id));
        }

        [HttpGet]
        [Route("filter")]
        public async Task<IActionResult> Filter([FromQuery]SearchVacancyParameterDTO parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            parameters.UserId = await GetCurrentUserId();
            SearchResponseDTO<VacancyDTO> result = _service.SearchVacancies(parameters);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]VacancyDTO model)
        {
            model.UserId = await GetCurrentUserId();
            await _service.Create(model);

            return Ok(_service.GetById(model.Id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]VacancyDTO model)
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

        private async Task<Guid?> GetCurrentUserId()
        {
            string name = _userManager.GetUserName(User);
            return (await _userManager.FindByNameAsync(name))?.Id;
        }
    }
}
