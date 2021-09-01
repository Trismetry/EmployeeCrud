using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using EmployeeCrud.Models;
using EmployeeCrud.DAL;
using System.Data.Entity;
using System.Reflection;

namespace EmployeeCrud.BLL
{
   
    public class Main
    {
        private DBContextEmp db = new DBContextEmp();

        public bool Save(Employee employee)
        {
            bool res = Check(employee);
            if (res)
            {
                if (employee.Id != null)
                    Edit(employee);
                else
                    Create(employee);
            }        
            return res;
        }
        public bool Check(Employee employee)
        {
            Employee employee1 = employee;
            // Get the PersonnelNumber of the object we are saving
            Employee employeeRes = db.Employees.Where(x => x.PersonnelNumber == employee1.PersonnelNumber && x.IsCompanyWorker == employee1.IsCompanyWorker && employee.Id != x.Id).FirstOrDefault();

            if (employeeRes != null)
                return false;

            if (employee.PersonnelNumber == null && employee.IsCompanyWorker)
                return false;

            if (employee.PersonnelNumber != null && !employee.IsCompanyWorker)
                return false;

            return true;
        }
        public void Create(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
        }

        public void Edit(Employee employee)
            {
            db.Entry(employee).State = EntityState.Modified;
            db.SaveChanges();          
        }
    }
    //private T Deserialise<T>(string json)
    //{
    //    using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
    //    {
    //        var serialiser = new DataContractJsonSerializer(typeof(T));
    //        return (T)serialiser.ReadObject(ms);
    //    }
    //}

    //public List<Employee> GetList(string items)
    //{
    //    IEnumerable<Employee> lineitems = Deserialise<IEnumerable<Employee>>(items);
    //    return lineitems.ToList();
    //    // do whatever needs to be done - create, update, delete etc.
    //}

}
