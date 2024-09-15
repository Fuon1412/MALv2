namespace api.DTOs
{
    public class TagDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public List<AnimeDTO> Animes { get; set; } = new List<AnimeDTO>();
    }
}