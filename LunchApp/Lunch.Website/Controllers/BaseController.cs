using System.Web.Mvc;
using Lunch.Core.Logic;
using Lunch.Core.Logic.Implementations;
using Lunch.Core.Models;
using Lunch.Website.Services;
using StructureMap;

namespace Lunch.Website.Controllers
{
    [LunchAuthorize(Roles = "User")]
    public class BaseController : Controller
    {
        public User CurrentUser { get; set; }

        private readonly IUserLogic _userLogic;

        public BaseController()
        {
            _userLogic = ObjectFactory.GetInstance<IUserLogic>();
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            CurrentUser = _userLogic.Get(User.Identity.Name);
        }
    }
}
