namespace api.DTOs{
    public class AnimeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public decimal Rating { get; set; }
        public decimal Score { get; set; }
        public int Episodes { get; set; }
        
        public string Description { get; set; } = String.Empty;
        public string Image { get; set; } = String.Empty;
        public List<CharacterDTO> Characters { get; set; } = new List<CharacterDTO>();
        public List<GenreDTO> Genres { get; set; } = new List<GenreDTO>();
        public List<TagDTO> Tags { get; set; } = new List<TagDTO>();
    }
}