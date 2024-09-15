namespace api.DTOs
{
    public class CharacterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Image { get; set; } = String.Empty;
        public int? AnimeId { get; set; }  
    }
}