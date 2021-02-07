using System;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace ElenSoft.Insfrastrcture
{
    public static class ExtensionMethods
    {
        public static string GetUserId(this HttpContext context)
        {
            return context.User == null ? string.Empty : context.User.Claims.Single(x => x.Type == "id").Value;
        }

        public static string ToShamsi(this DateTime dateTime)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(dateTime).ToString("0000") + "/" +
                   pc.GetMonth(dateTime).ToString("00") + "/" +
                   pc.GetDayOfMonth(dateTime).ToString("00");


        }

    }
}