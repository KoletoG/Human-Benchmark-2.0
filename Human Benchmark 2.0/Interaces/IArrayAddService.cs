namespace Human_Benchmark_2._0.Interaces
{
    public interface IArrayAddService
    {
        void AddValueToArray<T>(T[] array, T score) where T : struct;
    }
}
