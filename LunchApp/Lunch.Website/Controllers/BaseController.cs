using System.Web.Mvc;
using Lunch.Website.Services;

namespace Lunch.Website.Controllers
{
    [LunchAuthorize]
    public class BaseController : Controller
    {
        
    }
}
