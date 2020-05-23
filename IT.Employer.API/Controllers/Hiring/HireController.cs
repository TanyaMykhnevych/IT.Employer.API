using IT.Employer.Entities.Models.Hiring;
using IT.Employer.Services.HubN;
using IT.Employer.Services.Services.Hiring;
using IT.Employer.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IT.Employer.WebAPI.Controllers.Hiring
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(CustomValidateModelAttribute))]
    [Authorize]
    public class HireController : Controller
    {
        private readonly IHireService _hireService;
        private readonly IHubContext<NotificationHub> _notificationHubContext;

        public HireController(IHireService hireService, IHubContext<NotificationHub> notificationHubContext)
        {
            _hireService = hireService;
            _notificationHubContext = notificationHubContext;
        }

        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            HireDTO hire = _hireService.GetHireById(id);

            return Ok(hire);
        }

        [HttpGet("company/{companyId:guid}")]
        public IActionResult GetMyHires(Guid companyId)
        {
            List<HireDTO> hires = _hireService.GetCompanyOffers(companyId);

            return Ok(hires);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HireDTO hire)
        {
            Guid id = await _hireService.CreateHire(hire);
            await _notificationHubContext.Clients.All.SendAsync("ReceiveOffer", _hireService.GetHireById(id));

            return Ok(id);
        }

        [HttpPut("{id:guid}/approve")]
        public async Task<IActionResult> Approve(Guid id)
        {
            await _hireService.ApproveHire(id);

            return Ok();
        }

        [HttpPut("{id:guid}/decline")]
        public async Task<IActionResult> Decline(Guid id)
        {
            await _hireService.DeclineHire(id);

            return Ok();
        }
    }
}
