using System.ComponentModel.DataAnnotations;
using RecordClique_DataAccess.Entities;

namespace RecordClique.Models
{
    public class Artist
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [MinLength(3, ErrorMessage = "Full name should have minimum 3 characters!")]
        public string Name { get; set; }
        public string? Picture { get; set; }
        public string? Biography { get; set;}
        public List<AlbumArtistLink>? AlbumArtistLinks { get; set; }


    }
}
