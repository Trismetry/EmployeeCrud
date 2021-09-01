using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace EmployeeCrud.Models
{
    public class Employee 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
        public int? PersonnelNumber { get; set; }
        public string FirstLastMidName { get; set; }
        public bool IsMaleGender { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool IsCompanyWorker { get; set; }

    }
    public class Employees {
        public IList<Employee> employees { get; set; }
    }
}