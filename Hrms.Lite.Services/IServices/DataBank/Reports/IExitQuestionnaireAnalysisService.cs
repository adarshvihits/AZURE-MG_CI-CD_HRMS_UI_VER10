using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank.Reports
{
    public interface IExitQuestionnaireAnalysisService
    {
        public Task<ExitQuestionnaireAnalysis> ExitQuestionnaireAnalysisReport();
        public Task<ExitQuestionnaireAnalysis> GetQuestionnaireDetails(int QuestionCode, int AnswerCode,Filter filter);
        public Task<DataSet> GetQuestionnaireEmployee_Excel( int QuestionCode, int AnswerCode, Filter filterdata);
    }
}
