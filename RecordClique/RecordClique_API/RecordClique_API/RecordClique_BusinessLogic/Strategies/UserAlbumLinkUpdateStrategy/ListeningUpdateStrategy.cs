using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordClique_BusinessLogic.Strategies.UserAlbumLinkUpdateStrategy.Abstractions;
using RecordClique_DataAccess.Repository.Abstraction;

namespace RecordClique_BusinessLogic.Strategies.UserAlbumLinkUpdateStrategy
{
    public class ListeningUpdateStrategy : IUserAlbumLinkUpdateStrategy
    {
        private readonly IRepository<UserAlbumLink> _userAlbumLinkRepository;

        public ListeningUpdateStrategy(IRepository<UserAlbumLink> userAlbumLinkRepository)
        {
            _userAlbumLinkRepository = userAlbumLinkRepository;
        }

        public async Task<UserAlbumLink> UpdateAsync(Guid userId, Guid albumId, bool value)
        {
            var allLinks = await _userAlbumLinkRepository.GetAll();
            var link = allLinks.Where(s => (s.FK_UserId == userId) && (s.FK_AlbumId == albumId)).FirstOrDefault();

            if (link == null && value)
            {
                link = new UserAlbumLink
                {
                    UserAlbumLinkId = Guid.NewGuid(),
                    FK_UserId = userId,
                    FK_AlbumId = albumId,
                    IsListening = true
                };
                await _userAlbumLinkRepository.AddAsync(link);
            }
            else if (link != null)
            {
                link.IsListening = value;
                await _userAlbumLinkRepository.UpdateAsync(link, link.UserAlbumLinkId);
            }
            return link;
        }
    }
}

