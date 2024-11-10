using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordClique_BusinessLogic.Strategies.UserAlbumLinkUpdateStrategy.Abstractions;

namespace RecordClique_BusinessLogic.Strategies.UserAlbumLinkUpdateStrategy
{
    public class ListeningUpdateStrategy : IUserAlbumLinkUpdateStrategy
    {
        public Task<UserAlbumLink> UpdateAsync(Guid userId, Guid albumId, bool value)
        {
            throw new NotImplementedException();
        }
    }
}
