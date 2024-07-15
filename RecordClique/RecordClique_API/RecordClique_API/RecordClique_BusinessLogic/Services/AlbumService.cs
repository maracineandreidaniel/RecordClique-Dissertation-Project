using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecordClique.Models;
using RecordClique_BusinessLogic.DTOs;
using RecordClique_BusinessLogic.Services.Abstractions;
using RecordClique_DataAccess.Repository.Abstraction;

namespace RecordClique_BusinessLogic.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IRepository<Album> _albumRepository;
        private readonly IMapper _mapper;

        public AlbumService(IRepository<Album> albumRepository, IMapper mapper)
        {
            _albumRepository = albumRepository;
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
    }
}
