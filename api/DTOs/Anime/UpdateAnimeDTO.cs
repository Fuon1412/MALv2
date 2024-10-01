using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.DTOs{
public class UpadateAnimeDTO
    {
        [Required]
        public string Name { get; set; } = String.Empty;
        [Range(0, 10)]
        public decimal Rating { get; set; }
        [Range(0, 100)]
        public decimal Score { get; set; }
        [Range(1, int.MaxValue)]
        public int Episodes { get; set; }
        public string Description { get; set; } = String.Empty;
        public string Image { get; set; } = String.Empty;
        public List<int> CharacterIds { get; set; } = new List<int>();
        public List<int> GenreIds { get; set; } = new List<int>();        
        public List<int> TagIds { get; set; } = new List<int>();
    }
}