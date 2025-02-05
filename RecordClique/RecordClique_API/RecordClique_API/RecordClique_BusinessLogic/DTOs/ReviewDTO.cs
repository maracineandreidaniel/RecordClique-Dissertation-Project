namespace RecordClique_BusinessLogic.DTOs
{
    public class ReviewDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public int Stars { get; set; }
        public Guid FK_UserId { get; set; }
        public Guid FK_AlbumId { get; set; }
    }
}
