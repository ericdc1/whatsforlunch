using System.Web.Mvc;
using Lunch.Website.Services;

namespace Lunch.Website.Controllers
{
    [LunchAuthorize(Roles = "User")]
    public class BaseController : Controller
    {
        
    }
}
