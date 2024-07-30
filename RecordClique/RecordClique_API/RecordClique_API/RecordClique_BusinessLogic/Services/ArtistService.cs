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
            var artist = _mapper.Map<Artist>(artistRequest);

            await _artistRepository.AddAsync(artist);

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
