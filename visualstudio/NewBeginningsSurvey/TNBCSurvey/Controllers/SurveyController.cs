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
<<<<<<< HEAD
using System.Dynamic;

namespace TNBCSurvey.Controllers
{

   
=======
using TNBCSurvey.Service;

namespace TNBCSurvey.Controllers
{
  
>>>>>>> 6f833f77287e8d499be64e1d4522e3adf7b7e0f9
    public class SurveyController : ApiController
    {
        readonly ClientRepository _repoC;
        readonly SurveyAnswerRepository _repoA;
        readonly SurveyTicketRepository _repoT;
        readonly QuestionRepository _repoQ;
        private ApplicationDbContext _context;
        public SurveyController()
        {
            _context = new ApplicationDbContext();
            _repoC = new ClientRepository();
            _repoA = new SurveyAnswerRepository();
            _repoT = new SurveyTicketRepository();
            _repoQ = new QuestionRepository();
        }

<<<<<<< HEAD
        [Route("api/survey")]
        [HttpPost]
        public void sendSurveyLinks()
=======
        [Route("api/survey/{id}")]
        [HttpPost]
        public void resendSurveyTicket(int id)
        {
            Client user = _context.Client.Find(id);
            _repoT.ResendSurveyTicket(user);
        }

        [Route("api/survey")]
        [HttpPost]
        public string sendSurveyLinks()
>>>>>>> 6f833f77287e8d499be64e1d4522e3adf7b7e0f9
        {
            var clients = _repoC.GetAllActiveClients();
            foreach(var client in clients)
            {
                _repoT.CreateSurveyTicket(client);
            }
<<<<<<< HEAD
        }

        [Route("api/survey/resend/{clientId}")]
        [HttpPost]
        public string resendSurveyLink(int clientId)
        {
            var clients = _repoC.GetAllActiveClients();
            var client = clients.Where(x => x.Client_SID == clientId).FirstOrDefault();
           return  _repoT.CreateandCopySurveyTicket(client);
            
=======

            return $"Sent emails to {clients.Count()} clients.";
>>>>>>> 6f833f77287e8d499be64e1d4522e3adf7b7e0f9
        }

        [Route("api/survey/{id}/{token}")]
        [HttpGet]
        public Client validSurveyLink(int id, string token)
        {
            if (_repoT.GetOneByToken(id, token) > 0) //ticket is valid
                return _context.Client.Find(id);
            return null;
        }

<<<<<<< HEAD
        [Route("api/survey/answers")]
        [HttpPost]
        public void saveSurveyAnswers([FromBody]dynamic value)
        {
            DateTime dt = DateTime.Now;
            string question_Period = dt.Year + "Q" + (dt.Month + 2) / 3;
=======
        [Route("api/survey/answers/{id}/{token}")]
        [HttpPost]
        public void saveSurveyAnswers(int id, string token, [FromBody]dynamic value)
        {
            var surveyPeriod = SurveyPeriodService.GetCurrentSurveyPeriod();
>>>>>>> 6f833f77287e8d499be64e1d4522e3adf7b7e0f9
            for (int i = 1; i <= 19; i++)
            {
                if (value.survey["Q" + i] != null)
                {
                    int client_SID = Convert.ToInt32(value.survey["client_SID"]);
                    int question_SID = i;
                    string answer_Text = value.survey["Q" + i];
<<<<<<< HEAD
                    _repoA.Add(client_SID, question_Period, question_SID, answer_Text);
                }
            }
        }


        [Route("api/survey/getanswers/{clientId}")]
        [HttpGet]
        public dynamic getSurveyAnswers(string clientId)
        {

            var list = _repoA.GetSurveyResultsByClientId(clientId);
            dynamic output = new List<dynamic>();

            dynamic row = new ExpandoObject();
            

            var surveyAnswerList = list?.ToList(); 


            for (int i= 0 ;i< surveyAnswerList.Count(); i++)
            {
                int queNum = (i + 1);
                string propertyName = "Q" + queNum;
                ((IDictionary<String, Object>)row)[propertyName] = surveyAnswerList[i].Answer_Text;
                ((IDictionary<String, Object>)row)["FirstName"] = surveyAnswerList[i].FirstName;
                ((IDictionary<String, Object>)row)["LastName"] = surveyAnswerList[i].LastName;

            }
            output.Add(row);
            return output;
            
        }

        [Route("api/survey/list")]
        public List<Client> GetAll()
        {
            return _context.Client.ToList();
=======
                    _repoA.Add(client_SID, surveyPeriod, question_SID, answer_Text);
                }
            }

            _repoT.SetTokenUsed(id, token);
>>>>>>> 6f833f77287e8d499be64e1d4522e3adf7b7e0f9
        }

        [Route("api/survey/excel/{surveyPeriod}")]
        [HttpGet]
        public HttpResponseMessage exportToExcel(string surveyPeriod)
        {
<<<<<<< HEAD
            var fileId = @"C:\Users\khu7493\Downloads\excelExport-{Guid.NewGuid()}.xlsx";
=======
            var fileId = $"/excelExport-{Guid.NewGuid()}.xlsx";
>>>>>>> 6f833f77287e8d499be64e1d4522e3adf7b7e0f9

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

<<<<<<< HEAD
        [Route("api/survey/csv/{surveyPeriod}")]
        [HttpGet]
        public HttpResponseMessage exportToCsv(string surveyPeriod)
        {
=======
        [Route("api/survey/csv")]
        [HttpGet]
        public HttpResponseMessage exportToCsv()
        {
            var surveyPeriod = SurveyPeriodService.GetCurrentSurveyPeriod();
>>>>>>> 6f833f77287e8d499be64e1d4522e3adf7b7e0f9
            List<string> rows = new List<string>();

            // Header
            var row = "\"Name\",\"Survey Period\",";
            var questions = _repoQ.GetQuestions().ToList();
            foreach(var question in questions)
            {
                row += $"\"{question.Question_Text}\",";
            }
            rows.Add(row);

            // Body
            var surveyResults = _repoA.GetSurveyResultsByPeriod(surveyPeriod).ToList();
            int currentClientId = -1;
            row = null;
            for (var i = 0; i < surveyResults.Count; i++)
            {
                var result = surveyResults[i];

                if (result.Client_SID != currentClientId)
                {
                    if(row != null)
                    {
                        rows.Add(row);
                    }

                    currentClientId = result.Client_SID;
                    row = $"\"{result.LastName}, {result.FirstName}\",";
                }

                row += $"\"{result.Answer_Text}\",";
            }
            rows.Add(row);

            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            foreach(var r in rows)
            {
                writer.Write(r);
                writer.Write('\n');
            }
            writer.Flush();
            stream.Position = 0;

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StreamContent(stream);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = surveyPeriod + ".csv" };
            return response;
        }
    }
}
