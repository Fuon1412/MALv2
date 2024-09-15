using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Anime
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Rating { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Score { get; set; }
        public int Episodes { get; set; }
        
        public string Description { get; set; } = String.Empty;
        public string Image { get; set; } = String.Empty;
        public List<Character> characters { get; set; } = new List<Character>();
        public List<Genre> Genres { get; set; } = new List<Genre>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}