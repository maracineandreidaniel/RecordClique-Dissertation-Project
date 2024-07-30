using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RecordClique.Models;
using RecordClique.Models.DTOs;
using RecordClique_BusinessLogic.DTOs;
using RecordClique_BusinessLogic.Services.Abstractions;
using RecordClique_DataAccess.Repository.Abstraction;

namespace RecordClique_BusinessLogic.Services
{
    public class RecordLabelService : IRecordLabelService
    {

        private readonly IRepository<RecordLabel> _recordLabelRepository;
        private readonly IMapper _mapper;

        public RecordLabelService(IMapper mapper, IRepository<RecordLabel> recordLabelRepository) {
            this._mapper = mapper;
            this._recordLabelRepository = recordLabelRepository;
        }
        public async Task<RecordLabelDto> AddRecordLabel(RecordLabelDto recordLabelRequest)
        {
            var recordLabel = _mapper.Map<RecordLabel>(recordLabelRequest);
            await _recordLabelRepository.AddAsync(recordLabel);
            return recordLabelRequest;
        }

        public async Task<string> DeleteRecordLabel(Guid id)
        {
            var recordLabel = await _recordLabelRepository.GetByIdAsync(id);
            if (recordLabel != null)
            {
                await _recordLabelRepository.RemoveAsync(recordLabel);
            }
            return "Done!";
        }

        public async Task<IEnumerable<RecordLabelDto>> GetAllRecordLabels()
        {
            var recordLabels = await _recordLabelRepository.GetAll();
            var recordLabelDtos = recordLabels.Select(t => _mapper.Map<RecordLabelDto>(t)).ToList();
            return recordLabelDtos;
        }

        public async Task<RecordLabelDto> GetRecordLabelById(Guid id)
        {
            var recordLabel = await _recordLabelRepository.GetByIdAsync(id);
            return _mapper.Map<RecordLabelDto>(recordLabel);
        }

        public async Task<RecordLabelDto> UpdateRecordLabel(RecordLabelDto recordLabelRequest)
        {
            var recordLabel = await _recordLabelRepository.GetByIdAsync(recordLabelRequest.Id);
            recordLabel.Name = recordLabelRequest.Name;
            recordLabel.Picture = recordLabelRequest.Picture;
            recordLabel.Biography = recordLabelRequest.Biography;
            await _recordLabelRepository.UpdateAsync(recordLabel, recordLabelRequest.Id);
            return _mapper.Map<RecordLabelDto>(recordLabel);
        }
    }
}
