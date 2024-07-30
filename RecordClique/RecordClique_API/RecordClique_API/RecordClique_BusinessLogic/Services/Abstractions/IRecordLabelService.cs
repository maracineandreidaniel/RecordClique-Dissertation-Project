using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordClique.Models.DTOs;
using RecordClique_BusinessLogic.DTOs;

namespace RecordClique_BusinessLogic.Services.Abstractions
{
    public interface IRecordLabelService
    {
        Task<IEnumerable<RecordLabelDto>> GetAllRecordLabels();

        Task<RecordLabelDto> GetRecordLabelById(Guid id);

        Task<RecordLabelDto> AddRecordLabel(RecordLabelDto recordLabelRequest);

        Task<RecordLabelDto> UpdateRecordLabel(RecordLabelDto recordLabelRequest);

        Task<string> DeleteRecordLabel(Guid id);
    }
}
