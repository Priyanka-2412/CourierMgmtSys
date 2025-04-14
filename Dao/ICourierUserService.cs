using CodingTasks.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTasks.Dao
{
    public interface ICourierUserService
    {
        bool PlaceOrder(Courier courierObj);
        string GetOrderStatus(string trackingNumber);

        bool CancelOrder(string trackingNumber);


        List<Courier> GetAssignedOrder(int courierStaffId);
        List<Courier> RetrieveDeliveryHistory(string trackingNumber);

        decimal GenerateRevenueReport();

    }
}
