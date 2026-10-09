using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Recruitment;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Recruitment;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Recruitment.Controllers
{
    [Area("Recruitment")]
    [SessionAuthorize]
    public class MasterInterviewPanelController : Controller
    {
        private readonly IMasterInterviewPanelService _service;
        private readonly IDropdownService _dropdownService;

        public MasterInterviewPanelController(IMasterInterviewPanelService service, IDropdownService dropdownService)
        {
            _service = service;
            _dropdownService = dropdownService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                MasterInterviewPanel model = await GetFilteredList(0, 0, 0, 0);
                return View(model);
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }

        public async Task<IActionResult> GetMasterInterviewPanelList(int LocationCode, int DepartmentCode, int SectionCode, int GradeCode)
        {
            try
            {
                return PartialView("_MasterInterviewPanelList", await GetFilteredList(LocationCode, DepartmentCode, SectionCode, GradeCode));
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                MasterInterviewPanel model = new MasterInterviewPanel();
                model.Mode = "CREATE";
                model.Active = true;
                model.MemberList = new List<MasterInterviewPanelMember> { new MasterInterviewPanelMember { Role = "Chairperson" } };
                await FillDropdowns(model);
                return PartialView("_CreateOrEdit", model);
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }

        public async Task<IActionResult> Edit(Guid PanelGI)
        {
            try
            {
                MasterInterviewPanel model = await _service.GetMasterInterviewPanelDetails(PanelGI) ?? new MasterInterviewPanel();
                model.Mode = "MODIFY";
                if (model.MemberList == null || model.MemberList.Count == 0)
                    model.MemberList = new List<MasterInterviewPanelMember> { new MasterInterviewPanelMember { Role = "Chairperson" } };
                await FillDropdowns(model);
                return PartialView("_CreateOrEdit", model);
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> MasterInterviewPanelSave(MasterInterviewPanel model)
        {
            try
            {
                var response = await _service.Create(model);
                return Json(response);
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }

        private async Task FillDropdowns(MasterInterviewPanel model)
        {
            model.LocationList = await _dropdownService.GetLocationDropdown() ?? new List<Dropdown>();
            model.DepartmentList = await _dropdownService.GetDepartmentDropdown() ?? new List<Dropdown>();
            model.SectionList = await _dropdownService.GetSectionDropdown() ?? new List<Dropdown>();
            model.GradeList = await _dropdownService.GetGradeDropdown() ?? new List<Dropdown>();
        }

        private async Task<MasterInterviewPanel> GetFilteredList(int LocationCode, int DepartmentCode, int SectionCode, int GradeCode)
        {
            MasterInterviewPanel model = await _service.GetMasterInterviewPanelList() ?? new MasterInterviewPanel();
            await FillDropdowns(model);

            model.PanelList = (model.PanelList ?? new List<MasterInterviewPanel>())
                .Where(x => (LocationCode == 0 || x.LocationCode == LocationCode)
                         && (DepartmentCode == 0 || x.DepartmentCode == DepartmentCode)
                         && (SectionCode == 0 || x.SectionCode == SectionCode)
                         && (GradeCode == 0 || x.GradeCode == GradeCode))
                .ToList();

            // branch / department / section / grade names come from the masters
            foreach (var panel in model.PanelList)
            {
                panel.LocationName = DropdownName(model.LocationList, panel.LocationCode);
                panel.DepartmentName = DropdownName(model.DepartmentList, panel.DepartmentCode);
                panel.SectionName = DropdownName(model.SectionList, panel.SectionCode);
                panel.GradeName = DropdownName(model.GradeList, panel.GradeCode);
            }
            return model;
        }

        private static string DropdownName(List<Dropdown> list, int code)
        {
            return list?.FirstOrDefault(x => x.Code == code)?.Name ?? "";
        }
    }
}
