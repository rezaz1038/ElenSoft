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
            public const string List = BaseUrl + "/category/list";
            public const string Upsert = BaseUrl + "/category/upsert";
            public const string Delete = BaseUrl + "/category/{request}";
            public const string Single = BaseUrl + "/category/{request}";
        }

        public static class Tag
        {
            public const string List = BaseUrl + "/tag/list";
            public const string Upsert = BaseUrl + "/tag/upsert";
            public const string Delete = BaseUrl + "/tag/{request}";
            public const string Single = BaseUrl + "/tag/{request}";
        }

        public static class Archive
        {
            public const string List = BaseUrl + "/archive/list";
            public const string Upsert = BaseUrl + "/archive/upsert";
            public const string Delete = BaseUrl + "/archive/{request}";
            public const string Single = BaseUrl + "/archive/{request}";
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
            public const string ListAll = BaseUrl + "/identity/role/list/all";
            public const string List = BaseUrl + "/identity/role/list";
            public const string Upsert = BaseUrl + "/identity/role/upsert";
            public const string Delete = BaseUrl + "/identity/role/{request}";
            public const string Single = BaseUrl + "/identity/role/{request}";


        }

        public static class User
        {
            public const string Register = BaseUrl + "/identity/user/register";
            public const string SetLevel = BaseUrl + "/identity/user/levelup";
            public const string GetCliams = BaseUrl + "/identity/user/claim/list";

            public const string List = BaseUrl + "/identity/user/list";
            public const string Upsert = BaseUrl + "/identity/user/upsert";
            public const string Delete = BaseUrl + "/identity/user/{request}";
            public const string Single = BaseUrl + "/identity/user/{request}";
            public const string SingleModel = BaseUrl + "/identity/user/model/{request}";
            public const string UploadAvatar = BaseUrl + "/identity/user/avatar";

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
