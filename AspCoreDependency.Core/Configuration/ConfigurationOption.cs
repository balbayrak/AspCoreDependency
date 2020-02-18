using Microsoft.Extensions.DependencyInjection;

namespace AspCoreDependency.Core.Configuration
{
    public class ConfigurationOption
    {
        protected IServiceCollection _services { get; private set; }

        public ConfigurationOption(IServiceCollection services)
        {
            _services = services;
        }
    }
}
