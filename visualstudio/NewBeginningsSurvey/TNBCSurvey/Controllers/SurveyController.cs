using TNBCSurvey.DAL;
using TNBCSurvey.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using Microsoft.Office.Interop.Excel;

namespace TNBCSurvey.Controllers
{
  
    public class SurveyController : ApiController
    {
        readonly SurveyAnswerRepository _repoA;
        readonly SurveyTicketRepository _repoT;
        readonly QuestionRepository _repoQ;
        private ApplicationDbContext _context;
        public SurveyController()
        {
            _context = new ApplicationDbContext();
            _repoA = new SurveyAnswerRepository();
            _repoT = new SurveyTicketRepository();
            _repoQ = new QuestionRepository();
        }

        [Route("api/survey/{id}")]
        [HttpPost]
        public void sendSurveyLink(int id)
        {
            Client user = _context.Client.Find(id);
            _repoT.CreateSurveyTicket(user);
        }

        [Route("api/survey/{id}/{token}")]
        [HttpGet]
        public Client validSurveyLink(int id, string token)
        {
            if (_repoT.GetOneByToken(id, token) > 0) //ticket is valid
                return _context.Client.Find(id);
            return null;
        }

        [Route("api/survey/answers")]
        [HttpPost]
        public void saveSurveyAnswers([FromBody]dynamic value)
        {
            DateTime dt = DateTime.Now;
            string question_Period = dt.Year + "Q" + (dt.Month + 2) / 3;
            for (int i = 1; i <= 19; i++)
            {
                if (value.survey["Q" + i] != null)
                {
                    int client_SID = Convert.ToInt32(value.survey["client_SID"]);
                    int question_SID = i;
                    string answer_Text = value.survey["Q" + i];
                    _repoA.Add(client_SID, question_Period, question_SID, answer_Text);
                }
            }
        }

        [Route("api/survey/export/{surveyPeriod}")]
        [HttpGet]
        public HttpResponseMessage exportToExcel(string surveyPeriod)
        {
            var fileId = "C:\\Users\\ahill\\excelExport.xlsx";

            var excel = new Application();
            var workbook = excel.Workbooks.Add();
            Worksheet sheet = workbook.Sheets.Add();

            // Header
            sheet.Cells[1, 1] = "Name";
            sheet.Cells[1, 2] = "Survey Period";
            var questions = _repoQ.GetQuestions().ToList();
            for(int i = 0; i < questions.Count; i++)
            {
                sheet.Cells[1, i + 3] = questions[i].Question_Text;
            }

            // Body
            var surveyResults = _repoA.GetSurveyResultsByPeriod(surveyPeriod).ToList();
            var currentRowNum = 1;
            int currentColumnNum = 1;
            int currentClientId = -1;
            for(var i = 0; i < surveyResults.Count; i++)
            {
                var result = surveyResults[i];
                
                if(result.Client_SID != currentClientId)
                {
                    currentClientId = result.Client_SID;
                    currentRowNum++;
                    currentColumnNum = 2;
                    sheet.Cells[currentRowNum, 1] = $"{result.LastName}, {result.FirstName}";
                }

                sheet.Cells[currentRowNum, currentColumnNum] = result.Answer_Text;
                currentColumnNum++;
            }

            workbook.SaveAs(fileId);
            workbook.Close();
            excel.Quit();

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(new FileStream(fileId, FileMode.Open, FileAccess.Read));
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            response.Content.Headers.ContentDisposition.FileName = surveyPeriod + ".xlsx";
            return response;
        }
    }
}
