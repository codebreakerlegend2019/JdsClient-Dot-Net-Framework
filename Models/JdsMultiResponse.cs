using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace JdsClient.Models
{
    public class JdsMultiReponse<T>
    {
        public string RequestError { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public List<T> Data { get; set; }
    }
}
