using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Human_Benchmark_2._0.Models.DataModel
{
    public class UserDataModel : IdentityUser
    {
        [NotMapped]
        public static readonly int TimesOfReactions = 3;
        [Key]
        [Required]
        public override string Id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Length(3, 30)]
        public override string? UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public override string? Email { get; set; }
        [NotMapped]
        private const int arrayCount = 5;
        public int[] reactionTimesArray = new int[arrayCount];
        public UserDataModel(string email, string username)
        {
            Id = Guid.NewGuid().ToString();
            UserName = username;
            Email = email;
        }
        public UserDataModel()
        {
            Id = Guid.NewGuid().ToString();
        }
        public void AddReactionTimeToArray(int reaction)
        {
            int index = Array.IndexOf(reactionTimesArray, default);
            if (index != -1)
            {
                reactionTimesArray[index] = reaction;
            }
            else
            {
                Array.Copy(reactionTimesArray, 1, reactionTimesArray, 0, arrayCount - 1);
                reactionTimesArray[arrayCount - 1] = reaction;
            }
        }
    }
}
