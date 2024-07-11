using AutoMapper;
using RecordClique.Models;
using RecordClique.Models.DTOs;
using RecordClique_BusinessLogic.Services.Abstractions;
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
            var artist = new Artist
            {
                Id = Guid.NewGuid(),
                Name = artistRequest.Name,
                Picture = artistRequest.Picture,
                Biography = artistRequest.Biography
            };

            _artistRepository.AddAsync(artist);

            return artistRequest;

        }

        public async Task<string> DeleteArtist(Guid id)
        {
            var artist = await _artistRepository.GetByIdAsync(id);
            if (artist != null)
            {
                await _artistRepository.RemoveAsync(artist);
            }
            return "Done!";
        }

        public async Task<IEnumerable<ArtistDto>> GetAllArtists()
        {
           var artists = await _artistRepository.GetAll();
            var artistDtos = artists.Select(t => _mapper.Map<ArtistDto>(t)).ToList();
            return artistDtos;
        }

        public Task<ArtistDto> GetArtistById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ArtistDto> UpdateArtist(ArtistDto artistRequest)
        {
            throw new NotImplementedException();
        }
    }
}
