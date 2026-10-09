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
    public class InterviewFeedbackSettingsController : Controller
    {
        private readonly IInterviewFeedbackSettingsService _service;
        private readonly IDropdownService _dropdownService;

        public InterviewFeedbackSettingsController(IInterviewFeedbackSettingsService service, IDropdownService dropdownService)
        {
            _service = service;
            _dropdownService = dropdownService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                InterviewFeedbackSettings model = await GetFilteredList(1, 0);
                model.GradeList = await _dropdownService.GetGradeDropdown() ?? new List<Dropdown>();
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

        public async Task<IActionResult> GetInterviewFeedbackSettingsList(int RoundType, int GradeCode)
        {
            try
            {
                return PartialView("_InterviewFeedbackSettingsList", await GetFilteredList(RoundType, GradeCode));
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
                InterviewFeedbackSettings model = new InterviewFeedbackSettings();
                model.Mode = "CREATE";
                model.RoundType = 1;
                model.SelectedGrades = new List<int>();
                model.QuestionList = new List<InterviewFeedbackQuestion> { new InterviewFeedbackQuestion { Active = true } };
                model.GradeList = await _dropdownService.GetGradeDropdown() ?? new List<Dropdown>();
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

        public async Task<IActionResult> Edit(Guid FeedbackSetGI)
        {
            try
            {
                InterviewFeedbackSettings model = await _service.GetInterviewFeedbackSettingsDetails(FeedbackSetGI) ?? new InterviewFeedbackSettings();
                model.Mode = "MODIFY";
                model.SelectedGrades = model.SelectedGrades ?? new List<int>();
                if (model.QuestionList == null || model.QuestionList.Count == 0)
                    model.QuestionList = new List<InterviewFeedbackQuestion> { new InterviewFeedbackQuestion { Active = true } };
                model.GradeList = await _dropdownService.GetGradeDropdown() ?? new List<Dropdown>();
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
        public async Task<IActionResult> InterviewFeedbackSettingsSave(InterviewFeedbackSettings model)
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

        private async Task<InterviewFeedbackSettings> GetFilteredList(int RoundType, int GradeCode)
        {
            InterviewFeedbackSettings model = await _service.GetInterviewFeedbackSettingsList() ?? new InterviewFeedbackSettings();
            var all = model.FeedbackSetList ?? new List<InterviewFeedbackSettings>();
            var forGrade = all.Where(x => GradeCode == 0 || (x.SelectedGrades != null && x.SelectedGrades.Contains(GradeCode))).ToList();

            // grade names for the list tags come from the grade master
            var grades = await _dropdownService.GetGradeDropdown() ?? new List<Dropdown>();
            foreach (var set in forGrade)
            {
                set.GradeNameList = (set.SelectedGrades ?? new List<int>())
                    .Select(code => grades.FirstOrDefault(g => g.Code == code)?.Name)
                    .Where(name => !string.IsNullOrEmpty(name))
                    .ToList();
            }

            model.RoundType = RoundType;
            model.GradeCode = GradeCode;
            model.FirstRoundCount = forGrade.Count(x => x.RoundType == 1);
            model.SecondRoundCount = forGrade.Count(x => x.RoundType == 2);
            model.FeedbackSetList = forGrade.Where(x => x.RoundType == RoundType).ToList();
            return model;
        }
    }
}
