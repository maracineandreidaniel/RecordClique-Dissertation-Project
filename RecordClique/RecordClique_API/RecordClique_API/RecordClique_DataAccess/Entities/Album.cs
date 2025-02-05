using System.ComponentModel.DataAnnotations;
using RecordClique_DataAccess.Entities;

namespace RecordClique.Models
{
    public class Album 
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required!")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Title between 3 and 10 characters")]
        public string Title { get; set; }

        public string? Description { get; set; }

        public string? Cover { get; set; }

        [Required(ErrorMessage = "Release Date is required!")]
        public DateTime ReleaseDate { get; set; }       

        public Guid FK_RecordLabelId { get; set; }

        public RecordLabel RecordLabel { get; set; }      

        public List<AlbumGenreLink>? AlbumGenreLinks { get; set; }
        public List<UserAlbumLink>? UserAlbumLinks { get; set; }
        public List<AlbumArtistLink>? AlbumArtistLinks { get; set; }
        public List<Review>? Reviews { get; set; }
        public List<Track>? Tracks { get; set; }

    }

}
