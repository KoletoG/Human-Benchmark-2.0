using System.ComponentModel.DataAnnotations;
using Human_Benchmark_2._0.Models.ViewModels;

namespace Human_Benchmark_2._0.Models.DataModels
{
    public class WordDataModel
    {
        [Key]
        public string Id { get; set; }
        public string Word { get; set; }
        public WordDataModel(string word) 
        {
            Id=Guid.NewGuid().ToString();
            Word = word;
        }
    }
}
