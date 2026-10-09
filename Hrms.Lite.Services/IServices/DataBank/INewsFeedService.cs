using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface INewsFeedService
    {
        public Task<NewsFeed> GetList();
        Task<NewsFeed> GetNewsFeedCombinationDetails(int id);
        public Task<ResponseEntity> NewsFeedSave(NewsFeed model);
        public Task<ResponseEntity> NewsFeedUpdate(NewsFeed model);
        public Task<NewsFeed> GetNewsFeedDetails(Guid NewsFeedGI);
        public Task<ResponseEntity> Delete(NewsFeed model);
    }
}
