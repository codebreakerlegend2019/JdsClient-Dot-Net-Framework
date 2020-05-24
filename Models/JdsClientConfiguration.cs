using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JdsClient.Models
{
    public class JdsClientConfiguration
    {
        public string BaseUrl { get; set; }
        public int RequestRetryCount { get; set; }
        public TimeSpan RequestRetryInterval { get; set; }
    }
}
