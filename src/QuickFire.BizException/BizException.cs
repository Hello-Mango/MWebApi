using System;

namespace QuickFire.BizException
{
    public class EnumException : Exception
    {
        public ExceptionEnum Code { get; }
        public string[] Params { get; set; }
        //public Exception422(string code, string message, params string[] strings) : base(message)
        //{
        //    Code = code;
        //    Params = strings;
        //}
        //public Exception422(string message, params string[] strings) : base(message)
        //{
        //    Params = strings;
        //}
        public EnumException(ExceptionEnum exceptionEnum, params string[] strings)
        {
            Code = exceptionEnum;
            Params = strings;
        }
    }
}
