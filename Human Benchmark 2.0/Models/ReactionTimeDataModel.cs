using System.ComponentModel.DataAnnotations;

namespace Human_Benchmark_2._0.Models
{
    public class ReactionTimeDataModel
    {
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
            this.ReactionTime = reactionTime;
        }
        private int[] Reactions = new int[timesOfReactions];
        private int currentIndex = 0;
        public void CalculateReactionTime()
        {
            this.ReactionTime = Reactions.Average();
        }
        public void AddReactionTime(int reactionTime)
        {
            Reactions[currentIndex] = reactionTime;
            currentIndex++;
        }
    }
}
