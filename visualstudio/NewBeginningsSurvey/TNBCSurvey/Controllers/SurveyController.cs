﻿using TNBCSurvey.DAL;
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
        private ApplicationDbContext _context;
        public SurveyController()
        {
            _context = new ApplicationDbContext();
            _repoA = new SurveyAnswerRepository();
            _repoT = new SurveyTicketRepository();
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
            var fileId = "%HOME%\\data\\excelExport.xlsx";

            var excel = new Application();
            var workbook = excel.Workbooks.Add();
            Worksheet sheet = workbook.Sheets.Add();
            sheet.Cells[1, 1] = "Hello World!";

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
