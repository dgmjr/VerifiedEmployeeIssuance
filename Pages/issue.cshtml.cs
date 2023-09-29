using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web;
using MyAccountPage.Models;
using static System.Net.WebRequestMethods;
using static Microsoft.Graph.Constants;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;
using System.IO;

namespace MyAccountPage.Pages
{
    [Authorize(Policy = "alloweduser")]
    public class IssueModel : PageModel
    {
        private readonly GraphServiceClient _graphServiceClient;
        private readonly ILogger<IndexModel> _logger;

        public async Task<IActionResult> OnGet()
        {
            var _userdata = new UserData();
            try
            {
                var user = await _graphServiceClient.Me
                    .Request()
                    .Select(
                        "displayName,givenName,jobTitle,preferredLanguage,surname,mail,userPrincipalName"
                    )
                    .GetAsync();

                _userdata.displayName = user.DisplayName;
                _userdata.givenName = user.GivenName;
                _userdata.jobTitle = user.JobTitle;
                _userdata.preferredLanguage = user.PreferredLanguage;
                _userdata.surname = user.Surname;
                _userdata.mail = user.Mail;
                _userdata.revocationId = user.UserPrincipalName;

                try
                {
                    var photo = await _graphServiceClient.Me.Photo.Content.Request().GetAsync();
                    if (photo != null)
                    {
                        _userdata.photo = Convert.ToBase64String(
                            ((photo as MemoryStream)!).ToArray()
                        );
                    }
                    else
                    {
                        _userdata.photo = _userdata.defaultUserPhoto;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error getting photo");
                    _userdata.photo = _userdata.defaultUserPhoto;
                }
            }
            catch (ServiceException ex)
            {
                _logger.LogError(ex, "Error getting user data:" + ex.Message);
                return RedirectToPage("/Error");
            }

            ViewData["GraphApiResult"] = _userdata;
            return Page();
        }
    }
}
