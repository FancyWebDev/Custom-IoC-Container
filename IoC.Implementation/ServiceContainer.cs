using System;
using System.Collections.Generic;

namespace IoC.Implementation
{
    public class ServiceContainer
    {
        private readonly Dictionary<Type, ServiceInfo> _container = new();

        public IServiceProvider Build() => new ServiceProvider(_container);

        public void AddSingleton<TImplementation>() where TImplementation : class
        {
            var type = typeof(TImplementation);
            AddSingleton(type, type);
        }

        public void AddSingleton<TBaseType, TImplementation>()
        {
            var baseType = typeof(TBaseType);
            var implementationType = typeof(TImplementation);
            AddSingleton(baseType, implementationType);
        }
        
        public void AddTransient<TImplementation>() where TImplementation : class
        {
            var type = typeof(TImplementation);
            AddTransient(type, type);
        }

        public void AddTransient<TBaseType, TImplementation>()
        {
            var baseType = typeof(TBaseType);
            var implementationType = typeof(TImplementation);
            AddTransient(baseType, implementationType);
        }

        private void AddSingleton(Type baseType, Type implementation) => 
            AddService(baseType, implementation, ServiceLifeTime.Singleton);

        private void AddTransient(Type baseType, Type implementation) =>
            AddService(baseType, implementation, ServiceLifeTime.Transient);

        private void AddService(Type baseType, Type implementation, ServiceLifeTime serviceLifeTime) => 
            _container.TryAdd(baseType, new ServiceInfo(implementation, baseType, serviceLifeTime));
    }
}