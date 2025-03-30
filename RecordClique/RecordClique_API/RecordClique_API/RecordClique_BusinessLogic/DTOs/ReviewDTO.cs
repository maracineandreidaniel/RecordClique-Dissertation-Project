namespace RecordClique_BusinessLogic.DTOs
{
    public class ReviewDTO
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public Guid FK_UserId { get; set; }
        public Guid FK_AlbumId { get; set; }
        public string UserName { get; set; }
    }
}
