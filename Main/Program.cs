using CodingTasks.Dao;
using CodingTasks.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTasks.Main
{
    class program
    {

        static void Main(string[] args)
        {
            ICourierUserService courierService = new CourierUserServiceImpl();
            ICourierAdminService adminService = new CourierAdminServiceImpl();
            CourierManagementMenu menu = new CourierManagementMenu(courierService, adminService);
            menu.DisplayMenu();
        }
    }
}