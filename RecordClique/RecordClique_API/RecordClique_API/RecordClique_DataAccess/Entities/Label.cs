using System.ComponentModel.DataAnnotations;

namespace RecordClique.Models
{
    public class Label
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Profile Picture Required")]
        public string ProfilePicture { get; set; }

        [Required(ErrorMessage = "Name Required")]
        public string LabelName { get; set; }

        public string Biography { get; set;}

        public List<Album> Albums { get; set; }
    }
}
