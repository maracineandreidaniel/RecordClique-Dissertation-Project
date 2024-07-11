using System.ComponentModel.DataAnnotations;

namespace RecordClique.Models.DTOs
{
    public class ArtistDto
    {
        [Required]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Picture { get; set; }

        public string? Biography { get; set; }
    }
}
