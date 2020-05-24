﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace JdsClient.Models
{
    public class JdsSingleResponse<T>
    {
        public string RequestContent { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public T Data { get; set; }
    }
}
