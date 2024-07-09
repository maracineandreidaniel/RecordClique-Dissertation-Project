using RecordClique.Models;

namespace RecordClique_DataAccess.Entities
{
    public class AlbumArtistLink
    {
        public Guid FK_AlbumId { get; set; }
        public Album Album { get; set; }


        public Guid FK_ArtistId { get; set;}
        public Artist Artist { get; set; }
    }
}
