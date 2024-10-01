using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace api.DTOs{
public class CreateAnimeDTO
    {
        [Required]
        public string Name { get; set; } = String.Empty;
        [Range(0, 10)]
        public int Episodes { get; set; }
        public string Description { get; set; } = String.Empty;
        public string Image { get; set; } = String.Empty;
        public List<int> CharacterIds { get; set; } = new List<int>();
        public List<int> GenreIds { get; set; } = new List<int>();        
        public List<int> TagIds { get; set; } = new List<int>();
    }
}