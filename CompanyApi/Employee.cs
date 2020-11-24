using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyApi
{
    public class Employee
    {
        public Employee()
        {
        }

        public Employee(string name, long salary)
        {
            Name = name;
            Salary = salary;
        }

        public string EmployeeID { get; set; }
        public string Name { get; set; }
        public long Salary { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((Employee)obj);
        }

        protected bool Equals(Employee otheremployee)
        {
            return otheremployee.EmployeeID == this.EmployeeID && otheremployee.Name == this.Name &&
                   otheremployee.Salary == this.Salary;
        }
    }
}
