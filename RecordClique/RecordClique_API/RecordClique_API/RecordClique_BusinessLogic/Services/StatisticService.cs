using Microsoft.EntityFrameworkCore;
using RecordClique.Models;
using RecordClique_BusinessLogic.DTOs;
using RecordClique_BusinessLogic.Services.Abstractions;
using RecordClique_BusinessLogic.Strategies.Context;
using RecordClique_BusinessLogic.Strategies.StatisticsStrategy.Abstractions;
using RecordClique_BusinessLogic.Strategies.StatisticsStrategy;
using RecordClique_DataAccess.Repository.Abstraction;
using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Pdf;
using PdfSharpCore;
using RecordClique_BusinessLogic.Exceptions;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace RecordClique_BusinessLogic.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IRepository<Album> _albumRepository;
        private readonly StatisticContext _statisticContext = new StatisticContext();

        public StatisticService(IRepository<Album> albumRepository)
        {
            _albumRepository = albumRepository;
            //_statisticContext = statisticContext;
        }

        public async Task<List<StatisticDTO>> GetStatisticsAsync()
        {
            var strategies = new List<IStatisticStrategy>
            {
                new BestRatedAlbumsStrategy(_albumRepository),
                new WorstRatedAlbumsStrategy(_albumRepository),
                new TopFavouriteAlbumsStrategy(_albumRepository),
                new TopListeningAlbumsStrategy(_albumRepository),
                new TopWishlistAlbumsStrategy(_albumRepository)
            };

            var allStatistics = new List<StatisticDTO>();

            foreach (var strategy in strategies)
            {
                _statisticContext.SetStrategy(strategy);
                var result = await _statisticContext.ExecuteStrategyAsync();
                allStatistics.AddRange(result);
            }

            return allStatistics;
        }
        public async Task<IActionResult> GenerateAlbumReport()
        {
            var statistics = await GetStatisticsAsync();

            if (statistics == null || !statistics.Any())
            {
                throw new Exception("There are no album statistics!");
            }

            string dateNow = DateTime.Now.ToString("yyyy-MM-dd");
            string htmlContent = "<div style='width:100%; text-align:center'>";
            htmlContent += "<h1>Album Statistics Report</h1>";
            htmlContent += "<h2>Generated On: " + dateNow + "</h2>";
            htmlContent += "<p> RecordClique - Album Ranking </p>";
            htmlContent += "<div>";

            Dictionary<int, string> typeTitles = new Dictionary<int, string>
        {
            {Constants.Statistic.Type.BestRatedAlbumsStrategyType, Constants.Statistic.Label.BestRatedAlbumsStrategyLabel},
            {Constants.Statistic.Type.WorstRatedAlbumsStrategyType, Constants.Statistic.Label.WorstRatedAlbumsStrategyLabel},
            {Constants.Statistic.Type.TopFavouriteAlbumsStrategyType, Constants.Statistic.Label.TopFavouriteAlbumsStrategyLabel},
            {Constants.Statistic.Type.TopListeningAlbumsStrategyType, Constants.Statistic.Label.TopListeningAlbumsStrategyLabel},
            {Constants.Statistic.Type.TopWishlistAlbumsStrategyType, Constants.Statistic.Label.TopWishlistAlbumsStrategyLabel},
        };

            foreach (var type in statistics.Select(s => s.Type).Distinct())
            {
                var topAlbums = statistics.Where(s => s.Type == type)
                                           .Take(3)
                                           .ToList();

                string typeTitle = typeTitles.ContainsKey(type) ? typeTitles[type] : "Unknown Category";
                htmlContent += "<h2>" + typeTitle + "</h2>";
                htmlContent += "<table style='width:100%; border-collapse: collapse; border: 1px solid #000;'>";
                htmlContent += "<thead style='font-size: xx-large;; color: red; font-weight:bold'>";
                htmlContent += "<tr>";
                htmlContent += "<td style='border:1px solid #000; padding: 8px;'> Album Title </td>";
                htmlContent += "<td style='border:1px solid #000; padding: 8px;'> Artist Name </td>";
                htmlContent += "<td style='border:1px solid #000; padding: 8px;'> Score </td>";
                htmlContent += "</tr>";
                htmlContent += "</thead>";
                htmlContent += "<tbody>";

                foreach (var album in topAlbums)
                {
                    htmlContent += "<tr style='border:1px solid #000; text-align:center;'>";
                    htmlContent += "<td style='border:1px solid #000; padding: 8px;'>" + album.AlbumTitle + "</td>";
                    htmlContent += "<td style='border:1px solid #000; padding: 8px;'>" + album.ArtistName + "</td>";
                    htmlContent += "<td style='border:1px solid #000; padding: 8px;'>" + album.Result + "</td>";
                    htmlContent += "</tr>";
                }

                htmlContent += "</tbody></table><br>";
            }

            htmlContent += "</div>";

            var document = new PdfDocument();
            PdfGenerator.AddPdfPages(document, htmlContent, PdfSharpCore.PageSize.A4);

            byte[] response;
            using (MemoryStream ms = new MemoryStream())
            {
                document.Save(ms);
                response = ms.ToArray();
            }

            string fileName = "Album_Report_" + dateNow + ".pdf";
            return new FileContentResult(response, "application/pdf")
            {
                FileDownloadName = fileName
            };
        }
    }
}
