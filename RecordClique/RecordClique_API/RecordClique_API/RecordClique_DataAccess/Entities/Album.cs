using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RecordClique_DataAccess.Entities;

namespace RecordClique.Models
{
    public class Album 
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required!")]
        [MinLength(3, ErrorMessage = "Title should have minimum 3 characters")]
        public string Title { get; set; }

        public string? Description { get; set; }

        public string? Cover { get; set; }

        [Required(ErrorMessage = "Release Date is required!")]
        public DateTime ReleaseDate { get; set; }       

        public Guid FK_RecordLabelId { get; set; }

        public RecordLabel RecordLabel { get; set; }      

        public List<Genre>? Genres { get; set; }
        public List<User>? Users { get; set; }
        public List<Artist>? Artists { get; set; }

    }

}
