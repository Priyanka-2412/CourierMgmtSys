using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTasks.Entities
{
    public class CourierCompany
    {
        public string CompanyName { get; set; }
        public List<Courier> CourierDetails { get; set; }
        public List<Employee> EmployeeDetails { get; set; }
        public List<Locations> LocationDetails { get; set; }

        public CourierCompany()
        {
            CourierDetails = new List<Courier>();
            EmployeeDetails = new List<Employee>();
            LocationDetails = new List<Locations>();
        }

        public CourierCompany(string companyName, List<Courier> courierDetails, List<Employee> employeeDetails, List<Locations> locationDetails)
        {
            CompanyName = companyName;
            CourierDetails = courierDetails ?? new List<Courier>();
            EmployeeDetails = employeeDetails ?? new List<Employee>();
            LocationDetails = locationDetails ?? new List<Locations>();
        }

        public override string ToString()
        {
            return $"Company: {CompanyName}, Couriers: {CourierDetails.Count}, Employees: {EmployeeDetails.Count}, Locations: {LocationDetails.Count}";
        }
    }
}
