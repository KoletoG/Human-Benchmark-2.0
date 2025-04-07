using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Human_Benchmark_2._0.Models
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
            if (reactionTimesArray.Contains(default))
            {
                for (int i = 0; i < arrayCount; i++)
                {
                    if (reactionTimesArray[i] == default)
                    {
                        reactionTimesArray[i] = reaction;
                        return;
                    }
                }
            }
            else
            {
                for (int i = 0; i < arrayCount - 1; i++)
                {
                    reactionTimesArray[i] = reactionTimesArray[i + 1];
                }
                reactionTimesArray[arrayCount - 1] = reaction;
            }
        }
    }
}
