using System;
using System.Collections.Generic;
using System.Text;

namespace QuickFireApi.Core
{
    public interface IGenerateId<T>
    {
        public T NextId();
    }
}
