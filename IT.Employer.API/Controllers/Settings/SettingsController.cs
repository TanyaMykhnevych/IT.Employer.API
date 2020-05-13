using IT.Employer.Services.Models.Settings;
using IT.Employer.WebAPI.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace IT.Employer.WebAPI.Controllers.Settings
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(CustomValidateModelAttribute))]
    public class SettingsController : Controller
    {
        private readonly AppSettings _appSettings;

        public SettingsController(IOptionsMonitor<AppSettings> appSettingsOptions)
        {
            _appSettings = appSettingsOptions.CurrentValue;
        }

        [HttpGet]
        public IActionResult GetApplicationSettings()
        {
            return Ok(_appSettings);
        }
    }
}
