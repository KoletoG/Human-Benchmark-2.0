using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Human_Benchmark_2._0.Models.DataModels
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
        public double[] avgTimeScoreArray = new double[arrayCount];
        public int[] memoryNumbersScoreArray = new int[arrayCount];
        public int[] memoryWordsScoreArray = new int[arrayCount];
        public int[] reverseWordsScoreArray = new int[arrayCount];
        public int[] reverseNumbersScoreArray = new int[arrayCount];
        public int[] blocksScoreArray = new int[arrayCount];
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
        public void AddScoreBlocksToArray(int score)
        {
            int index = Array.IndexOf(blocksScoreArray, default);
            if (index != -1)
            {
                blocksScoreArray[index] = score;
            }
            else
            {
                Array.Copy(blocksScoreArray, 1, blocksScoreArray, 0, arrayCount - 1);
                blocksScoreArray[arrayCount - 1] = score;
            }
        }
        public void AddReverseWordsScoreToArray(int score)
        {
            int index = Array.IndexOf(reverseWordsScoreArray, default);
            if (index != -1)
            {
                reverseWordsScoreArray[index] = score;
            }
            else
            {
                Array.Copy(reverseWordsScoreArray, 1, reverseWordsScoreArray, 0, arrayCount - 1);
                reverseWordsScoreArray[arrayCount - 1] = score;
            }
        }
        public void AddReverseNumbersScoreToArray(int score)
        {
            int index = Array.IndexOf(reverseNumbersScoreArray, default);
            if (index != -1)
            {
                reverseNumbersScoreArray[index] = score;
            }
            else
            {
                Array.Copy(reverseNumbersScoreArray, 1, reverseNumbersScoreArray, 0, arrayCount - 1);
                reverseNumbersScoreArray[arrayCount - 1] = score;
            }
        }
        public void AddScoreToWordsArray(int score)
        {
            int index = Array.IndexOf(memoryWordsScoreArray, default);
            if (index != -1)
            {
                memoryWordsScoreArray[index] = score;
            }
            else
            {
                Array.Copy(memoryWordsScoreArray, 1, memoryWordsScoreArray, 0, arrayCount - 1);
                memoryWordsScoreArray[arrayCount - 1] = score;
            }
        }
        public void AddCalcSpeedToArray(double avgTime)
        {
            int index = Array.IndexOf(avgTimeScoreArray, default);
            if (index != -1)
            {
                avgTimeScoreArray[index] = avgTime;
            }
            else
            {
                Array.Copy(avgTimeScoreArray, 1, avgTimeScoreArray, 0, arrayCount - 1);
                avgTimeScoreArray[arrayCount - 1] = avgTime;
            }
        }
        public void AddScoreToMemoryNumbersArray(int score)
        {
            int index = Array.IndexOf(memoryNumbersScoreArray, default);
            if (index != -1)
            {
                memoryNumbersScoreArray[index] = score;
            }
            else
            {
                Array.Copy(memoryNumbersScoreArray, 1, memoryNumbersScoreArray, 0, arrayCount - 1);
                memoryNumbersScoreArray[arrayCount - 1] = score;
            }
        }
    }
}
