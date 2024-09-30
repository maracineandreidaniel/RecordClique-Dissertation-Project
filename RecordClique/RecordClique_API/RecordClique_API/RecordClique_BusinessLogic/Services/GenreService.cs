using Microsoft.EntityFrameworkCore;
using RecordClique_BusinessLogic.Services.Abstractions;
using RecordClique_DataAccess.Entities;
using RecordClique_DataAccess.Helpers;
using RecordClique_DataAccess.Repository.Abstraction;

namespace RecordClique_BusinessLogic.Services
{
    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> _genreRepository;

        public GenreService(IRepository<Genre> genreRepository)
        {
            this._genreRepository = genreRepository;
        }

        public async Task<List<SelectOptionResult>> GetGenreSelectOptions()
        {

            var query = await _genreRepository.GetAll();

            var genres = query.Select(s => new SelectOptionResult
            {
                Id = s.Id,
                Value = s.Name
            }).ToListAsync();

            return await genres;
        }
    }
}
