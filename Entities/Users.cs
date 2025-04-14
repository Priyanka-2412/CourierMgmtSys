using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTasks.Entities
{
    class Users
    {
        public long UserID { get; set; }
        public string Names { get; set; }
        public string Email { get; set; }
        public string Passwords { get; set; }
        public string ContactNumber { get; set; }
        public string UserAddress { get; set; }
        public string ZipCode { get; set; }

        public Users() { }

        public Users(long userID, string names, string email, string passwords, string contactNumber, string userAddress, string zipCode)
        {
            UserID = userID;
            Names = Names;
            Email = email;
            Passwords = passwords;
            ContactNumber = contactNumber;
            UserAddress = userAddress;
            ZipCode = zipCode;
        }

        public override string ToString()
        {
            return $"UserID: {UserID}, UserName: {Names}, Email: {Email}, Contact: {ContactNumber}, Address: {UserAddress}, ZipCode: {ZipCode}";
        }
    }
}