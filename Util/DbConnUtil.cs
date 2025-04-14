using System.Configuration;

namespace CodingTasks.Util
{
    public class DbConnUtil
    {
        public static string GetConnectionString()
        {
            return "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CourierMgmtSys;Integrated Security=True;";
        }
    }
}