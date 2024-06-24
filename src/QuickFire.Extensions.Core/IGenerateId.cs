using System;
using System.Collections.Generic;
using System.Text;

namespace QuickFire.Extensions.Core
{
    public interface IGenerateId<T>
    {
        public T NextId();
    }
}
