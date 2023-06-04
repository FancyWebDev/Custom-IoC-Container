using System;
using IoC.Implementation.Example;

namespace IoC.Dependencies
{
    public class OtherLogger : ILogger
    {
        public void Log(string message) => Console.WriteLine(message);
    }
}

