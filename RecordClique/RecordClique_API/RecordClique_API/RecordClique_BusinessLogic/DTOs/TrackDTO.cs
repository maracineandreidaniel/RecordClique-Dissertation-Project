using RecordClique.Models;

namespace RecordClique_BusinessLogic.DTOs
{
    public class TrackDTO
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public Guid FK_AlbumId { get; set; }
    }
}
