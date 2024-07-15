using System.ComponentModel.DataAnnotations;
using RecordClique_DataAccess.Entities;

namespace RecordClique.Models
{
    public class Artist
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        [StringLength(10, MinimumLength = 3, ErrorMessage = "Full name between 3 and 10")]
        public string Name { get; set; }
        public string? Picture { get; set; }
        public string? Biography { get; set;}
        public ICollection<Album>? Albums { get; set; }


    }
}
