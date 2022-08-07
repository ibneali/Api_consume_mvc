using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using apicrud.Models;

namespace apicrud.Controllers
{
   
    public class empController : ApiController
    {
        studentEntities dbobj = new studentEntities();
        public IHttpActionResult getemp()
        {
            var result = dbobj.Employees.ToList();
            return Ok(result);
        }
        [HttpPost]
        public IHttpActionResult empinsert(Employee obj)
        {
            dbobj.Employees.Add(obj);
            dbobj.SaveChanges();
            return Ok();
        }
        public IHttpActionResult getemp(int id)
        {
            empclass empdetails = null;
            empdetails = dbobj.Employees.Where(x => x.Id == id).Select(empmodel => new empclass()
            {

                Id = empmodel.Id,
                Firstname = empmodel.Firstname,
                Lastname = empmodel.Lastname,
                Country = empmodel.Country,
                Salary = empmodel.Salary,
                Gender = empmodel.Gender,
                email = empmodel.email,
                password = empmodel.password,

            }).FirstOrDefault<empclass>();
            if (empdetails == null)
            {
                return NotFound();
            }

            return Ok(empdetails);
            
        }
        public IHttpActionResult Put(empclass obj)
        {
            var updateemp = dbobj.Employees.Where(x => x.Id == obj.Id).FirstOrDefault<Employee>();
            if (updateemp != null)
            {
                updateemp.Id = obj.Id;
                updateemp.Firstname = obj.Firstname;
                updateemp.Lastname = obj.Lastname;
                updateemp.Country = obj.Country;
                updateemp.Salary = obj.Salary;
                updateemp.Gender = obj.Gender;
                updateemp.email = obj.email;
                updateemp.password = obj.password;
                dbobj.SaveChanges();

            }
            else
            {
                return NotFound();
            }
            return Ok();
        }
        public IHttpActionResult delete(int id)
        {
            var empdel = dbobj.Employees.Where(x => x.Id == id).FirstOrDefault();
            dbobj.Entry(empdel).State = System.Data.Entity.EntityState.Deleted;
            dbobj.SaveChanges();
            return Ok();
        }
    }
}