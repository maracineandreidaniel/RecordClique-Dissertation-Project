using System.ComponentModel.DataAnnotations;

namespace RecordClique.Models
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Profile picture is required!")]
        public string ProfilePicture { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [StringLength(10,MinimumLength = 3,ErrorMessage = "Full name between 3 and 10")]
        public string ArtistName { get; set; }

        [Required(ErrorMessage = "Biography is required!")]
        public string Biography { get; set;}

        public List<Album> Albums { get; set; }


    }
}
