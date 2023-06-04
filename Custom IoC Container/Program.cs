using IoC.Dependencies;
using IoC.Implementation;
using IoC.Implementation.Example;

var services = new ServiceContainer();

services.AddSingleton<ILogger, OtherLogger>();
services.AddSingleton<Logger>();
services.AddTransient<Counter>();

var serviceProvider = services.Build();
var logger = serviceProvider.GetService<Logger>();

logger.Log("Hello logger");

var counter1 = new Counter(logger);
counter1.ShowCount();
var counter2 = new Counter(logger);
counter2.ShowCount();
var counter3 = new Counter(logger);
counter3.ShowCount();
