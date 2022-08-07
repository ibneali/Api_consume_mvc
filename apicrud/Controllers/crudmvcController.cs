using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using apicrud.Models;
using System.Net.Http;

namespace apicrud.Controllers
{
    public class crudmvcController : Controller
    {
        // GET: crudmvc
        HttpClient clt = new HttpClient();
        public crudmvcController()
        {           
            clt.BaseAddress = new Uri("http://localhost:8522/api/emp");
        }
        public ActionResult Index()
        {
            IEnumerable<Employee> empobj = null;

            var consumeapi = clt.GetAsync("emp");
            consumeapi.Wait();
            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<IList<Employee>>();
                displaydata.Wait();
                empobj = displaydata.Result;
            }

            return View(empobj);
        }
        public ActionResult add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult add(Employee obj)
        {
            var insertrecord = clt.PostAsJsonAsync<Employee>("emp", obj);
            insertrecord.Wait();
            var savedata = insertrecord.Result;
            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            return View("add");
        }
        public ActionResult detail(int id)
        {
            empclass empobj = null;
            var consumapi = clt.GetAsync("emp?id=" + id.ToString());
            consumapi.Wait();
            var readdata = consumapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<empclass>();
                displaydata.Wait();
                empobj = displaydata.Result;

            }
            return View(empobj);
        }
        public ActionResult Edit(int id)
        {
            empclass empobj = null;
            var consumapi = clt.GetAsync("emp?id=" + id.ToString());
            consumapi.Wait();
            var readdata = consumapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<empclass>();
                displaydata.Wait();
                empobj = displaydata.Result;

            }
            return View(empobj);

        }
        [HttpPost]
        public ActionResult Edit(empclass obj)
        {
            var insertrecord = clt.PutAsJsonAsync<empclass>("emp",obj);
            insertrecord.Wait();
            var savedata = insertrecord.Result;
            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            else
            {
                ViewBag.msg = "Employee Record Not Updated...";
            }
            return View(obj);
        }
        public ActionResult Delete(int id)
        {
            var delrecord = clt.DeleteAsync("emp/" + id.ToString());
            delrecord.Wait();
            var displaydata = delrecord.Result;
            if (displaydata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Index");
        }
    }
}