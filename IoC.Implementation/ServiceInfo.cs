using System;

namespace IoC.Implementation
{
    internal record ServiceInfo
    {
        internal Type ImplementationType { get; }
        internal Type BaseType { get; }
        internal ServiceLifeTime ServiceLifeTime { get; }

        public ServiceInfo(Type implementationType, Type baseType, ServiceLifeTime serviceLifeTime)
        {
            ImplementationType = implementationType;
            BaseType = baseType;
            ServiceLifeTime = serviceLifeTime;
        }
    }
}