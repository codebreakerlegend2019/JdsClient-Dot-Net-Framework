using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JdsClient.Models
{
    public class JdsResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string RequestContent { get; set; }
    }
}
