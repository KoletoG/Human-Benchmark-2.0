using Human_Benchmark_2._0.Data;
using Human_Benchmark_2._0.Models;
using Microsoft.EntityFrameworkCore;

namespace Human_Benchmark_2._0.Methods
{
    public static class IOMethods
    {
        public static async Task<UserDataModel> GetUserByNameAsync(this ApplicationDbContext _context,string name)
        {
            return await _context.Users.SingleAsync(x => x.UserName == name);
        }
    }
}
