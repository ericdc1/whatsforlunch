using System.Web.Routing;
using System;
using System.Web.Mvc;
using StructureMap;

namespace Lunch.Website.DependencyResolution
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
                return base.GetControllerInstance(requestContext, null);
            var controller = ObjectFactory.GetInstance(controllerType);
            return (IController)controller;
        }
    }
}
