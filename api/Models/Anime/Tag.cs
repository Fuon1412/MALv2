using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public List<Anime> Animes { get; set; } = new List<Anime>();
    }
}