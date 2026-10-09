using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class ExitQuestionnaireAnalysisService : IExitQuestionnaireAnalysisService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public ExitQuestionnaireAnalysisService(IServiceBase httpService)
        {
            _baseURL = "ExitQuestionnaireAnalysis";
            _httpService = httpService;
        }
        public async Task<ExitQuestionnaireAnalysis> ExitQuestionnaireAnalysisReport()
        {
            return await _httpService.GetAsync<ExitQuestionnaireAnalysis>($"{_baseURL}/ExitQuestionnaireAnalysisReport");
        }
        public async Task<ExitQuestionnaireAnalysis> GetQuestionnaireDetails(int QuestionCode, int AnswerCode,Filter filter)
        {
            return await _httpService.PostAsync<ExitQuestionnaireAnalysis,Filter>($"{_baseURL}/GetQuestionnaireDetails?QuestionCode={QuestionCode}&AnswerCode={AnswerCode}",filter);
        }

        public async Task<DataSet> GetQuestionnaireEmployee_Excel(int QuestionCode, int AnswerCode, Filter filterdata)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetQuestionnaireEmployee_Excel?QuestionCode={QuestionCode}&AnswerCode={AnswerCode}", filterdata);
        }
    }
}
