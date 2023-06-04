using System;

namespace IoC.Implementation
{
    public interface IServiceProvider
    {
        T GetService<T>();
        object GetService(Type type);
    }
}