using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace MobiTime.Functions
{
    public static class Authentication
    {
        public static bool Authenticate(string @ApplicationPassword)
        {
            bool Valid;

            if (@ApplicationPassword != "07E1567E-D5D7-440C-AB2E-6A97481B2AF8")
            {
                Valid = false;
            }
            else
            {
                Valid = true;
            }

            return Valid;
        }
    }
}