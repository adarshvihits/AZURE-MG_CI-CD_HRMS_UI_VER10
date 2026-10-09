using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Hrms.Lite.Infrastructure.Helpers;
using System.Collections.Generic;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Hrms.Lite.Shared.Account;
using System.Data;
using ClosedXML.Excel;
using System.Linq;
using System.IO;
using Spire.Xls;
using Newtonsoft.Json;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class EmployeeVsRenewalDateController : Controller
    {
        private readonly IEmployeeVsRenewalDateService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public EmployeeVsRenewalDateController(IEmployeeVsRenewalDateService service, ICommonService commonService, IHelpers Helpers)
        {
            _service = service;
            _commonService = commonService;
            _helpers = Helpers;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                EmployeeVsRenewalDate model = new EmployeeVsRenewalDate();
                model.Filter = await _commonService.GetFilterDropdowns();
                return View(model);
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

        public async Task<IActionResult> GetEmployeeVsRenewalDateList(Filter filter1, int TabIndex)
        {
            {
                try
                {
                    EmployeeVsRenewalDate model = new EmployeeVsRenewalDate();

                    if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                    {
                        filter1.searchValue = "";
                    }

                    model = await _service.GetList(TabIndex, filter1);

                    int totalrows = 0;
                    int totalrowsafterfiltering = 0;

                    if (model.EmployeeVsRenewalDateList == null || model.EmployeeVsRenewalDateList.Count == 0)
                    {

                        return Json(new
                        {
                            data = model.ActiveCount,

                            draw = filter1.draw,
                            recordsTotal = totalrows,
                            recordsFiltered = totalrows
                        });
                    }

                    if (model.EmployeeVsRenewalDateList.Count > 0)
                    {

                        if (filter1.Tabindex == 0)
                        {
                            totalrows = model.EmployeeVsRenewalDateList[0].active;
                        }
                        else
                        {
                            totalrows = model.EmployeeVsRenewalDateList[0].active;
                        }
                    }

                    if (filter1.searchValue != "")
                    {
                        totalrowsafterfiltering = model.EmployeeVsRenewalDateList[0].totalRecords;
                        return Json(new
                        {
                            data = model.EmployeeVsRenewalDateList,
                            draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                            recordsTotal = totalrows,
                            recordsFiltered = totalrows//totalrowsafterfiltering
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            data = model.EmployeeVsRenewalDateList,
                            draw = filter1.draw,//Request.Form["draw"], 
                            recordsTotal = totalrows,
                            recordsFiltered = totalrows
                        });
                    }


                }
                catch (Exception ex)
                {
                    return View();
                }
            }
        }

        public async Task<ActionResult> Create(Guid EmployeeGI)
        {
            try
            {

                EmployeeVsRenewalDate model = await _service.GetEmployeeVsRenewalDateDetails(EmployeeGI, "CREATE");

                if (model.RenewalDate == default(DateTime))
                {
                    model.RenewalDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); 
                }


                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                model.Mode = "CREATE";

                return PartialView("_CreateOrEdit", model);
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

        public async Task<ActionResult> Edit(Guid EmployeeGI)
        {
            try
            {
                EmployeeVsRenewalDate model = await _service.GetEmployeeVsRenewalDateDetails(EmployeeGI, "MODIFY");
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                model.Mode = "MODIFY";
                return PartialView("_CreateOrEdit", model);
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







        public async Task<ActionResult> Save(EmployeeVsRenewalDate input)
        {
            try
            {
                var response = await _service.Create(input);

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


        public async Task<ActionResult> Delete(EmployeeVsRenewalDate model)
        {
            try
            {
                var response = await _service.Delete(model);
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

