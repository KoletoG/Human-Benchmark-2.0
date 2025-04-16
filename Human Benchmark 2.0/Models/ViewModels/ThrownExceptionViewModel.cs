namespace Human_Benchmark_2._0.Models.ViewModels
{
    public class ThrownExceptionViewModel
    {
        public Exception ExceptionM { get; set; }
        public ThrownExceptionViewModel(Exception exception)
        {
            ExceptionM = exception;
        }
    }
}
