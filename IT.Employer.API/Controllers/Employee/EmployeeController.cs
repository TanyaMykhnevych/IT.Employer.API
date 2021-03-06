﻿using IT.Employer.Domain.Models.User;
using IT.Employer.Entities.Models.Base;
using IT.Employer.Entities.Models.EmployeeN;
using IT.Employer.Services.Services.EmployeeN;
using IT.Employer.WebAPI.Controllers.Base;
using IT.Employer.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IT.Employer.WebAPI.Controllers.EmployeeN
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(CustomValidateModelAttribute))]
    [Authorize]
    public class EmployeeController : BaseUserAccessController
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service, UserManager<AppUser> userManager) : base(userManager)
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
        public async Task<IActionResult> Filter([FromQuery]SearchEmployeeParameterDTO parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            parameters.CurrentUserCompanyId = (await GetCurrentUser()).CompanyId;
            SearchResponseDTO<EmployeeDTO> result = _service.SearchEmployees(parameters);

            return Ok(result);
        }

        [HttpGet]
        [Route("filter/active/single")]
        public async Task<IActionResult> FilterSingleActiveEmployees([FromQuery]SearchEmployeeParameterDTO parameters)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            parameters.CurrentUserCompanyId = (await GetCurrentUser()).CompanyId;
            SearchResponseDTO<EmployeeDTO> result = _service.SearchSingleActiveEmployees(parameters);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]EmployeeDTO model)
        {
            Guid createdId = await _service.Create(model);

            return Ok(_service.GetById(createdId));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody]EmployeeDTO model)
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
