using Human_Benchmark_2._0.Models.DataModels;

namespace Human_Benchmark_2._0.Interaces
{
    public interface IIOService
    {
        Task<UserDataModel> GetUserByNameAsync(string name);
        Task FillDatabaseWithWordsAsync();
        Task<string[]> GetRandomWordsAsync(int count);
    }
}
