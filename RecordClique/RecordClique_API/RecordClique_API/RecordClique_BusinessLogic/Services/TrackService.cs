using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecordClique_BusinessLogic.DTOs;
using RecordClique_BusinessLogic.Services.Abstractions;
using RecordClique_DataAccess.Entities;
using RecordClique_DataAccess.Repository.Abstraction;

namespace RecordClique_BusinessLogic.Services
{
    public class TrackService : ITrackService
    {
        private readonly IRepository<Track> _trackRepository;
        private readonly IMapper _mapper;

        public TrackService(IRepository<Track> trackRepository, IMapper mapper) {
            _trackRepository = trackRepository;
            _mapper = mapper;
        }

        public async Task<List<TrackDTO>> GetTracks(Guid albumId)
        {
            var query = await _trackRepository.GetAll();
            query = query.Where(t => t.FK_AlbumId == albumId).Include(t => t.Album);

            var tracks = await query.ToListAsync();

            var trackDtos = tracks.Select(t =>
                _mapper.Map<TrackDTO>(t)
            ).ToList();

            return  trackDtos;
        }
    }
}
