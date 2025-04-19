using Human_Benchmark_2._0.Data.Migrations;
using System;

namespace Human_Benchmark_2._0.Methods
{
   
    public static class GlobalStaticMethods
    {
        public static void AddValueToArray<T>(this T[] array, T score)
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
