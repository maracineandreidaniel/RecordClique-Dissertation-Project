using RecordClique.Models.DTOs;

namespace RecordClique_BusinessLogic.DTOs
{
    public class AlbumDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public string? Cover { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public RecordLabelDto? RecordLabel { get; set; }

        public List<GenreDto>? Genres { get; set; }

        public List<ArtistDto>? Artists { get; set; }

    }
}
