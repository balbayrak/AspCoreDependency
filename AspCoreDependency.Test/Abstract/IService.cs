using System;

namespace AspCoreDependency.Test.Abstract
{
    public interface IService
    {
        public Guid id { get; }
        public string Run();
    }
}
