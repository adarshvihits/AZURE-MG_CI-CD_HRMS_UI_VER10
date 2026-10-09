using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Home
{
    public interface IHomeService
    {
        public Task<ResponseEntity> SaveTheme(Theme model);
        public Task<Theme> GetTheme();
        public Task<Theme> GetDarkModeStatus();
        public Task<ResponseEntity> ToggleDarkMode(int IsDarkMode);
    }
}
