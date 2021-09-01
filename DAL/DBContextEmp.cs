using EmployeeCrud.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EmployeeCrud.DAL
{
    public class DBContextEmp : DbContext
    {
        public DBContextEmp() : base("DBContextEmp")
        {
            Database.SetInitializer(new EmployeeInit());
        }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
