using System.ComponentModel.DataAnnotations;

namespace RecordClique.Models
{
    public class RecordLabel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Picture Required")]
        public string? Picture { get; set; }
        public string? Biography { get; set;}
        public List<Album>? Albums { get; set; }
    }
}
