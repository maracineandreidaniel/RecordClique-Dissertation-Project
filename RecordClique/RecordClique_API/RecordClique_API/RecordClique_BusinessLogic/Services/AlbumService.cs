using AutoMapper;
using Lucene;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Microsoft.EntityFrameworkCore;
using RecordClique.Models;
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

        public async Task<PaginatedResult<AlbumDto>> GetAlbums(int pageNumber, int pageSize, string? filterName, Guid? artistId, Guid? genreId, int? year, Guid? userId)
        {
            var query = await _albumRepository.GetAll();
            query = query.Include(a => a.RecordLabel)
                .Include(ag => ag.AlbumGenreLinks!)
                .ThenInclude(link => link.Genre)
                .Include(aa => aa.AlbumArtistLinks!)
                .ThenInclude(link => link.Artist);

            //if (!string.IsNullOrEmpty(filterName))
            //{
            //    query = query.Where(s => LuceneSearch(s.Title, filterName) == true);
            //}

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

            var albumsQuery = await query.ToListAsync();

            var albumDtos = albumsQuery
                .Select(a => _mapper.Map<AlbumDto>(a)).ToList();

            if (!string.IsNullOrEmpty(filterName))
            {
                albumDtos = albumDtos.Where(s => LuceneSearch(s.Title, filterName) == true).ToList();
            }

            var totalItems = albumDtos.Count();

            var albums = albumDtos
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .OrderByDescending(s => s.Title);

            if (userId != null)
            {
                foreach (var album in albumDtos)
                {
                    var userAlbumLinks = await _userAlbumLinkRepository.GetAll();
                    var userAlbumLink = await userAlbumLinks
                        .Where(s => s.FK_AlbumId == album.Id && s.FK_UserId == userId)
                        .FirstOrDefaultAsync();
                    if (userAlbumLink != null)
                    {
                        album.IsFavourite = userAlbumLink.IsFavourite;
                        album.IsListening = userAlbumLink.IsListening;
                        album.IsOnWishlist = userAlbumLink.IsOnWishlist;
                    }
                }
            }

            return new PaginatedResult<AlbumDto>
            {
                Items = albums,
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        private bool LuceneSearch(string title, string filterName)
        {
            var luceneVersion = Lucene.Net.Util.LuceneVersion.LUCENE_48;
            using var directory = new RAMDirectory();
            Analyzer standardAnalyzer = new StandardAnalyzer(luceneVersion);

            using var writer = new IndexWriter(directory, new IndexWriterConfig(luceneVersion, standardAnalyzer));

            var doc = new Lucene.Net.Documents.Document
            {
                new Lucene.Net.Documents.TextField("title", title, Lucene.Net.Documents.Field.Store.YES)
            };
            writer.AddDocument(doc);
            writer.Flush(triggerMerge: false, applyAllDeletes: false);

            var parser = new QueryParser(luceneVersion, "title", standardAnalyzer);
            var query = parser.Parse(filterName);

            using var reader = writer.GetReader(applyAllDeletes: true);
            var searcher = new IndexSearcher(reader);
            var hits = searcher.Search(query, 10).ScoreDocs;

            return hits.Any();
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

        public async Task<PaginatedResult<AlbumDto>> GetUserAllAlbums(int pageNumber, int pageSize, Guid userId, int? type)
        {
            var query = await _userAlbumLinkRepository.GetAll();
            query = query.Where(s => s.FK_UserId == userId);
            query = query
                .Include(s => s.Album)
                    .ThenInclude(a => a.RecordLabel)
                .Include(s => s.Album)
                    .ThenInclude(a => a.AlbumGenreLinks!)
                    .ThenInclude(link => link.Genre)
                .Include(s => s.Album)
                    .ThenInclude(a => a.AlbumArtistLinks!)
                    .ThenInclude(link => link.Artist)
                .Include(s => s.Album)
                    .ThenInclude(a => a.UserAlbumLinks);

            if (type == 1)
            {
                query = query.Where(a => a.IsFavourite == true);
            }
            else if (type == 2)
            {
                query = query.Where(a => a.IsListening == true);
            }
            else if(type == 3)
            {
                query = query.Where(a => a.IsOnWishlist == true);
            }

            var totalItems = query.Count();

            var albums = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(s => s.Album)
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

        public async Task<UserAlbumLink> UpdateUserAlbumLink(Guid albumId, Guid userId, Boolean ind, int type)
        {
            if (type == 1)
            {
                var favouriteStrategy = new FavouriteUpdateStrategy(_userAlbumLinkRepository);
                return await favouriteStrategy.UpdateAsync(userId, albumId, ind);
            }
            else if (type == 2)
            {
                var listeningStrategy = new ListeningUpdateStrategy(_userAlbumLinkRepository);
                return await listeningStrategy.UpdateAsync(userId, albumId, ind);
            }
            else if( type == 3)
            {
                var wishlistStrategy = new WishlistUpdateStrategy(_userAlbumLinkRepository);
                return await wishlistStrategy.UpdateAsync(userId, albumId, ind);
            }
            else
            {
                throw new Exception("Invalid type");
            }
        }

    }
}
