using Hrms.Lite.Shared.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Hrms.Lite.Infrastructure.Helpers
{
    public class SessionAuthorize : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var redirectTarget = new RouteValueDictionary { { "area", "" }, { "action", "Login" }, { "controller", "Account" } };
            var SSO_redirectTarget = new RouteValueDictionary { { "area", "" }, { "action", "Logout" }, { "controller", "Account" } };
            try
            {
                var session = filterContext.HttpContext.Session;
                var descriptor = filterContext.ActionDescriptor as ControllerActionDescriptor;              
                string _Controller = descriptor.ControllerName;
               
                string _Action = descriptor.ActionName;
                string[] _ExceptionAction = { "User:Login", "User:Logout", "User:ChangePassword", "Home:ShowNotifications", "User:ForgotPassword", "User:ResetPassword" };



                string json = session.GetString("User");
                int isRedirected = 0;
                if (!string.IsNullOrEmpty(json))
                {
                    var crede = JsonConvert.DeserializeObject<Login>(json);
                    isRedirected = crede.isRedirected;
                }


                if (_ExceptionAction.Contains(String.Format("{0}:{1}", _Controller, _Action)))
                {
                    return;
                }


                var urlconfig = filterContext.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
               
                var settingValue = urlconfig["URL_Editing"];

                if (settingValue == "false" && isRedirected==0)  //urlediting not possible
                {
                    if (session.GetString("JwtToken") == "" || session.GetString("JwtToken") == null)
                    {
                        filterContext.Result = new RedirectToRouteResult(redirectTarget);
                    }
                    else
                    {

                        string strPreviousPage = "";

                        // Check if there is a URL referrer
                        if (filterContext.HttpContext.Request.Headers.ContainsKey("Referer"))
                        {
                            var refererUrl = filterContext.HttpContext.Request.Headers["Referer"].ToString();
                            var uri = new Uri(refererUrl);
                            strPreviousPage = uri.Segments[uri.Segments.Length - 1];
                        }

                        // If the previous page is empty, redirect to the login page
                        if (string.IsNullOrEmpty(strPreviousPage))
                        {
                            redirectTarget = new RouteValueDictionary { { "area", "" }, { "action", "Logout" }, { "controller", "Account" } };
                            filterContext.Result = new RedirectToRouteResult(redirectTarget);
                        }
                    }
                }
                if (settingValue == "true" && isRedirected == 0)  //urlediting  possible
                {
                    if (session.GetString("JwtToken") != "" && session.GetString("JwtToken") != null) return;
                    else filterContext.Result = new RedirectToRouteResult(redirectTarget);

                }

                //----------------------------SSO USERS ------------------------

                if (settingValue == "false" && isRedirected == 1)  //urlediting not possible
                {
                    if (session.GetString("JwtToken") == "" || session.GetString("JwtToken") == null)
                    {
                        filterContext.Result = new RedirectToRouteResult(SSO_redirectTarget);
                    }
                    else
                    {

                        string strPreviousPage = "";

                        // Check if there is a URL referrer
                        if (filterContext.HttpContext.Request.Headers.ContainsKey("Referer"))
                        {
                            var refererUrl = filterContext.HttpContext.Request.Headers["Referer"].ToString();
                            var uri = new Uri(refererUrl);
                            strPreviousPage = uri.Segments[uri.Segments.Length - 1];
                        }

                        // If the previous page is empty, redirect to the login page
                        if (string.IsNullOrEmpty(strPreviousPage))
                        {
                            
                            filterContext.Result = new RedirectToRouteResult(SSO_redirectTarget);
                        }
                    }
                }
                if (settingValue == "true" && isRedirected == 1)  //urlediting  possible
                {
                    if (session.GetString("JwtToken") != "" && session.GetString("JwtToken") != null) return;
                    else filterContext.Result = new RedirectToRouteResult(SSO_redirectTarget);

                }

            }
            
            catch (Exception e)
            {                
                    filterContext.Result = new RedirectToRouteResult(SSO_redirectTarget);              

            }
        }


    }
}