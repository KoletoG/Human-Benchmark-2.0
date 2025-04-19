using System.ComponentModel.DataAnnotations;
using Human_Benchmark_2._0.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Human_Benchmark_2._0.Models.DataModels
{
    public class WordDataModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Word { get; private init; }
        public WordDataModel(string word) 
        {
            Word = word;
        }
    }
}
