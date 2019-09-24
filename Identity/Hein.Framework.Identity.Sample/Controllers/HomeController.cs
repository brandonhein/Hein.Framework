using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hein.Framework.Identity.Sample.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var name = new GetUserNameService().GetName();
            return Ok(new { userName = name });
        }
    }

    public class GetUserNameService
    {
        public string GetName()
        {
            //use Hein.Framework.Identity.HttpContext to get user
            return HttpContext.Current.User.Identity.Name;
        }
    }
}
