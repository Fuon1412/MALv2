using api.DTOs;
using api.Models;

namespace api.Mappers{
    public static class CharacterMapper{
        public static CharacterDTO toCharacterDTO(this Character character){
            return new CharacterDTO{
                Id = character.Id,
                Name = character.Name,
                Description = character.Description,
                Image = character.Image,
                AnimeId = character.AnimeId
            };
        }
    }
}