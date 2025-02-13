﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using AspCoreDependency.Core.Abstract;
using AspCoreDependency.Core.Concrete;

namespace AspCoreDependency.Core.Configuration
{
    public class DependencyOptionBuilder : ConfigurationOption, IDisposable
    {
        public DependencyOptionBuilder(IServiceCollection services) : base(services)
        {

        }

        public void AutoBind(Action<DependencyOption> option = null)
        {
            string nameSpaceStr = null;

            if (option != null)
            {
                DependencyOption dependencyOption = new DependencyOption();
                option(dependencyOption);
                nameSpaceStr = dependencyOption.namespaceStr;
            }

            BindType<ITransientType>(_services, ServiceLifetime.Transient, nameSpaceStr);
            BindType<IScopedType>(_services, ServiceLifetime.Scoped, nameSpaceStr);
            BindType<ISingletonType>(_services, ServiceLifetime.Singleton, nameSpaceStr);
        }

        public void Bind<TInterface>(Action<DependencyOption> option)
        {
            DependencyOption dependencyOption = new DependencyOption();
            option(dependencyOption);

            BindType<TInterface>(_services, dependencyOption.serviceLifetime, dependencyOption.namespaceStr);
        }

        public void Bind<TInterface, TConcrete>(Action<DependencyOption> option)
        {
            DependencyOption dependencyOption = new DependencyOption();
            option(dependencyOption);

            var descriptor = new ServiceDescriptor(typeof(TInterface), typeof(TConcrete), dependencyOption.serviceLifetime);

            var oldDescription = _services.FirstOrDefault(t => t.ServiceType.Equals(typeof(TInterface)));
            if (oldDescription != null)
            {
                _services.Remove(oldDescription);
            }

            var oldDescriptionImp = _services.FirstOrDefault(t => t.ImplementationType.Equals(typeof(TConcrete)));
            if (oldDescriptionImp != null)
            {
                _services.Remove(oldDescriptionImp);
            }

            _services.Add(descriptor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <param name="services"></param>
        /// <param name="lifeTime"></param>
        /// <param name="namespaceStr">namespace search with startwith option</param>
        private static void BindType<TInterface>(IServiceCollection services, ServiceLifetime lifeTime = ServiceLifetime.Scoped, string namespaceStr = null)
        {
            IEnumerable<TypeMap> maps = TypeMapHelper.GetTypeMaps<TInterface>(AppDomain.CurrentDomain.GetAssemblies(), namespaceStr);

            foreach (var typeMap in maps)
            {
                foreach (var serviceType in typeMap.ServiceTypes)
                {
                    var implementationType = typeMap.ImplementationType;

                    if (!implementationType.IsAssignableTo(serviceType))
                    {
                        throw new InvalidOperationException($@"Type ""{implementationType.ToFriendlyName()}"" is not assignable to ""${serviceType.ToFriendlyName()}"".");
                    }

                    var descriptor = new ServiceDescriptor(serviceType, implementationType, lifeTime);

                    var oldDescription = services.FirstOrDefault(t => t.ServiceType == typeof(TInterface));
                    if (oldDescription != null)
                    {
                        services.Remove(oldDescription);
                    }

                    var oldDescriptionImp = services.FirstOrDefault(t => t.ServiceType == implementationType);
                    if (oldDescriptionImp != null)
                    {
                        services.Remove(oldDescriptionImp);
                    }

                    services.Add(descriptor);
                }
            }

        }

        public void Dispose()
        {

        }
    }
}
