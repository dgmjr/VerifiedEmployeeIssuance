using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Graph;
using MyAccountPage.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace MyAccountPage.Pages
{
    public class AccessDeniedModel : PageModel
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        [BindProperty(SupportsGet = true)]
        public string requiredUserrole { get; set; }

        public AccessDeniedModel(ILogger<AccessDeniedModel> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            requiredUserrole = string.Empty;
        }

        public void OnGet()
        {
            requiredUserrole = _configuration["AzureAd:AllowedUsersRole"];
        }
    }
}
