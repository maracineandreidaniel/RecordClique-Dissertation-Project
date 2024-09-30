using RecordClique_BusinessLogic.DTOs;
using RecordClique_DataAccess.Helpers;

namespace RecordClique_BusinessLogic.Services.Abstractions
{
    public interface IRecordLabelService
    {
        Task<PaginatedResult<RecordLabelDto>> GetRecordLabels(int pageNumber, int pageSize);

        Task<RecordLabelDto> GetRecordLabelById(Guid id);

        Task<RecordLabelDto> AddRecordLabel(RecordLabelDto recordLabelRequest);

        Task<RecordLabelDto> UpdateRecordLabel(RecordLabelDto recordLabelRequest);

        Task<object> DeleteRecordLabel(Guid id);

        Task<List<SelectOptionResult>> GetRecordLabelSelectOptions();
    }
}
