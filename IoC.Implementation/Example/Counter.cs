namespace IoC.Implementation.Example
{
    public class Counter
    {
        private static int _instanceCount;

        private readonly int _instanceNumber;
        private readonly Logger _logger;
    
        public Counter(Logger logger)
        {
            _logger = logger;
            _instanceNumber = _instanceCount++;
        }

        public void ShowCount() => _logger.Log($"Instance number: {_instanceNumber}");
    }
}