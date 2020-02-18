using Microsoft.Extensions.DependencyInjection;

namespace AspCoreDependency.Core.Configuration
{
    public class DependencyOption
    {
        public DependencyOption()
        {
            this.namespaceStr = null;
            this.serviceLifetime = ServiceLifetime.Scoped;
        }

        /// <summary>
        /// namespace search with startwith option
        /// </summary>
        public string namespaceStr { get; set; }

        public ServiceLifetime serviceLifetime { get; set; }
    }
}
