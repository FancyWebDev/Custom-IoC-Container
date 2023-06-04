namespace IoC.Implementation.Example
{
    public class Logger
    {
        private readonly ILogger _logger;

        public Logger(ILogger logger)
        {
            _logger = logger;
        }
        
        public void Log(string message) => _logger.Log(message);
    }
}