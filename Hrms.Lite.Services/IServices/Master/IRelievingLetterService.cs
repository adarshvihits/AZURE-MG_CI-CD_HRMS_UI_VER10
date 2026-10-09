using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface IRelievingLetterService
    {
        public Task<Url> GetUrl();
        public Task<Url> GetOfferLetter(string Type);


    }
}
