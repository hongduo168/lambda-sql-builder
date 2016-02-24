using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LambdaSqlBuilder.Tests.Entities
{
    [SqlLambdaTable(Name = "Employees")]
    public class Employee
    {
        [SqlLambdaColumn(Name = "Employee ID")]
        public int EmployeeId { get; set; }

        [SqlLambdaColumn(Name = "First Name")]
        public string FirstName { get; set; }

        [SqlLambdaColumn(Name = "Last Name")]
        public string LastName { get; set; }

        [SqlLambdaColumn(Name = "City")]
        public string City { get; set; }

        [SqlLambdaColumn(Name = "Title")]
        public string Title { get; set; }

        [SqlLambdaColumn(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }
    }
}
