using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyApi
{
    public class Company
    {
        private long generateEmployeeID;
        private List<Employee> employees = new List<Employee>();

        public Company()
        {
        }

        public Company(string name)
        {
            Name = name;
        }

        public string CompanyID { get; set; }
        public string Name { get; set; }

        public void AddEmployee(Employee employee)
        {
            generateEmployeeID += 1;
            employee.EmployeeID = $"employee_{generateEmployeeID}";
        }

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

            return Equals((Company)obj);
        }

        protected bool Equals(Company otherCompany)
        {
            return otherCompany.CompanyID == this.CompanyID && otherCompany.Name == Name;
        }
    }
}
