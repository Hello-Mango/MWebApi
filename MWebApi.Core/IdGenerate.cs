using System;
using System.Collections.Generic;
using System.Text;

namespace MWebApi.Core
{
    public interface IdGenerateInterface<T>
    {
        public T NextId();
    }
}
