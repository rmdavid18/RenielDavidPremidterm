using RenielDavidPremidActivity.Infrastructure.Domain.Models;
using RenielDavidPremidActivity.Infrastructure.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using System.Data;
using System.Data.Entity;

namespace RenielDavidPremidActivity.Pages.Manage.Videos
{
    public class VideoIndexModel : PageModel
    {
        private ILogger<Index> _logger;
        private DefaultDbContext _context;

        [BindProperty]
        public ViewModel View { get; set; }

        public VideoIndexModel(DefaultDbContext context, ILogger<Index> logger)
        {
            _logger = logger;
            _context = context;
            View ??= new ViewModel();
        }
        public void OnGet(List<Video> Video, int? pageIndex = 1, int? pageSize = 10, string? sortBy = "", SortOrder sortOrder = SortOrder.Ascending, string? keyword = "")
        {
            var skip = (int)((pageIndex - 1) * pageSize);


            var query = (IQueryable<Video>)_context.Videos;

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(a =>
                            a.Title != null && a.Title.ToLower().Contains(keyword.ToLower())
                        || a.Description != null && a.Description.ToLower().Contains(keyword.ToLower())
                        

                );
            }

            var totalRows = query.Count();

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.ToLower() == "name" && sortOrder == SortOrder.Ascending)
                {
                    query = query.OrderBy(a => a.Title);
                }
                else if (sortBy.ToLower() == "name" && sortOrder == SortOrder.Descending)
                {
                    query = query.OrderByDescending(a => a.Title);
                }
                else if (sortBy.ToLower() == "description" && sortOrder == SortOrder.Ascending)
                {
                    query = query.OrderBy(a => a.Description);
                }
                else if (sortBy.ToLower() == "description" && sortOrder == SortOrder.Descending)
                {
                    query = query.OrderByDescending(a => a.Description);
                }
                else if (sortBy.ToLower() == "abbreviation" && sortOrder == SortOrder.Ascending)
                {
                    query = _context.Videos.OrderBy(a => a.DateOfPublished);
                }
                else if (sortBy.ToLower() == "abbreviation" && sortOrder == SortOrder.Descending)
                {
                    query = query.OrderByDescending(a => a.DateOfPublished);
                }
            }


            var Videos =      query
                            .Skip(skip)
                            .Take((int)pageSize)
                               .ToList();

            View.Videos = new Paged<Video>()
            {
                Items = Video,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRows = totalRows,
                SortBy = sortBy,
                SortOrder = sortOrder
            };

        }

        public class ViewModel
        {
            public Paged<Video>? Videos { get; set; }
        }
    }
}











