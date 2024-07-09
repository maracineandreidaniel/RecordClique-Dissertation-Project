using RecordClique.Models;

namespace RecordClique_DataAccess.Entities
{
    public class AlbumGenreLink
    {
        public Guid FK_AlbumId { get; set; }
        public Album Album { get; set; }


        public Guid FK_GenreId { get; set; }
        public Genre Genre { get; set; }

    }
}
