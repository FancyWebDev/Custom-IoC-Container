using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace IoC.Implementation
{
    internal class ServiceProvider : IServiceProvider
    {
        private readonly Dictionary<Type, ServiceInfo> _container;
        private readonly Dictionary<Type, object> _singletons = new();

        public ServiceProvider(Dictionary<Type, ServiceInfo> container)
        {
            _container = container;
        }

        public T GetService<T>()
        {
            var type = typeof(T);
            return (T) GetService(type);
        }

        public object GetService(Type type)
        {
            if (_container.TryGetValue(type, out var serviceInfo) == false)
                throw new ArgumentOutOfRangeException(type.Name, $"Cannot resolve {type.FullName}");
        
            if (serviceInfo.ServiceLifeTime == ServiceLifeTime.Singleton && _singletons.TryGetValue(type, out var instance))
                return instance;

            return CreateInstance(serviceInfo);
        }

        private object CreateInstance(ServiceInfo serviceInfo)
        {
            var implementationType = serviceInfo.ImplementationType;
            var constructors = implementationType.GetConstructors();
            if (constructors.Length > 1)
                throw new ArgumentOutOfRangeException(implementationType.Name, $"{implementationType.FullName} has more than 1 constructor");

            var constructor = constructors.Single();
            var parameters = constructor.GetParameters();

            var parameterInstances = new List<object>();

            foreach (var parameter in parameters)
                parameterInstances.Add(GetService(parameter.ParameterType));

            var requiredInstance = parameters.Length > 0
                ? Activator.CreateInstance(
                    implementationType, 
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.CreateInstance, 
                    null, 
                    parameterInstances.ToArray(), 
                    CultureInfo.CurrentCulture)
                : Activator.CreateInstance(implementationType);

            if (requiredInstance == null)
                throw new NullReferenceException($"Cannot invoke constructor of {implementationType.FullName}");
            
            if (serviceInfo.ServiceLifeTime == ServiceLifeTime.Singleton)
                _singletons.TryAdd(serviceInfo.BaseType, requiredInstance);

            return requiredInstance;
        }
    }
}