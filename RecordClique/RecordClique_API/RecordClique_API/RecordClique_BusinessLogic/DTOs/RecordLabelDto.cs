using System.ComponentModel.DataAnnotations;

namespace RecordClique_BusinessLogic.DTOs
{
    public class RecordLabelDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Picture { get; set; }
        public string? Biography { get; set; }
    }
}