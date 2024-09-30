using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordClique_DataAccess.Helpers;

namespace RecordClique_BusinessLogic.Services.Abstractions
{
    public interface IGenreService
    {
        Task<List<SelectOptionResult>> GetGenreSelectOptions();

    }
}
