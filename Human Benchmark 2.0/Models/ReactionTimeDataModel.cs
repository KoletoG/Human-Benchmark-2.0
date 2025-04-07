using System.ComponentModel.DataAnnotations;

namespace Human_Benchmark_2._0.Models
{
    public class ReactionTimeDataModel
    {
        [Key]
        public string Id { get; set; }
        public ushort ReactionTime { get; set; }
        public ReactionTimeDataModel() { 
        Id = Guid.NewGuid().ToString();
        }
        public ReactionTimeDataModel(ushort reactionTime)
        {
            this.ReactionTime = reactionTime;
        }
    }
}
