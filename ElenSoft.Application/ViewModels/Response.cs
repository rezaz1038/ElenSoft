using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElenSoft.Application.ViewModels
{
    public class Response
    {
        public bool Status { get; set; }
        public string Message { get; set; }

    }

    public class AuthenticationToken
    {
        public string Token { get; set; }
    }

    public class Response<T> : Response
    {
        public T Data { get; set; }

    }



    public class Pagenated
    {
        [JsonProperty("pageId")]
        public int PageId { get; set; }

        [JsonProperty("pageSize")]
        public int PageSize { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }



}



