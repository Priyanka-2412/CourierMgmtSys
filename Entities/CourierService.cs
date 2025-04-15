using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTasks.Entities
{
    public class CourierService
    {
        public int ServiceID { get; set; }
        public int CourierID { get; set; }
        public string ServiceName { get; set; }
        public decimal Cost { get; set; }

        public CourierService() { }

        public CourierService(int serviceID, int courierID, string serviceName, decimal cost)
        {
            ServiceID = serviceID;
            CourierID = courierID;
            ServiceName = serviceName;
            Cost = cost;
        }

        public override string ToString()
        {
            return $"ServiceID: {ServiceID}, CourierID: {CourierID}, ServiceName: {ServiceName}, Cost: ₹{Cost}";
        }
    }
}
