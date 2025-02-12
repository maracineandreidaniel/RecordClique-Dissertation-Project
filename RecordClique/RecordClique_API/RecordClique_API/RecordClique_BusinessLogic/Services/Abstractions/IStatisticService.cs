using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecordClique_BusinessLogic.DTOs;

namespace RecordClique_BusinessLogic.Services.Abstractions
{
    public interface IStatisticService
    {
        Task<List<StatisticDTO>> GetStatisticsAsync();
        Task<IActionResult> GenerateAlbumReport();
    }
}
