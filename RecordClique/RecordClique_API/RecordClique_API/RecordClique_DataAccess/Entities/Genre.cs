using System.ComponentModel.DataAnnotations;

namespace RecordClique_DataAccess.Entities
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<AlbumGenreLink>? AlbumGenreLinks { get; set; }

    }
}
