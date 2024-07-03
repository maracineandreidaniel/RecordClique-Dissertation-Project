using RecordClique_DataAccess.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecordClique.Models
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Text { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public int AlbumId { get; set; }

        public Album Album { get; set; }
    }
}
