using System;
using System.Net;
using System.Web.Http;
using TNBCSurvey.DAL;

namespace TNBCSurvey.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/HealthCheck")]
    public class HealthCheckController : ApiController
    {
        [Route("HealthCheck")]
        [HttpGet]
        public IHttpActionResult HealthCheck()
        {
            try
            {
                // Make sure a database call is successful.
                var repo = new QuestionRepository();
                var questions = repo.GetQuestions();
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {
                return Content(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}