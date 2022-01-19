using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Constants
{
    public class Permissions
    {
        public static List<string> GeneratePermissionsForModule(string module)
        {
            return new List<string>()
            {
                $"Permissions.{module}.Create",
                $"Permissions.{module}.View",
                $"Permissions.{module}.Edit",
                $"Permissions.{module}.Delete",
            };
        }


        public static class Properties
        {
            public const string View = "Permissions.Properties.View";
            public const string Create = "Permissions.Properties.Create";
            public const string Edit = "Permissions.Properties.Edit";
            public const string Delete = "Permissions.Properties.Delete";
        }

        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
        }

    }
}
