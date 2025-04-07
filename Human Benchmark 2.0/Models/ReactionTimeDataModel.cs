using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Benchmark_2._0.Models
{
    public class ReactionTimeDataModel
    {
        [NotMapped]
        public static readonly int TimesOfReactions = 3;
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
    }
}
