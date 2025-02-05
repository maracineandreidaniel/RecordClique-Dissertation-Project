using RecordClique_DataAccess.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecordClique.Models
{
    public class Review
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "You need to write some text!")]
        public string Text { get; set; }
        public int Stars { get; set; }

        [ForeignKey("User")]
        public Guid FK_UserId { get; set; }
        public User User { get; set; }


        [ForeignKey("Album")]
        public Guid FK_AlbumId { get; set; }
        public Album Album { get; set; }
    }
}
