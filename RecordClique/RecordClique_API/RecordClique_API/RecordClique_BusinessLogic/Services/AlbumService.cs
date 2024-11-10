using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecordClique.Models;
using RecordClique.Models.DTOs;
using RecordClique_BusinessLogic.DTOs;
using RecordClique_BusinessLogic.Services.Abstractions;
using RecordClique_BusinessLogic.Strategies.UserAlbumLinkUpdateStrategy;
using RecordClique_DataAccess.Entities;
using RecordClique_DataAccess.Helpers;
using RecordClique_DataAccess.Repository.Abstraction;

namespace RecordClique_BusinessLogic.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IRepository<Album> _albumRepository;
        private readonly IRepository<Artist> _artistRepository;
        private readonly IRepository<AlbumArtistLink> _albumArtistLinkRepository;
        private readonly IRepository<AlbumGenreLink> _albumGenreLinkRepository;
        private readonly IRepository<Genre> _genreRepository;
        private readonly IRepository<UserAlbumLink> _userAlbumLinkRepository;
        private readonly IMapper _mapper;

        public AlbumService(IRepository<Album> albumRepository, IRepository<Artist> artistRepository, IRepository<Genre> genreRepository, IRepository<AlbumArtistLink> albumArtistLinkRepository,
            IRepository<AlbumGenreLink> albumGenreLinkRepository, IRepository<UserAlbumLink> userAlbumLinkRepository, IMapper mapper)
        {
            _albumRepository = albumRepository;
            _genreRepository = genreRepository;
            _artistRepository = artistRepository;
            _albumArtistLinkRepository = albumArtistLinkRepository;
            _albumGenreLinkRepository = albumGenreLinkRepository;
            this._userAlbumLinkRepository = userAlbumLinkRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AlbumDto>> GetAllAlbums()
        {
            var albums = await _albumRepository.GetAll();
            var albumDtos = albums
                .Include(a => a.RecordLabel)
                .Include(a => a.AlbumGenreLinks)
                .ThenInclude(link => link.Genre)
                .Include(a => a.AlbumArtistLinks)
                .ThenInclude(link => link.Artist)
                .Select(a => _mapper.Map<AlbumDto>(a))
                .ToList();  
            return albumDtos;
        }

        public async Task<AlbumDto> AddAlbum(AlbumDto albumRequest)
        {
            var album = _mapper.Map<Album>(albumRequest);
            album.Id = Guid.NewGuid();

            await _albumRepository.AddAsync(album);

            if (albumRequest.Artists != null && albumRequest.Artists.Any())
            {
                foreach (var artistId in albumRequest.Artists)
                {
                    var artist = await _artistRepository.GetByIdAsync(artistId);
                    if (artist != null)
                    {
                        if (album.AlbumArtistLinks == null)
                            album.AlbumArtistLinks = new List<AlbumArtistLink>();

                        await _albumArtistLinkRepository.AddAsync(new AlbumArtistLink
                        {
                            FK_ArtistId = artistId,
                            FK_AlbumId = album.Id,
                            Artist = artist,
                            Album = album,
                        });
                    }
                }
            }

            if (albumRequest.Genres != null && albumRequest.Genres.Any())
            {
                foreach (var genreId in albumRequest.Genres)
                {
                    var genre = await _genreRepository.GetByIdAsync(genreId);
                    if (genre != null)
                    {
                        if (album.AlbumGenreLinks == null)
                            album.AlbumGenreLinks = new List<AlbumGenreLink>();

                        await _albumGenreLinkRepository.AddAsync(new AlbumGenreLink
                        {
                            FK_GenreId = genreId,
                            FK_AlbumId = album.Id,
                            Genre = genre,
                            Album = album,
                        }); ;
                    }
                }
            }

            return albumRequest;

        }

        public async Task<AlbumDto> UpdateAlbum(AlbumDto albumRequest)
        { 
            var albums = await _albumRepository.GetAll();
            var album = await albums
                .Include(a => a.RecordLabel)
                .Include(a => a.AlbumGenreLinks)
                .ThenInclude(link => link.Genre)
                .Include(a => a.AlbumArtistLinks)
                .ThenInclude(link => link.Artist)
                .Where(a => a.Id == albumRequest.Id)
                .FirstOrDefaultAsync();

            if (album == null)
            {
                throw new KeyNotFoundException("Album not found.");
            }

            var newAlbum = _mapper.Map<Album>(albumRequest);

            if (albumRequest.Artists != null)
            {
                var existingArtists = album.AlbumArtistLinks.Select(a => a.FK_ArtistId).ToList();
                foreach (var link in album.AlbumArtistLinks.ToList())
                {
                    if (!albumRequest.Artists.Contains(link.FK_ArtistId))
                    {
                        album.AlbumArtistLinks.Remove(link);
                        await _albumArtistLinkRepository.RemoveAsync(link);
                    }
                }

                foreach (var artistId in albumRequest.Artists)
                {
                    if (!existingArtists.Contains(artistId))
                    {
                        var artist = await _artistRepository.GetByIdAsync(artistId);
                        if (artist != null)
                        {
                            await _albumArtistLinkRepository.AddAsync(new AlbumArtistLink
                            {
                                FK_ArtistId = artistId,
                                FK_AlbumId = album.Id
                            });
                        }
                    }
                }
            }

            if (albumRequest.Genres != null)
            {
                var existingGenres = album.AlbumGenreLinks.Select(g => g.FK_GenreId).ToList();
                foreach (var link in album.AlbumGenreLinks.ToList())
                {
                    if (!albumRequest.Genres.Contains(link.FK_GenreId))
                    {
                        album.AlbumGenreLinks.Remove(link);
                        await _albumGenreLinkRepository.RemoveAsync(link);
                    }
                }

                foreach (var genreId in albumRequest.Genres)
                {
                    if (!existingGenres.Contains(genreId))
                    {
                        var genre = await _genreRepository.GetByIdAsync(genreId);
                        if (genre != null)
                        {
                            await _albumGenreLinkRepository.AddAsync(new AlbumGenreLink
                            {
                                FK_GenreId = genreId,
                                FK_AlbumId = album.Id
                            });
                        }
                    }
                }
            }

            await _albumRepository.UpdateAsync(newAlbum, album.Id);
            return _mapper.Map<AlbumDto>(album);
        }

        public async Task<object> RemoveAlbum(Guid id)
        {
            var album = await _albumRepository.GetByIdAsync(id);
            if (album == null)
            {
                throw new Exception("Album was not found!");
            }
            await _albumRepository.RemoveAsync(album);
            return new { Message = "Album was successfully deleted!" };
        }

        public async Task<PaginatedResult<AlbumDto>> GetAlbums(int pageNumber, int pageSize, string? filterName, Guid? artistId, Guid? genreId, int? year)
        {

            var query = await _albumRepository.GetAll();
            query = query.Include(a => a.RecordLabel)
                .Include(ag => ag.AlbumGenreLinks!)
                .ThenInclude(link => link.Genre)
                .Include(aa => aa.AlbumArtistLinks!)
                .ThenInclude(link => link.Artist);

            if (!string.IsNullOrEmpty(filterName))
            {
                query = query.Where(s => s.Title.ToLower().Contains(filterName.ToLower()));
            }

            if (artistId.HasValue && artistId != Guid.Empty)
            {
                query = query.Where(s => s.AlbumArtistLinks.Any(aa => aa.Artist.Id == artistId));
            }

            if (genreId.HasValue && genreId != Guid.Empty)
            {
                query = query.Where(s => s.AlbumGenreLinks.Any(ag => ag.Genre.Id == genreId));
            }

            if (year.HasValue)
            {
                query = query.Where(s => s.ReleaseDate.Year == year);
            }

            var totalItems = await query.CountAsync();

            var albums = await query
             .Skip((pageNumber - 1) * pageSize)
             .Take(pageSize)
             .OrderByDescending(s => s.Title)
             .ToListAsync();

            var albumDtos = albums
                .Select(a => _mapper.Map<AlbumDto>(a)).ToList();

            return new PaginatedResult<AlbumDto>
            {
                Items = albumDtos,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<AlbumDto> GetAlbumById(Guid id)
        {
            var query = await _albumRepository.GetAll();
            var album = await query.Include(ag => ag.AlbumGenreLinks!)
                .ThenInclude(link => link.Genre)
                .Include(aa => aa.AlbumArtistLinks!)
                .ThenInclude(link => link.Artist)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
            return _mapper.Map<AlbumDto>(album);
        }

        public async Task<IEnumerable<AlbumDto>> GetUserAllAlbums(Guid userId)
        {
            var query =  await _userAlbumLinkRepository.GetAll();
            query = query.Where(s => s.FK_UserId == userId);
            query = query
                .Include(s => s.User) 
                .Include(s => s.Album) 
                    .ThenInclude(a => a.RecordLabel) 
                .Include(s => s.Album)
                    .ThenInclude(a => a.AlbumGenreLinks!) 
                    .ThenInclude(link => link.Genre) 
                .Include(s => s.Album)
                    .ThenInclude(a => a.AlbumArtistLinks!) 
                    .ThenInclude(link => link.Artist) 
                .Include(s => s.Album)
                    .ThenInclude(a => a.RecordLabel); 
            var albums = await query.Select(s => s.Album).ToListAsync();
            var albumDtos = albums
                .Select(a => _mapper.Map<AlbumDto>(a)).ToList();
            return albumDtos;
        }

        public async Task<UserAlbumLink> UpdateUserAlbumLink(Guid albumId, Guid userId, Boolean ind, int type)
        {
            var favouriteStrategy = new FavouriteUpdateStrategy(_userAlbumLinkRepository);
            return await favouriteStrategy.UpdateAsync(userId, albumId, ind);
        }

    }
}
