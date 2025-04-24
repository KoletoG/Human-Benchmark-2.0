using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Human_Benchmark_2._0.Models.DataModels
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
        [NotMapped]
        public static readonly int arrayCount = 5;
        public double[] reactionTimesArray = new double[arrayCount];
        public double[] avgTimeScoreArray = new double[arrayCount];
        public int[] memoryNumbersScoreArray = new int[arrayCount];
        public int[] memoryWordsScoreArray = new int[arrayCount];
        public int[] reverseWordsScoreArray = new int[arrayCount];
        public int[] reverseNumbersScoreArray = new int[arrayCount];
        public int[] blocksScoreArray = new int[arrayCount];
        public double[] audioReactionAvgTimeArray = new double[arrayCount];
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
        
    }
}
