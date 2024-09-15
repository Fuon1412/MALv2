using api.DTOs;
using api.Models;

namespace api.Mappers{
    public static class GenreMapper{
        public static GenreDTO toGenreDTO(this Genre genre){
            return new GenreDTO{
                Id = genre.Id,
                Name = genre.Name,
                Animes = genre.Animes.Select(a => a.toAnimeDTO()).ToList()
            };
        }
    }
}