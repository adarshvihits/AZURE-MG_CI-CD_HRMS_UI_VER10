using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Controllers
{
    public class BaseController : Controller
    {        
        public void Initialize()
        {
            var test = HttpContext.Session.GetString("JwtToken");
        }        
    }
}
