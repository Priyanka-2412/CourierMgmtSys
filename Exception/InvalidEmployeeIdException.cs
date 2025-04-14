using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTasks.Exception
{
    public class InvalidEmployeeIdException : ApplicationException
    {
        public InvalidEmployeeIdException()
            : base("Employee not found or the employee has not been assigned any assets.")
        { }

        public InvalidEmployeeIdException(string message)
            : base(message)
        { }
    }
}
