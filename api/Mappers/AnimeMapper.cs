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

        public static Anime toCreatedAnimeDTO(this CreateAnimeDTO request){
            return new Anime{
                Name = request.Name,
                Description = request.Description,
                Image = request.Image,
                Episodes = request.Episodes,
                Rating = request.Rating,
                Score = request.Score,
                Tags = request.TagIds.Select(t => new Tag{Id = t}).ToList(),
                Genres = request.GenreIds.Select(g => new Genre{Id = g}).ToList(),
                characters = request.CharacterIds.Select(c => new Character{Id = c}).ToList()
            };
        }
    }
}