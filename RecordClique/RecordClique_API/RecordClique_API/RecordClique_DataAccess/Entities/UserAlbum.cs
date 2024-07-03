using RecordClique.Models;
using RecordClique_DataAccess.Entities;
using System.ComponentModel.DataAnnotations;

public class UserAlbum { 
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; }

    public int AlbumId { get; set; }
    public Album Album { get; set; }

    public bool IsListening { get; set; }
    public bool IsFavourite { get; set; }
    public bool IsOnWishlist { get; set; }
}
