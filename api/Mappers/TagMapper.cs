using api.Models;
using api.DTOs;

namespace api.Mappers
{
    public static class TagMapper
    {
        public static TagDTO toTagDTO(this Tag tag)
        {
            return new TagDTO
            {
                Id = tag.Id,
                Name = tag.Name,
                Animes = tag.Animes.Select(a => a.toAnimeDTO()).ToList()
            };
        }

      public static Tag toCreatedTagDTO(this CreatedTagDTO request)
        {
            return new Tag
            {
                Name = request.Name
            };
        }      
    }
}   