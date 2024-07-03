using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecordClique.Models
{
    public class Album 
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Cover { get; set; }

        public DateTime ReleaseDate { get; set; }       

        public int LabelID { get; set; }

        public Label Label { get; set; }      

    }

}
