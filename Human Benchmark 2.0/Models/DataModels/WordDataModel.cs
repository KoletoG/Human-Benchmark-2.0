using System.ComponentModel.DataAnnotations;
using Human_Benchmark_2._0.Models.ViewModels;

namespace Human_Benchmark_2._0.Models.DataModels
{
    public class WordDataModel
    {
        [Key]
        public readonly string Id = Guid.NewGuid().ToString();
        public string Word { get; private init; }
        public WordDataModel(string word) 
        {
            Word = word;
        }
    }
}
