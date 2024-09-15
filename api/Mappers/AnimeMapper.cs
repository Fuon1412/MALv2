using api.DTOs;
using api.Models;

namespace api.Mappers
{
    public static class AnimeMapper{
        public static AnimeDTO toAnimeDTO(this Anime anime){

            return new AnimeDTO{
                Id = anime.Id,
                Name = anime.Name,
                Description = anime.Description,
                Image = anime.Image,
                Episodes = anime.Episodes,
                Rating = anime.Rating,
                Characters = anime.characters.Select(c => c.toCharacterDTO()).ToList(),
                Genres = anime.Genres.Select(g => g.toGenreDTO()).ToList(),
                Tags = anime.Tags.Select(t => t.toTagDTO()).ToList()
            };
        }

        
    }
}