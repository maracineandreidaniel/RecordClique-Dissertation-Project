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

        public Guid? RecordLabel { get; set; }

        public List<Guid>? Genres { get; set; }

        public List<Guid>? Artists { get; set; }

        public string? ArtistsNames { get; set; }

        public string? GenresNames { get; set; }

    }
}
