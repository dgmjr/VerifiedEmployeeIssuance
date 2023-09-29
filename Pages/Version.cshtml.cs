using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Graph;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace MyAccountPage.Pages
{
    public class VersionModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public VersionModel(ILogger<VersionModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void OnGet()
        {
            ViewData["VerifiedEmployeeId:manifest"] = _configuration["VerifiedEmployeeId:manifest"];
            ViewData["VerifiedEmployeeId:Authority"] = _configuration[
                "VerifiedEmployeeId:Authority"
            ];
            ViewData["VerifiedIDService:ClientId"] = _configuration["VerifiedIDService:ClientId"];
            ViewData["VerifiedIDService:TenantId"] = _configuration["VerifiedIDService:TenantId"];
        }
    }
}
