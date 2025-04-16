namespace Human_Benchmark_2._0.Models.ViewModels
{
    public class ThrownExceptionViewModel
    {
        public string Message { get; set; }
        public Exception ExceptionM { get; set; }
        public ThrownExceptionViewModel(string message, Exception exception)
        {
            Message = message;
            ExceptionM = exception;
        }
    }
}
