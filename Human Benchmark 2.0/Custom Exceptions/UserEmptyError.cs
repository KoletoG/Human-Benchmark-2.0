namespace Human_Benchmark_2._0.Custom_Exceptions
{
    public class UserEmptyException : Exception
    {
        public UserEmptyException() : base("Username is empty!")
        {

        }
    }
}
