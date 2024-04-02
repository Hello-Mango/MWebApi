using System;
using System.Collections.Generic;
using System.Text;

namespace MWebApi.Core
{
    public class ApiResult<T>
    {
        public int statusCode { get; set; }
        public string msg { get; set; }
        public T data { get; set; }

        public bool succeeded { get; set; }

        public long timestamp { get; set; } = DateTime.UtcNow.Ticks;

        public string errorCode { get; set; }
        public string errors { get; set; }
    }
}
