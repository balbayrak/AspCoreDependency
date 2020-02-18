using AspCoreDependency.Test.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreDependency.Test.Concrete
{
    public class BaseManager : IService
    {
        public Guid id { get; private set; }
        public BaseManager()
        {
            id = Guid.NewGuid();
        }

        public virtual string Run()
        {
            return string.Empty;
        }
    }
}
