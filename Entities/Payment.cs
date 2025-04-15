using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTasks.Entities
{
    public class Payment
    {
        public long PaymentID { get; set; }
        public long CourierID { get; set; }
        public long LocationID { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public Payment() { }

        public Payment(long paymentID, long courierID, long locationID, double amount, DateTime paymentDate)
        {
            PaymentID = paymentID;
            CourierID = courierID;
            LocationID = locationID;
            Amount = amount;
            PaymentDate = paymentDate;
        }

        public override string ToString()
        {
            return $"PaymentID: {PaymentID}, CourierID: {CourierID}, LocationID: {LocationID}, Amount: ₹{Amount}, Date: {PaymentDate.ToShortDateString()}";
        }
    }
}
