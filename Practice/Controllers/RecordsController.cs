using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ClassLibrary;

namespace Practice.Controllers
{
    public class RecordsController : ApiController
    {
        public static PersonDir pdir = new PersonDir();

        // GET api/records
        //Sorting based on lastname firstname by default
        public IEnumerable<string> Get()
        {
            List<string> data = new List<string>();
            List<PersonInfo> pinfo_list = pdir.SortData("name");
            foreach (PersonInfo pinfo in pinfo_list)
            {
                data.Add(pinfo.Lname + ", " + pinfo.Fname + ", " + pinfo.Gendr + ", " + pinfo.Color + ", " + pinfo.Dob.ToString("M/d/yyyy"));
            }
            return data;
        }

        // GET api/records/birthdate
        //sorting based on the sort functionality
        public IEnumerable<string> Get(string id)
        {
            List<string> data = new List<string>();
            List<PersonInfo> pinfo_list = pdir.SortData(id);
            foreach (PersonInfo pinfo in pinfo_list)
            {
                data.Add(pinfo.Lname + ", " + pinfo.Fname + ", " + pinfo.Gendr + ", " + pinfo.Color + ", " + pinfo.Dob.ToString("M/d/yyyy"));
            }
            return data;
        }

        // POST api/records
        // inserting a row in comma, pipe or space seprated format
        public void Post([FromBody]string value)
        {
            pdir.FetchData(value, false);
        }

        /*
        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
        */
    }
}