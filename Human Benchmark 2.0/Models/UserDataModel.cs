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
        [Length(3, 30)]
        public override string? UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public override string? Email { get; set; }
        public ReactionTimeDataModel[] ReactionTimesArray = new ReactionTimeDataModel[arrayCount];
        private int currentIndex = 0;
        private const int arrayCount = 5;
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
        public void AddReactionTimeToArray(ReactionTimeDataModel reaction)
        {
            if (currentIndex > arrayCount-1)
            {
                for(int i = 0; i < arrayCount-1; i++)
                {
                    this.ReactionTimesArray[i] = this.ReactionTimesArray[i + 1];
                }
                this.ReactionTimesArray[arrayCount - 1] = reaction;
            }
            else
            {
                this.ReactionTimesArray[currentIndex] = reaction;
                currentIndex++;
            }
        }
    }
}
