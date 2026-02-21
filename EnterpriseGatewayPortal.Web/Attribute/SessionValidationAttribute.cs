using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
namespace EnterpriseGatewayPortal.Web.Attribute
{
    public class SessionValidationAttribute: ActionFilterAttribute, IResultFilter
    {
        private readonly ILogger<SessionValidationAttribute> _logger;
        public SessionValidationAttribute(ILogger<SessionValidationAttribute> logger)
        {
            _logger = logger;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _logger.LogInformation("--> Custom Attribute :: OnActionExecuting");
            var RequestPath = filterContext.HttpContext.Request.Path.Value;
            bool isbulksignurl = RequestPath.StartsWith("/BulkSign") || RequestPath.StartsWith("/DocumentTemplates");

            // Check if the user is authenticated
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                bool isAjax = filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
                if (isAjax)
                {
                    filterContext.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                    { "Controller", "Login" },
                    { "Action", "Index" }
                        });
                }
            }
            if(isbulksignurl)
            {
                var ugpassLogin = filterContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "IDPLogin").Value;
                if (ugpassLogin == "false")
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary {
                    { "Controller", "Dashboard" },
                    { "Action", "Index1" }
                        });
                }
                //if(ugpassLogin == "true")
                //{
                //    var bulksign = filterContext.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "BulkSign").Value;
                //    if (bulksign == "false")
                //    {
                //        filterContext.Result = new RedirectToRouteResult(
                //            new RouteValueDictionary {
                //    { "Controller", "Dashboard" },
                //    { "Action", "Index1" }
                //            });
                //    }
                //}
                
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
