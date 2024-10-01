using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Character
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Image { get; set; } = String.Empty;
        public int? AnimeId { get; set; }   
        public Anime? Anime { get; set; }
    }
}