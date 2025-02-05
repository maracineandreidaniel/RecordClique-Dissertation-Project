using RecordClique_BusinessLogic.DTOs;

namespace RecordClique_BusinessLogic.Services.Abstractions
{
    public interface ITrackService
    {
        Task<List<TrackDTO>> GetTracks(Guid albumId);
    }
}
