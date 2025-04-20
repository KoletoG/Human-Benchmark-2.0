using Human_Benchmark_2._0.Models.DataModels;

namespace Human_Benchmark_2._0.Models.ViewModels
{
    public class AdminMainViewModel
    {
        public UserDataModel User { get; private init; }
        public List<UserDataModel> Users { get; private init; }
        public AdminMainViewModel(UserDataModel user, List<UserDataModel> users) 
        {
            User = user;
            Users = users;
        }
    }
}
