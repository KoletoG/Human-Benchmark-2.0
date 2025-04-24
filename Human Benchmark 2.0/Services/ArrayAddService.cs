using Human_Benchmark_2._0.Interaces;

namespace Human_Benchmark_2._0.Services
{
    public class ArrayAddService : IArrayAddService
    {
        public void AddScoreToArray(int[] array, int score)
        {
            if (array[5] == default)
            {
                int index1 = Array.IndexOf(array, default);
                if (index1 != -1)
                {
                    array[index1] = score;
                    if (index1 == 5)
                    {
                        Array.Sort(array);
                        Array.Reverse(array);
                    }
                }
            }
            else
            {
                if (score <= array[^1]) return;
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] < score)
                    {
                        for (int y = array.Length - 1; y > i; y--)
                        {
                            array[y] = array[y - 1];
                        }
                        array[i] = score;
                        break;
                    }
                }
            }
            /*
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
            */
        }
        public void AddTimeToArray(double[] array, double time)
        {
            if (array[5] == default)
            {
                int index1 = Array.IndexOf(array, default);
                if (index1 != -1)
                {
                    array[index1] = time;
                    if (index1 == 5)
                    {
                        Array.Sort(array);
                    }
                }
            }
            else
            {
                if (time <= array[^1]) return;
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] > time)
                    {
                        for (int y = array.Length - 1; y > i; y--)
                        {
                            array[y] = array[y - 1];
                        }
                        array[i] = time;
                        break;
                    }
                }
            }
            /*
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
            */
        }
    }
}
