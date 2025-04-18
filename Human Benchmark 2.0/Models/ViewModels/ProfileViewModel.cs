using Human_Benchmark_2._0.Models.DataModels;

namespace Human_Benchmark_2._0.Models.ViewModels
{
    public class ProfileViewModel
    {
        public UserDataModel User { get; private init; }
        public ProfileViewModel(UserDataModel user) 
        {
            User = user;
        }
    }
}
