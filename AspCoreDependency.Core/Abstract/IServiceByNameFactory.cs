using System;

namespace AspCoreDependency.Core.Abstract
{
    public interface IServiceByNameFactory<TService>
    {
        TService GetByName(IServiceProvider serviceProvider, string name);

        string GetDefaultRegistrationName();
    }
}
