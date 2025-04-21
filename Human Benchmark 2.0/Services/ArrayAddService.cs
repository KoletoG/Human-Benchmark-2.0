using Human_Benchmark_2._0.Interaces;

namespace Human_Benchmark_2._0.Services
{
    public class ArrayAddService : IArrayAddService
    {
        public void AddValueToArray<T>(T[] array, T score) where T : struct
        {
            int index = Array.IndexOf(array, default);
            if (index != -1)
            {
                array[index] = score;
            }
            else
            {
                Span<T> scores = array;
                scores.Slice(1).CopyTo(scores);
                scores[^1] = score;
            }
        }
    }
}
