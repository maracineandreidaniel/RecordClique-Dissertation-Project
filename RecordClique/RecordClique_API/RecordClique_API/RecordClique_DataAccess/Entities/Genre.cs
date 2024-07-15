using System.ComponentModel.DataAnnotations;
using RecordClique.Models;

namespace RecordClique_DataAccess.Entities
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Album>? Albums { get; set; }

    }
}
