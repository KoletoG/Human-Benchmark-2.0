using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Human_Benchmark_2._0.Models
{
    public class UserDataModel : IdentityUser
    {
        [Key]
        [Required]
        public override string Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Length(3,30)]
        public override string? UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public override string? Email { get; set; }
        public UserDataModel(string email, string username)
        {
            Id = Guid.NewGuid().ToString();
            UserName = username;
            Email = email;
        }
        public UserDataModel() { 
        Id=Guid.NewGuid().ToString();
        }
    }
}
