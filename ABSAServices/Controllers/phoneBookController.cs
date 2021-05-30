using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ABSAServices.Models;

namespace ABSAServices.Controllers
{
    public class phoneBookController : ApiController
    {
        PhonebookEntities pbContext = new PhonebookEntities();
        public phoneBookController()
        {

        }

        // GET api/phoneBook/
        public List<phoneDetail> Get(string searchText, int searchBy)
        {
            List<phoneDetail> result = new List<phoneDetail>();
            if (searchBy == 2)
            {
                long searchNumber = Int64.Parse(searchText);
                result = pbContext.phoneDetails.Where(t => t.phoneNumber == searchNumber).ToList();
            }
            else if (searchBy == 1)
            {
                result = pbContext.phoneDetails.Where(t => t.name == searchText).ToList();
            }
            return result;
        }

        // POST api/phoneBook/
        public bool Post([FromBody]phoneDetail newPhoneDetails)
        {
            var details = new phoneDetail()
            {
                phoneNumber = newPhoneDetails.phoneNumber,
                name = newPhoneDetails.name,
                createdDate = newPhoneDetails.createdDate
            };

            pbContext.phoneDetails.Add(details);
            if (pbContext.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
