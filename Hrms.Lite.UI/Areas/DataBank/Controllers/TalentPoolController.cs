using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class TalentPoolController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ITalentPoolService _talentPoolService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;

        public static class Global
        {
            public static TalentPool GlobalTalentPool;
            public static Filter GlobalFilter;
            public static bool IsRefreshed = true;

        }
        public TalentPoolController(ITalentPoolService TalentPoolService, IDropdownService dropdownService, ICommonService commonService,IHelpers Helpers)
        {
            _talentPoolService = TalentPoolService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;
        }
        public async Task<IActionResult> Index()
        {
            try { 
            TalentPool TP = new TalentPool();
                TalentPool Skill = new TalentPool();
                ViewBag.Gen = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
            TP = await _talentPoolService.GetClassificationDetails();
                Skill = await _talentPoolService.GetSkillGraph();


                ViewBag.Operators = new SelectList(await _dropdownService.GetOperators(), "Code", "Name");
                TP.Filter = await _commonService.GetFilterDropdowns();
                TP.Gender = new Gender();
            TP.TalentPoolList = new List<TalentPoolList>();
            if (!Global.IsRefreshed)
            {
                Global.IsRefreshed = true;
                TalentPool model = new TalentPool();
                TalentPool TempModel = new TalentPool();
                model = Global.GlobalTalentPool;
                TP = Global.GlobalTalentPool;
                TP.TalentPoolList = new List<TalentPoolList>();
                TempModel = await _talentPoolService.GetTalentPoolDetails(Global.GlobalTalentPool);
                TP.TalentPoolList = TempModel.TalentPoolList;
                if (model.Education != null)
                {
                    var Edu = from li in model.Education
                              where li.APPLICABILITY == true
                              select li;
                    TempModel.Education = Edu.ToList();
                }
                if (model.Languages != null)
                {
                    var Lang = from li in model.Languages
                               where li.Read == true || li.Speak == true || li.Write
                               select li;
                    TempModel.Languages = Lang.ToList();
                }
                if (model.SkillProficiency != null)
                {
                    var SkillProf = from li in model.SkillProficiency
                                    where li.APPLICABILITY == true
                                    select li;
                    TempModel.SkillProficiency = SkillProf.ToList();
                }
                if (model.Skills != null)
                {
                    var Skills = from li in model.Skills
                                 where li.APPLICABILITY == true
                                 select li;
                    TempModel.Skills = Skills.ToList();
                }
                for (int j = 0; j < TempModel.Education.Count; j++)
                {
                    for (int i = 0; i < TP.Education.Count; i++)
                    {
                        if (TempModel.Education[j].NAME == TP.Education[i].NAME)
                        {
                            TP.Education[i].APPLICABILITY = true;
                        }
                    }
                }
                for (int j = 0; j < TempModel.Languages.Count; j++)
                {
                    for (int i = 0; i < TP.Languages.Count; i++)
                    {
                        if (TempModel.Languages[j].NAME == TP.Languages[i].NAME)
                        {
                            TP.Languages[i].APPLICABILITY = true;
                        }
                    }
                }
                for (int j = 0; j < TempModel.SkillProficiency.Count; j++)
                {
                    for (int i = 0; i < TP.SkillProficiency.Count; i++)
                    {
                        if (TempModel.SkillProficiency[j].NAME == TP.SkillProficiency[i].NAME)
                        {
                            TP.SkillProficiency[i].APPLICABILITY = true;
                        }
                    }
                }
                for (int j = 0; j< TempModel.Skills.Count;j++)
                {
                    for(int i=0;i<TP.Skills.Count;i++)
                    {
                        if(TempModel.Skills[j].NAME == TP.Skills[i].NAME)
                        {
                            TP.Skills[i].APPLICABILITY = true;
                        }
                    }
                }
                    
                    TP.Gender = model.Gender;
                TP.AgeFrom = model.AgeFrom;
                TP.AgeTo = model.AgeTo;
                TP.YOSFrom = model.YOSFrom;
                TP.YOSTo = model.YOSTo;                
                TP.AgeAboveOrBelow = Global.GlobalTalentPool.AgeAboveOrBelow;
                TP.YSAboveOrBelow = Global.GlobalTalentPool.YSAboveOrBelow;
            }
                TP.SkillGraph = Skill.SkillGraph;
                TP.SkillProficiencyGraph = Skill.SkillProficiencyGraph;
                TP.EducationGraph = Skill.EducationGraph;
                TP.LanguagesGraph = Skill.LanguagesGraph;
                return View(TP);
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
        public async Task<IActionResult> GetList(Filter filter1,TalentPool model)
        {
            try
            {
                TalentPool TP = new TalentPool();
                TalentPool m = new TalentPool();
                Global.GlobalTalentPool = model;
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                model.Filter = filter1;
             

                

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                TP = await _talentPoolService.GetTalentPoolDetails(model);

                if (model.Education != null)
                {
                    var Edu = from li in model.Education
                              where li.APPLICABILITY == true
                              select li;
                    TP.Education = Edu.ToList();
                }
                if (model.Languages != null)
                {
                    var Lang = from li in model.Languages
                               where li.Read == true || li.Speak == true || li.Write
                               select li;
                    TP.Languages = Lang.ToList();
                }
                if (model.SkillProficiency != null)
                {
                    var SkillProf = from li in model.SkillProficiency
                                    where li.APPLICABILITY == true
                                    select li;
                    TP.SkillProficiency = SkillProf.ToList();
                }
                if (model.Skills != null)
                {
                    var Skills = from li in model.Skills
                                 where li.APPLICABILITY == true
                                 select li;
                    TP.Skills = Skills.ToList();
                }
                TP.Gender = model.Gender;
                TP.AgeFrom = model.AgeFrom;
                TP.AgeTo = model.AgeTo;
                TP.YOSFrom = model.YOSFrom;
                TP.YOSTo = model.YOSTo;
                TP.YSAboveOrBelow = new Operators();
                TP.AgeAboveOrBelow = new Operators();
                TP.AgeAboveOrBelow = model.AgeAboveOrBelow;
                TP.YSAboveOrBelow = model.YSAboveOrBelow;
                TP.AllSkills = model.AllSkills;
                TP.AllSkillProficiency = model.AllSkillProficiency;
                TP.AllEducation = model.AllEducation;



                m.TalentPoolList = TP.TalentPoolList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (TP.TalentPoolList == null || TP.TalentPoolList.Count() == 0)
                {
                    

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (TP.TalentPoolList.Count > 0)
                {
                    totalrows = TP.TalentPoolList[0].active;

                   
                }

             
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = TP.TalentPoolList[0].totalRecords;
                    return Json(new
                    {
                        data = TP.TalentPoolList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = TP.TalentPoolList,
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








        public async Task<IActionResult> TalentPoolList()
        {
            try { 
            Global.IsRefreshed = false;
            return RedirectToAction("Index");
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
        public async Task<IActionResult> GetTalentPoolDetails()
        {
            try { 
            
            TalentPool TP = new TalentPool();
         
            //TP = await _talentPoolService.GetTalentPoolDetails(model);
            //if (model.Education != null)
            //{
            //    var Edu = from li in model.Education
            //              where li.APPLICABILITY == true
            //              select li;
            //    TP.Education = Edu.ToList();
            //}
            //if (model.Languages != null)
            //{
            //    var Lang = from li in model.Languages
            //               where li.Read == true || li.Speak == true || li.Write
            //              select li;
            //    TP.Languages = Lang.ToList();
            //}
            //if (model.SkillProficiency != null)
            //{
            //    var SkillProf = from li in model.SkillProficiency
            //               where li.APPLICABILITY == true
            //               select li;
            //    TP.SkillProficiency = SkillProf.ToList();
            //}
            //if (model.Skills != null)
            //{
            //    var Skills = from li in model.Skills
            //                 where li.APPLICABILITY == true
            //                    select li;
            //    TP.Skills = Skills.ToList();
            //}
            //TP.Gender = model.Gender;
            //TP.AgeFrom = model.AgeFrom;
            //TP.AgeTo = model.AgeTo;
            //TP.YOSFrom = model.YOSFrom;
            //TP.YOSTo = model.YOSTo;
            //TP.YSAboveOrBelow = new Operators();
            //TP.AgeAboveOrBelow = new Operators();
            //TP.AgeAboveOrBelow = model.AgeAboveOrBelow;
            //TP.YSAboveOrBelow = model.YSAboveOrBelow;
            //TP.AllSkills = model.AllSkills;
            //TP.AllSkillProficiency = model.AllSkillProficiency;
            //TP.AllEducation = model.AllEducation;
            return View("_TalentPoolDetails", TP);
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

        public async Task<IActionResult> ApplyFilter(Filter filter)
        {
            try { 
            Global.GlobalFilter = filter;
            TalentPool model = new TalentPool();
            TalentPool TP = new TalentPool();
            model = Global.GlobalTalentPool == null ? new TalentPool() : Global.GlobalTalentPool;
            model.Filter = new Filter();
            model.Filter = filter;
            TP = await _talentPoolService.GetTalentPoolDetails(model);
            if (model.Education != null)
            {
                var Edu = from li in model.Education
                          where li.APPLICABILITY == true
                          select li;
                TP.Education = Edu.ToList();
            }
            if (model.Languages != null)
            {
                var Lang = from li in model.Languages
                           where li.Read == true || li.Speak == true || li.Write
                           select li;
                TP.Languages = Lang.ToList();
            }
            if (model.SkillProficiency != null)
            {
                var SkillProf = from li in model.SkillProficiency
                                where li.APPLICABILITY == true
                                select li;
                TP.SkillProficiency = SkillProf.ToList();
            }
            if (model.Skills != null)
            {
                var Skills = from li in model.Skills
                             where li.APPLICABILITY == true
                             select li;
                TP.Skills = Skills.ToList();
            }
            TP.Gender = model.Gender;
            TP.AgeFrom = model.AgeFrom;
            TP.AgeTo = model.AgeTo;
            TP.YOSFrom = model.YOSFrom;
            TP.YOSTo = model.YOSTo;
                TP.YSAboveOrBelow = new Operators();
                TP.AgeAboveOrBelow = new Operators();
                TP.AgeAboveOrBelow = model.AgeAboveOrBelow;
                TP.YSAboveOrBelow = model.YSAboveOrBelow;
                TP.AllSkills = model.AllSkills;
                TP.AllSkillProficiency = model.AllSkillProficiency;
                TP.AllEducation = model.AllEducation;
                return PartialView("_TalentPoolDetails", TP);
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

      
        public async Task<IActionResult> ExportToExcel(int totRecords)
        {
            try
            {
                TalentPool model = new TalentPool();

                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = totRecords;
                    Global.GlobalFilter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Talent Pool", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                
                model.Filter = Global.GlobalFilter;
                      model=Global.GlobalTalentPool;

                //Get Datset
                var dataset = await _talentPoolService.GetTalentPoolDetails_Excel(model);
                DataTable dt = dataset.Tables[0];


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
                    var worksheet = workbook.Worksheets.Add("Talent Pool");
                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:E" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:E" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:E" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:E" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:E" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 4)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Talent Pool";

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
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 2).Value = "Total Count";
                    worksheet.Cell(4, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(4, 3).Value = dt.Rows.Count;
                    worksheet.Cell(5, 1).Style.Font.Bold = true;
                    worksheet.Cell(5, 1).Value = "Filter Applied";
                    worksheet.Range("B5:E5").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 6;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A6:E6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A6:E6").Style.Font.Bold = true;


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
                            "Talent Pool List.xlsx"
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

        //public async Task<IActionResult> ExportToExcel(int totRecords)
        //{
        //    try
        //    {
        //        TalentPool model = new TalentPool();
        //        if (Global.GlobalFilter == null)
        //            Global.GlobalFilter = new Filter();
        //        else
        //        {
        //            Global.GlobalFilter.length = totRecords;
        //            Global.GlobalFilter.start = 0;
        //        }
        //        model.Filter = Global.GlobalFilter;
        //        model = Global.GlobalTalentPool;
        //        string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);

        //        model = await _talentPoolService.GetTalentPoolDetails(model);


        //        var LoginUser = HttpContext.Session.GetString("LoginUser");
        //        var CompanyName = HttpContext.Session.GetString("CompanyName");
        //        ExcelPassword EP = new ExcelPassword();
        //        EP = await _commonService.GetExcelPassword("Talent Pool", "Excel");
        //        int slNo = 0;
        //        if (FilterValues == "")
        //        {
        //            FilterValues = "All";
        //        }
        //        string currDateTime = await _helpers.GetCurrentDate();
        //        
        //        using (var workbook = new XLWorkbook())
        //        {
        //            var worksheet = workbook.Worksheets.Add("Talent Pool");
        //            //int count = model.JoiningReportList.Count + 5;
        //            int count = model.TalentPoolList.Count + 6;


        //            worksheet.Range("A1:E" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1:E" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1:E" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1:E" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1:E" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
        //            var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 4)));
        //            wsReportNameHeaderRange.Style.Font.Bold = true;
        //            wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //            wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //            wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //            wsReportNameHeaderRange.Merge();
        //            wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
        //            wsReportNameHeaderRange.Value = "Talent Pool";

        //            worksheet.Cell(2, 1).Style.Font.Bold = true;
        //            worksheet.Cell(2, 1).Value = "Company Name";
        //            worksheet.Cell(2, 2).Value = CompanyName.ToUpper();


        //            worksheet.Cell(3, 1).Style.Font.Bold = true;
        //            worksheet.Cell(3, 1).Value = "Login User";
        //            worksheet.Cell(3, 2).Value = LoginUser;
        //            worksheet.Cell(3, 3).Style.Font.Bold = true;
        //            worksheet.Cell(3, 3).Value = "Date & Time";
        //            worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
        //            worksheet.Cell(3, 4).Value = currDateTime.ToString();
        //            worksheet.Cell(4, 1).Style.Font.Bold = true;
        //            worksheet.Cell(4, 1).Value = "Total Count";
        //            worksheet.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(4, 2).Value = model.TalentPoolList.Count;
        //            worksheet.Cell(5, 1).Style.Font.Bold = true;
        //            worksheet.Cell(5, 1).Value = "Filter Applied";
        //            worksheet.Range("B5:E5").Merge();
        //            worksheet.Cell("B5").Value = FilterValues;



        //            var currentRow = 6;
        //            #region Header
        //            worksheet.Range("A6:E6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
        //            worksheet.Cell(currentRow, 1).Value = "Sl.No";
        //            worksheet.Cell(currentRow, 1).Style.Font.Bold = true;

        //            worksheet.Cell(currentRow, 2).Value = "ID";
        //            worksheet.Cell(currentRow, 2).Style.Font.Bold = true;

        //            worksheet.Cell(currentRow, 3).Value = "Employee Name";
        //            worksheet.Cell(currentRow, 3).Style.Font.Bold = true;

        //            worksheet.Cell(currentRow, 4).Value = "Designation";
        //            worksheet.Cell(currentRow, 4).Style.Font.Bold = true;

        //            worksheet.Cell(currentRow, 5).Value = "Location";
        //            worksheet.Cell(currentRow, 5).Style.Font.Bold = true;


        //            #endregion

        //            #region Body
        //            foreach (var item in model.TalentPoolList)
        //            {
        //                currentRow++;
        //                slNo = slNo + 1;
        //                worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 1).Value = slNo;

        //                worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 2).Value = item.EmployeeID;

        //                worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 3).Value = item.FullName;

        //                worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 4).Value = item.Designation;

        //                worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 5).Value = item.Location;



        //                worksheet.Columns(1, 5).Style.Alignment.WrapText = true;
        //                worksheet.Rows().Style.Alignment.WrapText = true;
        //            }
        //            #endregion
        //            worksheet.Columns().AdjustToContents();
        //            worksheet.Rows().AdjustToContents();
        //            using (var stream = new MemoryStream())
        //            {
        //                workbook.SaveAs(stream);
        //                if (EP.NeedPassword)
        //                {
        //                    Workbook book = new Workbook();
        //                    book.LoadFromStream(stream);
        //                    //Protect Workbook
        //                    book.Protect(EP.Password,false,false);
        //                    book.SaveToStream(stream);
        //                }
        //                var content = stream.ToArray();
        //                return File(
        //                    content,
        //                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                    "Talent Pool List.xlsx"
        //                    );
        //            }
        //        }
        //    }
        //    catch (UnauthorizedAccessException e)
        //    {

        //        return RedirectToAction("Login", "Account", new { Area = "" });
        //    }
        //    catch (Exception e)
        //    {
        //        return RedirectToAction("Error", "Account", new { Area = "" });
        //    }

        //}

        public async Task<IActionResult> GetEmployeeTalentList(string TalentType, int Code)
        {
            try
            {
                TalentPool model = new TalentPool();

                model = await _talentPoolService.GetEmployeeTalentList(TalentType, Code);

                return PartialView("_EmployeeListPopup", model);
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
