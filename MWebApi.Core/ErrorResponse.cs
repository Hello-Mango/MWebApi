using System;
using System.Collections.Generic;
using System.Text;

namespace MWebApi.Core
{
    public class ErrorResponse
    {
        public bool Successed { get; set; }
        public string Message { get; set; }
        public string DebugMessage { get; set; }
        public string Timestamp { get; set; } = DateTime.UtcNow.Ticks.ToString();
    }
}
