using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Account;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.Services.Account;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _AccountService;
        private readonly IPasswordSettingsService _PasswordSettingsService;
        private readonly IMenuService _menuService;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly ICipherService _cipherService;
        private IHostingEnvironment Environment;
        private IBlob _blob;
        private readonly ILogger<AccountController> _logger;
        public static class Global
        {
            public static string[] mNames;
            public static int[] mCodes;
        }
        public AccountController(IAccountService AccountService, IMenuService menuService, IPasswordSettingsService PasswordSettingsService,
            ICommonService commonService,IDropdownService dropdownService, IHostingEnvironment _environment, ICipherService cipherService, IBlob blob, ILogger<AccountController> logger)
        {
            _AccountService = AccountService;
            _menuService = menuService;
            _PasswordSettingsService = PasswordSettingsService;
            _commonService = commonService;
            _dropdownService = dropdownService;
            Environment = _environment;
            _cipherService = cipherService;
            _blob = blob;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
         public IActionResult LoginV1()
        {
            return View();
        }  
        public async Task<IActionResult> Login()
        {
            var LoginStatus = HttpContext.Session.GetString("LoginStatus");
            if (LoginStatus == "Out")
                return RedirectToAction("AccessDenied", "Account", new { Area = "" });

            var InvalidEntry = HttpContext.Session.GetString("InvalidEntry");
            if (InvalidEntry == "1")
                return View("~/Views/Account/InvalidEntry.cshtml");

            CustomSoftware model = await _AccountService.GetSoftwareLogo();
            if (model.SoftwareLogo.fdata != null)
            {
                var imreBase64Data = Convert.ToBase64String(model.SoftwareLogo.fdata);
                //string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                var imgDataURL = String.Format("data:image/svg+xml;base64,{0}", imreBase64Data);

                ViewBag.SoftwareLogo = imgDataURL;
            }

            if (model.HRMS_SoftwareName.fdata != null)
            {
                var imreBase64Data = Convert.ToBase64String(model.HRMS_SoftwareName.fdata);
                //string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                var imgDataURL = String.Format("data:image/svg+xml;base64,{0}", imreBase64Data);

                ViewBag.HRMS_SoftwareName = imgDataURL;
            }
            ViewBag.GrpCompany = new SelectList("", "Code", "Name", 0);
            int width = 100;
            int height = 50;
            int fontSize = 20;
            int captchaLength = 2;
            CaptchaGenerator captchaGenerator = new CaptchaGenerator();
            (int result, string base64Captcha) = captchaGenerator.GenerateCaptcha(width, height, fontSize, captchaLength);
             var encData = _cipherService.Encrypt(Convert.ToString(result));
            //HttpContext.Session.SetString("Captcha", Convert.ToString(result));            
            ViewBag.CaptchaImage = "data:image/png;base64," + base64Captcha;
            ViewBag.CaptchaAnswer = encData;
            return View("~/Views/Account/Login.cshtml");
        }


        public async Task<IActionResult> LoginControls(Login model)
        {
            return PartialView("_LoginControls", model);        
        }
        public async Task<IActionResult> RefreshCaptcha()
        {
            int width = 100;
            int height = 50;
            int fontSize = 20;
            int captchaLength = 2;
            CaptchaGenerator captchaGenerator = new CaptchaGenerator();
            (int result, string base64Captcha) = captchaGenerator.GenerateCaptcha(width, height, fontSize, captchaLength);
            var encData = _cipherService.Encrypt(Convert.ToString(result));
            //HttpContext.Session.SetString("Captcha", Convert.ToString(result));
            ViewBag.CaptchaImage = "data:image/png;base64," + base64Captcha;
            ViewBag.CaptchaAnswer = encData;
            return PartialView("_Captcha");        
        }

        //[HttpPost]
        //[ValidateDNTCaptcha(
        //    ErrorMessage = "Please Enter Valid Captcha",
        //    CaptchaGeneratorLanguage = Language.English,
        //    CaptchaGeneratorDisplayMode = DisplayMode.SumOfTwoNumbers)]
        //public async Task<IActionResult> Proceed(Login Credentials)
        // {
        //    if (ModelState.IsValid)
        //    {
        //        Credentials.software = "HRMS";
        //        var response = await _AccountService.Authenticate(Credentials);

        //        return Json(response);


        //    }
        //    else
        //    {
        //        var errors = ModelState.Select(x => x.Value.Errors)
        //                  .Where(y => y.Count > 0)
        //                  .ToList();
        //        ResponseEntity response = new ResponseEntity("", false);
        //        response.Success = false;
        //        response.Message = errors[0][0].ErrorMessage;
        //        return Json(response);
        //    }
        //}
        [HttpPost]      
        public async Task<IActionResult> Proceed(Login Credentials,string Data)
        {
            //int Captcha = Convert.ToInt32(HttpContext.Session.GetString("Captcha"));
            int Captcha = Convert.ToInt32(_cipherService.Decrypt(Data));
            if (Captcha == Credentials.Captcha)
            {
                Credentials.software = "HRMS";
                var response = await _AccountService.Authenticate(Credentials);
                if(response.Success == false)
                    return Json(response);
                Login log = JsonConvert.DeserializeObject<Login>(response.Data);
                if (log.ProductGroup == "Malabar Gold & Diamonds")
                {
                    Login m = new Login();
                    m.MACAddress = getMacAddress();
                    m.software = "HRMS";
                    m.INOUT = "IN";
                    m.CompanyCode = Credentials.CompanyCode;
                    m.UserCode = Credentials.UserCode;
                    m.LoginSuccess = response.Success;
                    m.IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                    await _AccountService.LogHistory(m);
                }
                else
                {
                    response.Message = "Sorry.You Don't have permission to access this Database. Please check and verify the Product Group.";
                    response.Success = false;
                }
                //if (log.LoginStatus == "IN")
                //{
                //    ResponseEntity res = new ResponseEntity("", false);
                //    res.Success = false;
                //    res.Message = "Already signed in another device.";

                //    return Json(res);
                //}
                //else
                //{
                return Json(response);
                //}


            }
            else
            {                
                ResponseEntity response = new ResponseEntity("", false);
                response.Success = false;
                response.Message = "Please Enter Valid Captcha";
                return Json(response);
            }
        }
        public async Task<string> ViewAsBackGroundImage(string filename)
        {
            try
            {
                Shared.File model = new Shared.File();
                model.FileName = filename;
                model = await _blob.GetBlobFile(model);
                if (model != null)
                {
                    model.fdata = model.stream.ToArray();
                    string base64String = Convert.ToBase64String(model.fdata, 0, model.fdata.Length);
                    return base64String;
                }
                else
                    return null;
            }
            catch (UnauthorizedAccessException e)
            {

                return e.Message.ToString();
            }
            catch (Exception e)
            {

                return e.Message.ToString();
            }
        }
        [HttpPost]
        //[ValidateDNTCaptcha(
        //    ErrorMessage = "Please Enter Valid Captcha",
        //    CaptchaGeneratorLanguage = Language.English,
        //    CaptchaGeneratorDisplayMode = DisplayMode.SumOfTwoNumbers)]
        public async Task<IActionResult> Login( Login Credentials )
        { 
            //if (ModelState.IsValid)
            //{
            Credentials.software = "HRMS";
            var response = await _AccountService.Login(Credentials);

            if (response.Success == false)
            {
                TempData["Message"] = response.Message;

                return RedirectToAction("Login", "Account");
            }


            Login login = JsonConvert.DeserializeObject<Login>(response.Data);
   
            HttpContext.Session.SetString("JwtToken", login.Token);
            login.MenuEntity = new MenuEntity();
            MenuEntity ModuleList = await _menuService.GetModuleList("HRMS");
            var ModuleCode = 0;
            for (int i = 0; i < ModuleList.Module.Count; i++)
            {
                if (ModuleList.Module[i].Route == login.authUser.DashBoardRoute)
                {
                    ModuleCode = ModuleList.Module[i].ModuleCode;
                    break;
                }
            }
            HttpContext.Session.SetString("LoginUser", login.UserCode);
            MenuEntity menu = await _menuService.GetModuleVsMenuList(ModuleCode);
            login.MenuEntity = menu;
            login.MenuEntity.Module = ModuleList.Module;
            //Set Avatar Letter
            var Avatarletter = login.authUser.EmployeeName.Substring(0, 1);
            login.authUser.AvatarLetter = Avatarletter;
            var storage = await _commonService.GetFileStorageType();
         //     GET Custom APPURL
            login.SoftwareURLs = await _commonService.GetSoftwareURLs();

            login.GroupCompanyDrpdown = await _AccountService.GetGroupCompanyDropDown(Credentials);
            login.GrpCompanyCode = login.GroupCompanyCode;
            login.isRedirected = Credentials.isRedirected;
            HttpContext.Session.SetString("User", JsonConvert.SerializeObject(login));
            HttpContext.Session.SetString("CompanyName", login.authUser.CompanyName);
            HttpContext.Session.SetString("UserRoleActions", JsonConvert.SerializeObject(await _commonService.UserRole_Actions()));

 
           
           
                if (login.authUser.DashBoardRoute == "DataBankDashBoard")
                {
                    if(Credentials.GrpChange == "YES")
                        return Json(new { redirectUrl = Url.Action("Index", "LandingPage", new { Area = "DashBoard" }) });
                    else
                    return RedirectToAction("Index", "LandingPage", new { Area = "DashBoard" });
                }
                else if (login.authUser.DashBoardRoute == "Home")
                {
                    if (Credentials.GrpChange == "YES")
                        return Json(new { redirectUrl = Url.Action("Index", login.authUser.DashBoardRoute, new { Area = "" }) });
                    return RedirectToAction("Index", login.authUser.DashBoardRoute, new { Area = "" });

                }
                else
                {
                    if (login.authUser.DashBoard == "D2")
                    {
                        if (Credentials.GrpChange == "YES")
                            return Json(new { redirectUrl = Url.Action("IndexV3", login.authUser.DashBoardRoute, new { Area = "DashBoard" }) });
                        else
                            return RedirectToAction("IndexV3", login.authUser.DashBoardRoute, new { Area = "DashBoard" });
                    }
                    else
                    {
                        if (Credentials.GrpChange == "YES")
                            return Json(new { redirectUrl = Url.Action("Index", login.authUser.DashBoardRoute, new { Area = "DashBoard" }) });else
                        return RedirectToAction("Index", login.authUser.DashBoardRoute, new { Area = "DashBoard" });
                    }
                }

            
        }
        public async Task<IActionResult> GetmenusDrop()
        {
            var msd = await _dropdownService.GetMenuSearchDropdown();
            string[] menuNames = new string[msd.Count];
            int[] menuCodes = new int[msd.Count];
            for (int i = 0; i < msd.Count; i++)
            {
                menuNames[i] = msd[i].Name;
                menuCodes[i] = msd[i].Code;
            }
            HttpContext.Session.SetString("mNames", JsonConvert.SerializeObject(menuNames));
            HttpContext.Session.SetString("mCodes", JsonConvert.SerializeObject(menuCodes));
            //Global.mNames = menuNames;
            //Global.mCodes = menuCodes;
            return Json(menuNames);
        }
        public async Task<IActionResult> GetEmployeesDropdown()
        {
            var emp = await _dropdownService.GetEmployeeDropDown();
            string[] empNames = new string[emp.Count];
            int[] empCodes = new int[emp.Count];
            for (int i = 0; i < emp.Count; i++)
            {
                empNames[i] = emp[i].Name;
                empCodes[i] = emp[i].Code;
            }
            HttpContext.Session.SetString("eNames", JsonConvert.SerializeObject(empNames));
            HttpContext.Session.SetString("eCodes", JsonConvert.SerializeObject(empCodes));
            return Json(empNames);
        }
        public static string GetMimeTypeFromFilename(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return null;
            }

            var extension = Path.GetExtension(filename).TrimStart('.').ToLowerInvariant();
            var mimeTypeMap = new Dictionary<string, string>()
          {
        { "jpg", "data:image/jpeg;base64," },
        { "jpeg", "data:image/jpeg;base64," },
        { "png", "data:image/png;base64," },
        // Add more mappings for other supported extensions
          };

            return mimeTypeMap.ContainsKey(extension) ? mimeTypeMap[extension] : null;
        }

        public async Task<IActionResult> RedirectToMenu(string MenuName)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (token != null)
            {
                var currentMenuCode = 0;
                MenuItem menuItem = new MenuItem();
                if (MenuName != null)
                {
                    var mNames = JsonConvert.DeserializeObject<string[]>(HttpContext.Session.GetString("mNames"));
                    var mCodes = JsonConvert.DeserializeObject<int[]>(HttpContext.Session.GetString("mCodes"));
                    for (int i = 0; i < mNames.Length; i++)
                    {
                        if (mNames[i].ToLower() == MenuName.ToLower())
                        {
                            currentMenuCode = mCodes[i];
                            menuItem = await _menuService.GetMenuDetails(currentMenuCode);
                            MenuEntity ModuleList = await _menuService.GetModuleList("HRMS");
                            MenuEntity menu = await _menuService.GetModuleVsMenuList(menuItem.ModuleCode);
                            var LoginUser = HttpContext.Session.GetString("User");
                            Login login = JsonConvert.DeserializeObject<Login>(LoginUser);
                            login.MenuEntity = menu;
                            login.MenuEntity.Module = ModuleList.Module;
                            HttpContext.Session.SetString("User", JsonConvert.SerializeObject(login));
                            break;
                        }

                    }
                }

                return Json(menuItem);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public async Task<IActionResult> RedirectToEmployeeProfile(string EmployeeName)
        {
            string token = HttpContext.Session.GetString("JwtToken");
            if (token != null)
            {
                var currentEmployeeCode = 0;
                EmployeeHeader model = new EmployeeHeader();
                if (EmployeeName != null)
                {
                    var eNames = JsonConvert.DeserializeObject<string[]>(HttpContext.Session.GetString("eNames"));
                    var eCodes = JsonConvert.DeserializeObject<int[]>(HttpContext.Session.GetString("eCodes"));
                    for (int i = 0; i < eNames.Length; i++)
                    {
                        if (eNames[i].ToLower() == EmployeeName.ToLower())
                        {
                            currentEmployeeCode = eCodes[i];
                            
                            model = await _commonService.GetEmployeeCard(currentEmployeeCode);                            
                            break;
                        }

                    }
                }

                return RedirectToAction("GetEmployeeProfileDetails", "EmployeeProfile", new { Area = "DataBank", EmployeeGI = model.EmployeeGuid, Breadcrumb = "EmployeeProfile", BreadcrumbName = "Employee Profile List" });
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public async Task<IActionResult> RedirectToESSP()
        {
            var Urls = await _commonService.GetSoftwareURLs();
            return Redirect(Urls.ESSPLandingPageURL);

        }
        public async Task<IActionResult> Logout()
        {     
            SoftwareURLs url = await _commonService.GetSoftwareURLs();
            string token = HttpContext.Session.GetString("JwtToken");
            if (token != null)
            {
                Login model = new Login();
                model.MACAddress = getMacAddress();
                model.software = "HRMS";
                model.INOUT = "OUT";                
                model.IPAddress =HttpContext.Connection.RemoteIpAddress?.ToString();
                await _AccountService.LogHistory(model);
            }
            
            string json = HttpContext.Session.GetString("User");
            int isRedirected = 0;
            if (!string.IsNullOrEmpty(json))
            {
                var crede = JsonConvert.DeserializeObject<Login>(json);
                isRedirected = crede.isRedirected;
            }
                HttpContext.Session.Clear();
                foreach (var cookie in Request.Cookies.Keys)
                {               
                    if (cookie != "Career_Portal_Cookie")
                    {   HttpContext.Session.Remove(cookie);
                        HttpContext.Response.Cookies.Delete(cookie);
                     }               

                }
            if (isRedirected ==0)
                return RedirectToAction("Login");
            else //FRom ESSP
                return Redirect(url.ESSPAppURL);
        }
        public async Task<IActionResult> Error()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            var exception = exceptionFeature?.Error;

            if (exception != null)
            {
                _logger.LogError(exception, "An unhandled exception occurred.");
            }

            var logPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Logs",
                $"ErrorLog_{DateTime.Now:yyyyMMdd}.txt"
            );

            Directory.CreateDirectory(Path.GetDirectoryName(logPath));

            var logMessage = $@"
            -------------------------------
            Date       : {DateTime.Now}
            Path       : {HttpContext.Request.Path}
            Method     : {HttpContext.Request.Method}
            Message    : {exception?.Message}
            StackTrace : {exception?.StackTrace}
            -------------------------------";

            await System.IO.File.AppendAllTextAsync(logPath, logMessage);
            return View();
        }
        public string getMacAddress()
        {
            string hex="";
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet && nic.OperationalStatus == OperationalStatus.Up)
                {
                   hex = BitConverter.ToString(nic.GetPhysicalAddress().GetAddressBytes());
                }
                else if (nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 && nic.OperationalStatus == OperationalStatus.Up)
                {
                    hex = BitConverter.ToString(nic.GetPhysicalAddress().GetAddressBytes());
                }
            }
            return hex;
        }
        public async Task<IActionResult> ForgotPassword()
        {
            ForgetPassword model = new ForgetPassword();
            return View("~/Views/Account/ForgotPassword.cshtml",model);
        } 
        public async Task<IActionResult> ChangePassword()
        {
            ChangePassword model = new ChangePassword();
            model.PasswordPolicySettings = await _PasswordSettingsService.GetPasswordPolicySettings();
            return View("~/Views/Account/ChangePassword.cshtml",model);
        }
        public async Task<IActionResult> VerifyOTP()
        {
            ForgetPassword model = new ForgetPassword();
            return PartialView("_VerifyOTP", model);
        }
        public async Task<IActionResult> NewPWD(string CompanyID)
        {
            ChangePassword model = new ChangePassword();
            model.PasswordPolicySettings = await _PasswordSettingsService.GetForgetPasswordPasswordPolicySettings(CompanyID);
            return PartialView("_NewPassword", model);
        }
        public async Task<IActionResult> ForgetGetPasswordPolicy(string CompanyID)
        {
            ForgetPassword model = new ForgetPassword();
            model.PasswordPolicySettings = await _PasswordSettingsService.GetForgetPasswordPasswordPolicySettings(CompanyID);
            return PartialView("_NewPassword", model);
        }
        [HttpPost]
        public async Task<IActionResult> GenerateOTP(ForgetPassword model)
        {
           var response = await _PasswordSettingsService.ForgetPasswordGenerateOTP(model);
           
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> VerifyOTP(ForgetPassword model)
        {
            var response = await _PasswordSettingsService.ForgetPasswordVerifyOTP(model);

            return Json(response);
        }

        public async Task<IActionResult> ChangePasswordGenerateOTP(string CurrPassword)
        {
            var response = await _PasswordSettingsService.ChangePasswordGenerateOTP(CurrPassword);

            return Json(response);
        }

        public async Task<IActionResult> ChangePasswordVerifyOTP(string Otp)
        {
            var response = await _PasswordSettingsService.ChangePasswordVerifyOTP(Otp);

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> SavePassword(ChangePassword model)
        {
            var response = await _PasswordSettingsService.ChangePasswordSave(model);

            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPasswordSave(ForgetPassword model)
        {
            var response = await _PasswordSettingsService.ForgetPasswordSave(model);

            return Json(response);
        }

        public async Task<IActionResult> GetLogOutUserRoleDetails(int UserRoleCode)
        {
            UserRoleDetails model = new UserRoleDetails();
            model = await _AccountService.GetLogOutUserRoleDetails(UserRoleCode);
            return PartialView("_UserRoleDetails", model);

        }
        [HttpGet]
        public async Task<JsonResult> GetGroupCompanyDropdown(string CompanyCode,string UserCode)
        {
            Login model = new Login();
            model.CompanyCode = CompanyCode;
            model.UserCode = UserCode;
            var Grpcmp = new SelectList(await _AccountService.GetGroupCompanyDropDown(model), "Code", "Name");
            return Json(Grpcmp);
        }
    }
   
}
