namespace Human_Benchmark_2._0.Models.ViewModels
{
    public class ThrownExceptionViewModel
    {
        internal Exception ExceptionM { get;private init; }
        internal bool IsAdmin { get; private init; }
        public ThrownExceptionViewModel(Exception exception, string name)
        {
            ExceptionM = exception;
            IsAdmin = name == "Admin";
        }
    }
}
