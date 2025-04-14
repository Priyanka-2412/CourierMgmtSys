using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTasks.Entities
{
    public class Courier
    {
        public long CourierID { get; set; }
        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverAddress { get; set; }
        public double Weights { get; set; }
        public string CourierStatus { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime DeliveryDate { get; set; }
        public long UserID { get; set; }
        public long EmployeeID { get; set; }
        public long LocationID { get; set; }
        public long ServiceID { get; set; }


        public Courier() { }

        public Courier(long courierID, string senderName, string senderAddress, string receiverName, string receiverAddress, double weights, string courierStatus, string trackingNumber, DateTime deliveryDate, long userID, long employeeID, long locationID, long serviceID)
        {
            CourierID = courierID;
            SenderName = senderName;
            SenderAddress = senderAddress;
            ReceiverName = receiverName;
            ReceiverAddress = receiverAddress;
            Weights = weights;
            CourierStatus = courierStatus;
            TrackingNumber = trackingNumber;
            DeliveryDate = deliveryDate;
            UserID = userID;
            EmployeeID = employeeID;
            LocationID = locationID;
            ServiceID = serviceID;
        }

        public override string ToString()
        {
            return $"CourierID: {CourierID}, Sender: {SenderName}, Receiver: {ReceiverName}, Weights: {Weights}kg, Status: {CourierStatus}, Tracking#: {TrackingNumber}, DeliveryDate: {DeliveryDate.ToShortDateString()}, EmployeeID: {EmployeeID}, LocationID: {LocationID}, ServiceID: {ServiceID}";

        }
    }
}
