using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicHub.IdentityServer.Attributes;

namespace MusicHub.IdentityServer.Controllers
{
    [Authorize]
    [SecurityHeaders]
    public class ProfileController : Controller
    {
        public ProfileController()
        {

        }

        public IActionResult Index()
        {
            var usr = User;

            return View("Index", usr.Identity.Name);
        }
    }
}
