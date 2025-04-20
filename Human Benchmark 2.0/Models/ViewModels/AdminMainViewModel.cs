using Human_Benchmark_2._0.Models.DataModels;

namespace Human_Benchmark_2._0.Models.ViewModels
{
    public class AdminMainViewModel
    {
        public UserDataModel User { get; private init; }
        public List<UserDataModel> Users { get; private init; }
        public int PageNumber { get; private init; }
        public bool FinalPage { get; private init; }
        public bool FirstPage { get; private init; }
        public AdminMainViewModel(UserDataModel user, List<UserDataModel> users, int pageNumber, bool finalPage, bool firstPage) 
        {
            User = user;
            Users = users;
            PageNumber = pageNumber;
            FinalPage = finalPage;
            FirstPage = firstPage;
        }
    }
}
