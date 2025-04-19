using System.ComponentModel.DataAnnotations;
using Human_Benchmark_2._0.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Human_Benchmark_2._0.Models.DataModels
{
    public class WordDataModel
    {
        [Key]
        [Required]
        public string Id { get; private init; }
        public string Word { get; private init; }
        public WordDataModel(string word) 
        {
            Word = word;
            Id = Guid.NewGuid().ToString();
        }
    }
}
