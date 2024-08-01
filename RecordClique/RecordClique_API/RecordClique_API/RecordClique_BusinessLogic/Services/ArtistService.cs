using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecordClique.Models;
using RecordClique.Models.DTOs;
using RecordClique_BusinessLogic.Services.Abstractions;
using RecordClique_DataAccess.Helpers;
using RecordClique_DataAccess.Repository.Abstraction;

namespace RecordClique_BusinessLogic.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IRepository<Artist> _artistRepository;
        private readonly IMapper _mapper;

        public ArtistService(IMapper mapper, IRepository<Artist> artistRepository)
        {
            _mapper = mapper;
            _artistRepository = artistRepository;
        }

        public async Task<ArtistDto> AddArtist(ArtistDto artistRequest)
        {
            var artist = _mapper.Map<Artist>(artistRequest);

            await _artistRepository.AddAsync(artist);

            return artistRequest;

        }

        public async Task<object> DeleteArtist(Guid id)
        {
            var artist = await _artistRepository.GetByIdAsync(id);
            if (artist == null)
            {
                throw new Exception("Artist was not found!");
            }
            await _artistRepository.RemoveAsync(artist);
            return new { Message = "Artist was successfully deleted!" };
        }

        public async Task<PaginatedResult<ArtistDto>> GetArtists(int pageNumber, int pageSize, string? filterName)
        {

            var query = await _artistRepository.GetAll();


            if (!string.IsNullOrEmpty(filterName))
            {
                query = query.Where(s => s.Name.ToLower().Contains(filterName.ToLower()));
            }

            var totalItems = await query.CountAsync();


            var artists = await query
             .Skip((pageNumber - 1) * pageSize)
             .Take(pageSize)
             .OrderByDescending(s => s.Name)
             .ToListAsync();

            var artistDtos = artists.Select(s =>
                _mapper.Map<ArtistDto>(s)
            )
                .ToList();

            return new PaginatedResult<ArtistDto>
            {
                Items = artistDtos,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<ArtistDto> GetArtistById(Guid id)
        {
            var artist = await _artistRepository.GetByIdAsync(id);
            return  _mapper.Map<ArtistDto>(artist);
        }

        public async Task<ArtistDto> UpdateArtist(ArtistDto artistRequest)
        {
            var artist = await _artistRepository.GetByIdAsync(artistRequest.Id);    

            artist.Name = artistRequest.Name;
            artist.Picture = artistRequest.Picture;
            artist.Biography = artistRequest.Biography;

            await _artistRepository.UpdateAsync(artist, artistRequest.Id);

            return _mapper.Map<ArtistDto>(artist);
        }


    }
}
