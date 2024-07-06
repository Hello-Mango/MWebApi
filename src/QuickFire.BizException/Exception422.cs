using System;

namespace QuickFire.BizException
{
    public class Exception422 : Exception
    {
        public string[] Params { get; set; }

        public Exception422(string message, params string[] strings) : base(message)
        {
            Params = strings;
        }
    }
}
