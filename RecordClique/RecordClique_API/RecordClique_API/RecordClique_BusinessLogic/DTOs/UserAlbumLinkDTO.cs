using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordClique.Models;
using RecordClique_DataAccess.Entities;

namespace RecordClique_BusinessLogic.DTOs
{
    public class UserAlbumLinkDTO
    {
        public Guid FK_UserId { get; set; }
        public Guid FK_AlbumId { get; set; }

        public bool IsListening { get; set; }
        public bool IsFavourite { get; set; }
        public bool IsOnWishlist { get; set; }
    }
}
