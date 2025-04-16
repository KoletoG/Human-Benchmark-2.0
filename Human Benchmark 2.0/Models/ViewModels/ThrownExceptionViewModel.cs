namespace Human_Benchmark_2._0.Models.ViewModels
{
    public class ThrownExceptionViewModel
    {
        public Exception ExceptionM { get; private set; }
        public string? Name { get; private set; }
        public ThrownExceptionViewModel(Exception exception, string name)
        {
            ExceptionM = exception;
            Name = name;
        }
    }
}
