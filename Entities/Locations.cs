using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTasks.Entities
{
    class Locations
    {
        public long LocationID { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }

        public Locations() { }

        public Locations(long locationID, string locationName, string address)
        {
            LocationID = locationID;
            LocationName = locationName;
            Address = address;
        }

        public override string ToString()
        {
            return $"LocationID: {LocationID}, Name: {LocationName}, Address: {Address}";
        }
    }
}
