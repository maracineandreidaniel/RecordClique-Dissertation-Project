using System.ComponentModel.DataAnnotations;
using RecordClique.Models;
using RecordClique_DataAccess.Entities;

public class UserAlbumLink
{
    [Key]
    public Guid UserAlbumLinkId { get; set; }
    public Guid FK_UserId { get; set; }
    public User User { get; set; }

    public Guid FK_AlbumId { get; set; }
    public Album Album { get; set; }



    public bool IsListening { get; set; }
    public bool IsFavourite { get; set; }
    public bool IsOnWishlist { get; set; }
}
