using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyApi
{
    public class EmployeeUpdateModel
    {
        public EmployeeUpdateModel()
        {
        }

        public EmployeeUpdateModel(string name, long salary)
        {
            Name = name;
            Salary = salary;
        }

        public string Name { get; set; }
        public long Salary { get; set; }
    }
}
