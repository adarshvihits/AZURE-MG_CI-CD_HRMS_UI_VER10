using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.General.Controllers
{
    [Area("General")]
    [SessionAuthorize]
    public class Mobile_MenuController : Controller
    {
        private readonly IMenuService _menuService;


        public Mobile_MenuController( IMenuService menuService)
        {        
            _menuService = menuService;
           
        }
        public async Task<IActionResult> Create()
        {
            MenuApplicability model = new MenuApplicability();
           model = await _menuService.GetMenuList();
            return View("Create",model);
        }


        public async Task<IActionResult> onMenuChange(int[] Code, bool[] Appl, string[] Name)
        {
            try
            {
                MenuApplicability fpa = new MenuApplicability();
                MenuApplicability model = new MenuApplicability();
                fpa.MenuArray = new PermissionArray();             
          
                fpa.MenuArray.Code = new int[Code.Length];
                fpa.MenuArray.Name = new string[Name.Length];
                fpa.MenuArray.Applicable = new bool[Appl.Length];
                fpa.MenuArray.Code = Code;
                fpa.MenuArray.Applicable = Appl;
                fpa.MenuArray.Name = Name;
   
                string data = JsonConvert.SerializeObject(fpa);

                model = await _menuService.GetSubMenuList(data, Code.Length);
                return PartialView("_SubMenu", model);
            }
            catch (UnauthorizedAccessException e)
            {

                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(MenuApplicability model)
        {
            try
            {
                var submenu = from li in model.SubMenuList
                           where li.SubMenuApplicability == true
                           select li;

                var menu = from li in model.MenuList
                           where li.MenuApplicability == true
                           select li;
                model.SubMenuList = submenu.ToList();
                model.MenuList = menu.ToList();
                var response = await _menuService.MobileMenuSave(model);
                return Json(response);
              
            }
            catch (UnauthorizedAccessException e)
            {

                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }

    }
}
