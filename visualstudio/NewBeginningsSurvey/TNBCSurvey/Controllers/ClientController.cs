using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TNBCSurvey.DAL;
using TNBCSurvey.Models;

namespace TNBCSurvey.Controllers
{
    [Authorize]
    [RoutePrefix("api/client")]
    public class ClientController : ApiController
    {
        private ApplicationDbContext _context;

        //Use Dependency Injection instead
        public ClientController()
        {
            _context = new ApplicationDbContext();
        }

        [Route("view/{id}")]
        public Client GetOneById(int id)
        {
            return _context.Client.Find(id);
        }

        [Route("list")]
        public List<Client> GetAll()
        {
            return _context.Client.ToList();
        }

        [Route("new")]
        [HttpPost]
        public void Add(Client newClient)
        {
            try
            {
                _context.Client.Add(newClient);
                _context.SaveChanges();
            } catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [Route("delete/{id}")]
        [HttpPost]
        public void Delete(int id)
        {
            Client x = _context.Client.Find(id);
            _context.Client.Remove(x);
            _context.SaveChanges();
        }

        [Route("editcontent")]
        [HttpPut]
        public void EditContent(Client client)
        {
            Client x = _context.Client.Find(client.Client_SID);
            x.FirstName = client.FirstName;
            x.LastName = client.LastName;
            x.GroupNumber = client.GroupNumber;
            x.ProgramStartDate = client.ProgramStartDate;
            x.Email = client.Email;
            x.Active = client.Active;
            _context.Entry(x).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}