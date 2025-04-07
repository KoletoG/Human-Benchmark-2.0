using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Benchmark_2._0.Models
{
    public class ReactionTimeDataModel
    {
        [NotMapped]
        private const int timesOfReactions = 3;
        [Key]
        public string Id { get; set; }
        public double ReactionTime { get; set; }
        public ReactionTimeDataModel()
        {
            Id = Guid.NewGuid().ToString();
        }
        public ReactionTimeDataModel(ushort reactionTime)
        {
            Id = Guid.NewGuid().ToString();
            this.ReactionTime = reactionTime;
        }
        [NotMapped]
        private int[] Reactions = new int[timesOfReactions];
        [NotMapped]
        private int currentIndex = 0;
        public void CalculateReactionTime()
        {
            this.ReactionTime = Reactions.Average();
            currentIndex = 0;
        }
        public void AddReactionTime(int reactionTime)
        {
            Reactions[currentIndex] = reactionTime;
            currentIndex++;
        }
    }
}
