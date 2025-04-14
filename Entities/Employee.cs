using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTasks.Entities
{
    public class Employee
    {
        public long EmployeeID { get; set; }
        public string Names { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Roles { get; set; }
        public double Salary { get; set; }
        public long LocationID { get; set; }
        public string Passwords { get; set; }

        public Employee() { }

        public Employee(long employeeID, string names, string email, string contactNumber, string roles, double salary, long locationID, string passwords)
        {
            EmployeeID = employeeID;
            Names = names;
            Email = email;
            ContactNumber = contactNumber;
            Roles = roles;
            Salary = salary;
            LocationID = locationID;
            Passwords = passwords;
        }

        public override string ToString()
        {
            return $"EmployeeID: {EmployeeID}, Name: {Names}, Role: {Roles}, Salary: ₹{Salary}, LocationID: {LocationID}, Passwords: {Passwords}";
        }
    }
}
