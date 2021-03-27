using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElenSoft.Web
{
    public static class MapRoutes
    {

        // private const string BaseUrl = "api" + "/" + "v{version:apiVersion}";
        private const string BaseUrl = "api" + "/" + "v1";

        public static class Category
        {
            public const string List = BaseUrl + "/offish/category/list";
            public const string Upsert = BaseUrl + "/offish/category/upsert";
            public const string Delete = BaseUrl + "/offish/category/{request}";
            public const string Single = BaseUrl + "/offish/category/{request}";
        }

        public static class Department
        {
            public const string List = BaseUrl + "/department/list";
            public const string Upsert = BaseUrl + "/department/upsert";
            public const string Delete = BaseUrl + "/department/{request}";
            public const string Single = BaseUrl + "/department/{request}";
        }

        public static class PGM
        {

            public const string List = BaseUrl + "/pgm/list";
            public const string Upsert = BaseUrl + "/pgm/upsert";
            public const string Delete = BaseUrl + "/pgm/{request}";
            public const string Single = BaseUrl + "/pgm/{request}";

        }

        public static class Role
        {
            public const string List = BaseUrl + "/identity/role/list";
            public const string Upsert = BaseUrl + "/identity/role/upsert";
            public const string Delete = BaseUrl + "/identity/role/{request}";
            public const string Single = BaseUrl + "/identity/role/{request}";

        }

        public static class User
        {
            public const string List = BaseUrl + "/identity/user/list";
            public const string Upsert = BaseUrl + "/identity/user/upsert";
            public const string Delete = BaseUrl + "/identity/user/{request}";
            public const string Single = BaseUrl + "/identity/user/{request}";

            public const string Login = BaseUrl + "/identity/user/login";
            public const string ResetPassword = BaseUrl + "/identity/user/password/reset";
            public const string ChangePassword = BaseUrl + "/identity/user/password/change";

        }

        public static class Equipment
        {
            public const string List = BaseUrl + "/equipment/list";
            public const string Upsert = BaseUrl + "/equipment/upsert";
            public const string Delete = BaseUrl + "/equipment/{request}";
            public const string Single = BaseUrl + "/equipment/{request}";
            public static class Type
            {
                public const string List = BaseUrl + "/equipment/type/list";
                public const string Upsert = BaseUrl + "/equipment/type/upsert";
                public const string Delete = BaseUrl + "/equipment/type/{request}";
                public const string Single = BaseUrl + "/equipment/type/{request}";
            }
            public static class Brand
            {
                public const string List = BaseUrl + "/equipment/brand/list";
                public const string Upsert = BaseUrl + "/equipment/brand/upsert";
                public const string Delete = BaseUrl + "/equipment/brand/{request}";
                public const string Single = BaseUrl + "/equipment/brand/{request}";
            }
            public static class Place
            {
                public const string List = BaseUrl + "/equipment/place/list";
                public const string Upsert = BaseUrl + "/equipment/place/upsert";
                public const string Delete = BaseUrl + "/equipment/place/{request}";
                public const string Single = BaseUrl + "/equipment/place/{request}";
            }

        }

 
        public static class Offish
        {

           

            public static class Upsert
            {
                public const string register = BaseUrl + "/offish/upsert/register";
                public const string Upshert = BaseUrl + "/offish/category/upsert";
                public const string Delete = BaseUrl + "/offish/category/{request}";
                public const string Single = BaseUrl + "/offish/category/{request}";
            }


        }

    }
}
