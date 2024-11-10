using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordClique_BusinessLogic.Strategies.UserAlbumLinkUpdateStrategy.Abstractions
{
    public interface IUserAlbumLinkUpdateStrategy
    {
        Task<UserAlbumLink> UpdateAsync(Guid userId, Guid albumId, bool value);
    }

}
