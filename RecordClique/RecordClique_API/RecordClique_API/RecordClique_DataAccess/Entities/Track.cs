using RecordClique.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecordClique_DataAccess.Entities
{
    public class Track
    {
        [Key]
        public Guid Id { get; set; }
        public string Path { get; set; }

        [ForeignKey("Album")]
        public Guid FK_AlbumId { get; set; }
        public Album Album { get; set; }
    }
}
