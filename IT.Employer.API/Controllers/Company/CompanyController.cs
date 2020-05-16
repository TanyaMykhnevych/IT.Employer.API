using IT.Employer.Domain.Models.User;
using IT.Employer.Entities.Models.Base;
using IT.Employer.Entities.Models.CompanyN;
using IT.Employer.Services.Services.CompanyN;
using IT.Employer.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IT.Employer.WebAPI.Controllers.CompanyN
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(CustomValidateModelAttribute))]
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _service;
        private readonly UserManager<AppUser> _userManager;

        public CompanyController(ICompanyService service, UserManager<AppUser> userManager)
        {
            _service = service;
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
        public IActionResult Filter([FromQuery]SearchCompanyParameterDTO parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            SearchResponseDTO<CompanyDTO> result = _service.SearchCompanies(parameters);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CompanyDTO model)
        {
            Guid created = await _service.Create(model, await GetCurrentUserId());

            return Ok(_service.GetById(created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]CompanyDTO model)
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


        private async Task<Guid> GetCurrentUserId()
        {
            string name = _userManager.GetUserName(User);
            return (await _userManager.FindByNameAsync(name)).Id;
        }
    }
}
