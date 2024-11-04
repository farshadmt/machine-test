using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helper
{
    public static class SqlConstants
    {
        public static string UserRegistration => "Usp_User_Registration";

        public static string UserLogin => "Usp_User_Login";

        public static string GetUserlist => "Usp_User_Listing";

        public static string GetUserByCode => "Usp_Get_Userbycode";

        public static string UpdateUser => "Usp_Update_User";
    } 
}
