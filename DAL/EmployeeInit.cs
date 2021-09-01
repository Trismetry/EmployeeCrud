using System;
using System.Collections.Generic;
using EmployeeCrud.DAL;
using EmployeeCrud.Models;

namespace EmployeeCrud.DAL
{
    public class EmployeeInit : System.Data.Entity.DropCreateDatabaseAlways<DBContextEmp>
    {
        protected override void Seed(DBContextEmp context)
        {

            DateTime Employeedate = (DateTime.Now).AddYears(45);
            var employees = new List<Employee>
            {
            new Employee{Id=1, PersonnelNumber=21, FirstLastMidName="Calvin Klein",BirthDate=Employeedate},
            new Employee{Id=2, PersonnelNumber=22, FirstLastMidName="Andrew Eldritch",BirthDate=Employeedate, IsMaleGender=true, IsCompanyWorker = true},
            };

            context.Employees.AddRange(employees);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}