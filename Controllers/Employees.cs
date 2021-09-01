using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EmployeeCrud.DAL;
using EmployeeCrud.Models;
using EmployeeCrud.BLL;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace EmployeeCrud
{
    public class EmployeesController : Controller
    {
        private DBContextEmp db = new DBContextEmp();
        public Main m_BLL = new Main();

        // GET: Employees
        public ActionResult Index()
        {
            return View(db.Employees.ToList());
        }

        [HttpPost]
        public JsonResult GetList(string data, int pagesize = 10, int pageindex = 1, bool isDes = false)
       {
          pagesize = int.Parse(Request["rows"]??"10");
          pageindex = int.Parse(Request["page"]??"1");

           List<Employee> list = db.Employees.ToList();
          
            return Json(list, JsonRequestBehavior.AllowGet);

        }


        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid) return Json(0, JsonRequestBehavior.AllowGet);
            
            return Json(m_BLL.Save(employee).ToString(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Import()
        {
            return View();
        }

       //[HttpPost]
        public ActionResult ImportSave()
        {
            if (!ModelState.IsValid) return null;

            Stream imStream = Request.Files["userfile[]"].InputStream;
            Employees employees = null;
            using (imStream)
            {
                byte[] array = new byte[imStream.Length];
                imStream.Read(array, 0, array.Length);
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                 employees = JsonConvert.DeserializeObject<Employees> (textFromFile);
            }

           
            StringBuilder fileRes = new StringBuilder();
            

            foreach (Employee emp in employees.employees)
            {
                bool isEditOrCreate = emp.Id == null;
                bool res = m_BLL.Save(emp);

                string editOrCreate = isEditOrCreate ? "Creating new Employee" + (res? " with Id #" + emp.Id.ToString() : string.Empty) : 
                        "Updating Employee information" + " with Id #" + emp.Id.ToString();

                fileRes.AppendLine(editOrCreate + (res ? " - Success" : " - Bussiesss logic check failed"));
            }
                

            return File(Encoding.UTF8.GetBytes(fileRes.ToString()),
               "text/plain",
                string.Format("{0}.txt", "results"));

            //return Json((res).ToString(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

      
        
        [HttpPost]
        public JsonResult Delete(string id)
        {
            Employee employee = db.Employees.Find(int.Parse(id));
            db.Employees.Remove(employee);
            db.SaveChanges();
            return Json(1, JsonRequestBehavior.AllowGet);
        }
      
    }
}
