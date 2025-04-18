using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

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
        private const int arrayCount = 5;
        public int[] reactionTimesArray = new int[arrayCount];
        public double[] avgTimeScoreArray = new double[arrayCount];
        public int[] memoryNumbersScoreArray = new int[arrayCount];
        public int[] memoryWordsScoreArray = new int[arrayCount];
        public int[] reverseWordsScoreArray = new int[arrayCount];
        public int[] reverseNumbersScoreArray = new int[arrayCount];
        public int[] blocksScoreArray = new int[arrayCount];
        public int[] audioReactionAvgTimeArray = new int[arrayCount];
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
                Span<int> scores = reactionTimesArray;
                scores.Slice(1).CopyTo(scores);
                scores[^1] = reaction;
            }
        }
        public void AddAudioReactionAvgToArray(int reaction)
        {
            int index = Array.IndexOf(audioReactionAvgTimeArray, default);
            if (index != -1)
            {
                audioReactionAvgTimeArray[index] = reaction;
            }
            else
            {
                Span<int> scores = audioReactionAvgTimeArray;
                scores.Slice(1).CopyTo(scores);
                scores[^1] = reaction;
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
                Span<int> scores = blocksScoreArray;
                scores.Slice(1).CopyTo(scores);
                scores[^1] = score;
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
                Span<int> scores = reverseWordsScoreArray;
                scores.Slice(1).CopyTo(scores);
                scores[^1] = score;
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
                Span<int> scores = reverseNumbersScoreArray;
                scores.Slice(1).CopyTo(scores);
                scores[^1] = score;
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
                Span<int> scores = memoryWordsScoreArray;
                scores.Slice(1).CopyTo(scores);
                scores[^1] = score;
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
                Span<double> scores = avgTimeScoreArray;
                scores.Slice(1).CopyTo(scores);
                scores[^1] = avgTime;
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
                Span<int> scores = memoryNumbersScoreArray;
                scores.Slice(1).CopyTo(scores);
                scores[^1] = score;
            }
        }
    }
}
