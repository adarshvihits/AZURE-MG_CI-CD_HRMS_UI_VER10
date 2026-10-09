using ClosedXML.Excel;
using ExcelDataReader;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.Services.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class RequestVsAuthorityController : Controller
    {
        private readonly IRequestVsAuthorityService _requestVsAuthorityService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IWebHostEnvironment _environment;
        private readonly IReporteesService _reportees;
        public RequestVsAuthorityController(IRequestVsAuthorityService requestVsAuthorityService, IDropdownService dropdownService, ICommonService commonService,IHelpers helpers, 
            IWebHostEnvironment environment, IReporteesService reportee)
        {
            _requestVsAuthorityService = requestVsAuthorityService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
            _environment = environment;
            _reportees = reportee;
        }
     
        #region REQUEST VS AUTHORITY
        public async Task<IActionResult> Index()
        {
            try { 
            RequestVsAuthority model = new RequestVsAuthority();
            model.Tabindex = 0;
            model.SearchId = 0;
            Filter filter = new Filter();

            ViewBag.Una= new SelectList(await _dropdownService.GetRequestListDropDown(), "Code", "Name");
               model.Filter = await _commonService.GetFilterDropdowns();

                ViewBag.ReqType = new SelectList(await _dropdownService.GetRequestTypeDropDown(), "Code", "Name");
                ViewBag.AuthLevel = new SelectList(await _dropdownService.GetRequestLevelDropDown(0), "Code", "Name");
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
        public async Task<IActionResult> GetList(Filter filter1, int TabIndex, int RequestCode, int LEVEL)
        {
            try
            {
                RequestVsAuthority model = new RequestVsAuthority();
                RequestVsAuthority m = new RequestVsAuthority();
                model.EmployeeHeader = new EmployeeHeader();

                Filter filter = new Filter();
               model.Filter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _requestVsAuthorityService.GetRequestVsAuthorityList(TabIndex, filter1.SearchId, filter1, RequestCode, LEVEL);

                
                m.RequestVsAuthorityList = model.RequestVsAuthorityList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.RequestVsAuthorityList == null || model.RequestVsAuthorityList.Count() == 0)
                {
                    
                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        count=model,
                        recordsFiltered = totalrows
                    });
                }




                if (model.RequestVsAuthorityList.Count > 0)
                {
                    totalrows = m.RequestVsAuthorityList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

               
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.RequestVsAuthorityList[0].totalRecords;
                    return Json(new
                    {
                        data = model.RequestVsAuthorityList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        count = model,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.RequestVsAuthorityList,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        count = model,
                        recordsFiltered = totalrows
                    });
                }


            }
            catch (Exception ex)
            {
                return View();
            }
        }

        //public async Task<JsonResult> GetTabCount()
        //{
        //    RequestVsAuthority model = new RequestVsAuthority();
        //    model.AllTabCount = Global.AllTabCount;
        //    model.Assigned = Global.AssignedCount;
        //    model.NotAssigned = Global.NotAssignedCount;

        //    return Json(model);
        //}


      


        public async Task<IActionResult> ApplyFilter(int TabIndex, int SearchId, Filter filter, int RequestCode, int LEVEL)
        {
            try { 
            RequestVsAuthority model = new RequestVsAuthority();
            model.Filter = filter;
            model = await _requestVsAuthorityService.GetRequestVsAuthorityList(TabIndex, SearchId, filter, RequestCode, LEVEL);
                if (TabIndex == 1)
                    return PartialView("_AssignedList", model);
              
                else if(TabIndex == 2)
                    return PartialView("_NotAssignedList", model);
                else
                   
                return PartialView("_RequestVsAuthorityList", model);
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

        public async Task<IActionResult> GetRequestVsAuthorityList(int TabIndex, int SearchID)
        {
            RequestVsAuthority model = new RequestVsAuthority();
            model.Filter = new Filter();
            
            if (TabIndex == 1)
                return PartialView("_AssignedList");

            else if (TabIndex == 2)
                return PartialView("_NotAssignedList");
            else

                return PartialView("_RequestVsAuthorityList");
        }
        public async Task<IActionResult> BatchUpdate()
        {
            RequestVsAuthority model = new RequestVsAuthority();
            ViewBag.Rqt = new SelectList(await _dropdownService.GetRequestTypeDropDown(), "Code", "Name");
            ViewBag.Rql = new SelectList(await _dropdownService.GetRequestLevelDropDown(0), "Code", "Name");
            ViewBag.Aut = new SelectList(await _dropdownService.GetRequestAuthorityDropDown(), "Code", "Name");
            return PartialView("_BatchSave", model);
        }
        [HttpPost]
        public async Task<IActionResult> BatchUpdate(RequestVsAuthority model, int Tabindex, int SearchId, Filter Filter)
        {
            try
            {
                //    if (Global.GlobalFilter == null)
                //    Global.GlobalFilter = new Filter();
                //model.Filter = Global.GlobalFilter;
                model.Filter = new Filter();
                model.Filter = Filter;
            var response = await _requestVsAuthorityService.RequestVsAuthorityBatchUpdate(model,Tabindex,SearchId);
                if (response.Success == true)
                    response.Message = "Updated Successfully";
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
        [HttpPost]
        public async Task<IActionResult> RequestVsAuthorityBatchUpdateValidation(RequestVsAuthority model, int Tabindex, int SearchId, Filter Filter)
        {
            try
            {
                //model.Filter = Global.GlobalFilter;
                model.Filter = new Filter();
                model.Filter = Filter;
                var response = await _requestVsAuthorityService.RequestVsAuthorityBatchUpdateValidation(model, Tabindex, SearchId);

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
        [HttpPost]
        public async Task<IActionResult> ExistCheck(RequestVsAuthority model)
        {
            try
            {
                RequestVsAuthority mmodel = new RequestVsAuthority();
                var response = await _requestVsAuthorityService.AlreadyExistCheck(model);
                
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
        public async Task<IActionResult> RequestAuthority(Guid EmployeeGI)
        {
            try { 
            RequestVsAuthority model = new RequestVsAuthority();
            RequestVsAuthority m = new RequestVsAuthority();
            ViewBag.Rqt = new SelectList(await _dropdownService.GetRequestTypeDropDown(), "Code", "Name");
            ViewBag.Aut = new SelectList(await _dropdownService.GetRequestAuthorityDropDown(), "Code", "Name");
            model = await _requestVsAuthorityService.GetRequestVsAuthorityLevelList(EmployeeGI, 2);
            m= await _requestVsAuthorityService.GetRequestVsAuthorityLogList(EmployeeGI);
            model.EmployeeHeader= await _commonService.GetEmployeeHeader(EmployeeGI);
            model.AuthorityLevel = new AuthorityLevel();
            model.AddLevel = new AddLevel();
            model.LogDetails = m.LogDetails;
            model.EmployeeGI = EmployeeGI;
            model.BatchUpdate = new BatchUpdate();
            model.BatchUpdate.RequestType = new RequestType();
            model.BatchUpdate.RequestType.Code = 2;
            return View("~/Areas/DataBank/Views/RequestVsAuthority/RequestVsAuthority.cshtml", model);
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
        public async Task<IActionResult>GetLevelList(Guid EmployeeGI,int ReqTypeId)
        {
            try { 
            RequestVsAuthority model = new RequestVsAuthority();
            ViewBag.Aut = new SelectList(await _dropdownService.GetRequestAuthorityDropDown(), "Code", "Name");
            model = await _requestVsAuthorityService.GetRequestVsAuthorityLevelList(EmployeeGI, ReqTypeId);
            return PartialView("_LevelList", model);
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
        public async Task<IActionResult> GetLogList(Guid EmployeeGI)
        {
            try { 
            RequestVsAuthority model = new RequestVsAuthority();
            model = await _requestVsAuthorityService.GetRequestVsAuthorityLogList(EmployeeGI);
            return PartialView("_LogList", model);
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
        public async Task<IActionResult> AddLevel(Guid EmployeeGI,int code)
        {
            try { 
            RequestVsAuthority model = new RequestVsAuthority();
            
            model =await _requestVsAuthorityService.GetRequestVsAuthorityDetailsAddNew(EmployeeGI, code);
            model.AddLevel.RequestType = code;
            model.AddLevel.Mode = "Create";
            ViewBag.Aut = new SelectList(await _dropdownService.GetRequestAuthorityDropDown(), "Code", "Name");
            return PartialView("_AddLevel", model);
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
        public async Task<IActionResult> AddLevel(RequestVsAuthority model,string RequestTypeName)
        {
            try { 
            var response = await _requestVsAuthorityService.RequestVsAuthoritySave_Delete(model);
            if (response.Success == true)
            {
                if (model.AddLevel.Mode == "Create")
                    response.Message = RequestTypeName + " Authority Level Added";
                else if (model.AddLevel.Mode == "MODIFY")
                    response.Message = RequestTypeName + " Authority Level Modified";
                else
                    response.Message = RequestTypeName + " Deleted Successfully";
            }
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
        public async Task<IActionResult> RequestTypeChange(Guid EmployeeGI, int RequestID)
        {
            try { 
            //Global.RequestTypeName = ReqTypeName;
            RequestVsAuthority model = new RequestVsAuthority();
            ViewBag.Aut = new SelectList(await _dropdownService.GetRequestAuthorityDropDown(), "Code", "Name");
            model = await _requestVsAuthorityService.GetRequestVsAuthorityLevelList(EmployeeGI, RequestID);
            return PartialView("_LevelList", model);
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
        public async Task<IActionResult> LevelDetails(Guid EmpGI,Guid ReqVsAuthGI,string mode)
        {
            try { 
            RequestVsAuthority model = new RequestVsAuthority();
            model= await _requestVsAuthorityService.GetRequestVsAuthorityLevelDetails(EmpGI, ReqVsAuthGI);          
            model.AddLevel.RequestType = 0;
            model.AddLevel.Mode =mode;
            ViewBag.Aut = new SelectList(await _dropdownService.GetRequestAuthorityDropDown(), "Code", "Name");
            return PartialView("_AddLevel", model);
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
     


        public async Task<IActionResult> ImportFromExcel(RequestVsAuthority model)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            string fileName = model.Excel.FileData.FileName;
            try
            {
                model.UnMatchedList = new List<RequestVsAuthority>();
                string wwwPath = this._environment.WebRootPath;
                string contentPath = this._environment.ContentRootPath;


                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    model.Excel.FileData.CopyTo(stream);

                }
                try
                {
                    using (var stream = System.IO.File.Open(path + "\\" + fileName, FileMode.Open, FileAccess.Read))
                    {

                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            List<RequestVsAuthority> List = new List<RequestVsAuthority>();
                            var readerrr = ExcelReaderFactory.CreateReader(stream);
                            while (reader.Read()) //Each row of the file
                            {
                                var item = new RequestVsAuthority();
                                if (reader.GetValue(0) != null)
                                {
                                    item.EmpId = reader.GetValue(0).ToString();
                                }
                                if (reader.GetValue(1) != null)
                                {
                                    item.EmployeeName = reader.GetValue(1).ToString();
                                }
                                if (reader.GetValue(2) != null)
                                {
                                    item.AuthorityId = reader.GetValue(2).ToString();
                                }
                               if (reader.GetValue(3) != null)
                                {
                                    item.AuthorityName = reader.GetValue(3).ToString();
                                }
                               
                               // else
                                //{
                                    List.Add(item);
                                //}
                            }
                            if (List[0].EmpId != "Employee ID" || List[0].EmployeeName != "Employee Name" || List[0].AuthorityId != "Authority ID" || List[0].AuthorityName != "Authority Name")
                            {
                                // Check if file exists with its full path    
                                if (System.IO.File.Exists(path + "\\" + fileName))
                                {
                                    System.IO.File.Delete(path + "\\" + fileName);
                                }
                                return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                            }
                            model.UnMatchedList = List;

                            model.UnMatchedList.RemoveAt(0);

                        }
                    }
                }
                catch (Exception e)
                {
                    // Check if file exists with its full path    
                    if (System.IO.File.Exists(path + "\\" + fileName))
                    {
                        System.IO.File.Delete(path + "\\" + fileName);
                    }
                    return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                }

                model.Excel.FileData = null;
                var response = await _requestVsAuthorityService.ImportFromExcel(model);
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }

                if (response.Success != true && response.Data !=null)
                {
                    model.UnMatchedList = response.Data;
                    model.Message = response.Message;
                    return PartialView("_UnMatchedList", model);
                }
                else
                {
                    return Json(response);
                }
                // Check if file exists with its full path    

            }
            catch (UnauthorizedAccessException e)
            {

                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                // Check if file exists with its full path    
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }
                return RedirectToAction("Error", "Account", new { Area = "" });
            }


        }



        public async Task<IActionResult> ExportToExcel(int TabIndex, int SearchId, string TabName, int totRecords,int RequestTypeCode,int AuthorityLevelCode,Filter filter)
        {
            try
            {

                filter.length = totRecords;
                filter.start = 0;
                string FilterValues = await _helpers.GetFilterValues(filter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Request Vs Authority (" + TabName + ")", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _requestVsAuthorityService.GetRequestVsAuthorityList_Excel(TabIndex, SearchId, RequestTypeCode, AuthorityLevelCode, filter);
                DataTable dt = dataset.Tables[0];
                dataset.Tables[0].Rows.RemoveAt(0);
                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Request Vs Authority ");
                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:N" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:N" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:N" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:N" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:N" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 13)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Request Vs Authority (" + TabName + ")";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Login User";
                    worksheet.Cell(3, 2).Value = LoginUser;
                    worksheet.Cell(3, 3).Style.Font.Bold = true;
                    worksheet.Cell(3, 3).Value = "Date & Time";
                    worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(3, 4).Value = currDateTime.ToString();
                    worksheet.Cell(3, 5).Style.Font.Bold = true;
                    worksheet.Cell(3, 5).Value = "Total Count";
                    worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 6).Value = dt.Rows.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:N4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:N5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:N5").Style.Font.Bold = true;

                    worksheet.Range("G5:G" + (dt.Rows.Count + 5)).Style.DateFormat.Format = "dd-MMM-yyyy";

                    worksheet.Columns().AdjustToContents();
                    worksheet.Rows().AdjustToContents();
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        if (EP.NeedPassword)
                        {
                            Workbook book = new Workbook();
                            book.LoadFromStream(stream);
                            //Protect Workbook
                            book.Protect(EP.Password,false,false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Request Vs Authority " + TabName + " List.xlsx"
                            );
                    }
                }
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


        #endregion
        #region  CHANGE AUTHORITY

        public async Task<IActionResult> ChangeAuthority_Index()
        {
            Reportees model = new Reportees();
            model.Filter = await _commonService.GetFilterDropdowns();
            ViewBag.Authority = new SelectList(await _dropdownService.GetAuthorityDropDown(0, 0), "Code", "Name");
            ViewBag.Type = new SelectList(await _dropdownService.GetRequestTypeDropDown(), "Code", "Name");
            ViewBag.Level = new SelectList(await _dropdownService.GetRequestLevelDropDown(0), "Code", "Name");
            ViewBag.ChangeAuthority = new SelectList(await _dropdownService.GetRequestAuthorityDropDown(), "Code", "Name");
            return View("~/Areas/DataBank/Views/RequestVsAuthority/ChangeAuthority/ChangeAuthorityIndex.cshtml", model);
        }

        [HttpGet]
        public async Task<JsonResult> GetLevelDropDown(int TypeCode)
        {
            SelectList data = new SelectList("", "Code", "Name");
            data = new SelectList(await _dropdownService.GetRequestLevelDropDown(TypeCode), "Code", "Name");
            return Json(data);
        }
        [HttpGet]
        public async Task<JsonResult> GetAuthorityDropDown(int TypeCode, int LevelCode)
        {
            SelectList data = new SelectList("", "Code", "Name");
            data = new SelectList(await _dropdownService.GetAuthorityDropDown(TypeCode, LevelCode), "Code", "Name");
            return Json(data);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeAuthority_EmployeeList(Filter filter1, int Authority, int Type, int Level)
        {
            try
            {
                Reportees model = new Reportees();            


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                if(Authority!=0)
                model = await _reportees.GetReporteesList(Type, Level, Authority, filter1);
           
                int totalrows = 0;

                int totalrowsafterfiltering = 0;

                if (model.ReporteesList == null || model.ReporteesList.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,


                        recordsFiltered = totalrows
                    });
                }



                if (model.ReporteesList.Count > 0)
                {
                    totalrows = model.ReporteesList[0].active;


                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = model.ReporteesList[0].totalRecords;
                    return Json(new
                    {
                        data = model.ReporteesList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,

                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.ReporteesList,
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

        [HttpPost]
        public async Task<IActionResult> ChangeAuthoritySave(Reportees input)
        {
            try
            {

                var response = await _requestVsAuthorityService.EmployeeVSChangeAuthorityBatchUpdate(input);
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
        #endregion



    }
}
