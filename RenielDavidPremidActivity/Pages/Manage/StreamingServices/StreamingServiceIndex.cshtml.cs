using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RenielDavidPremidActivity.Infrastructure.Domain.Models;
using RenielDavidPremidActivity.Infrastructure.Domain;
using System.Linq;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;


namespace RenielDavidPremidActivity.Pages.Manage.StreamingServices
{
    public class StreamingServiceIndexModel : PageModel
    {
        private ILogger<Index> _logger;
        private DefaultDbContext _context;

        [BindProperty]
        public ViewModel View { get; set; }

        public StreamingServiceIndexModel(DefaultDbContext context, ILogger<Index> logger)
        {
            _logger = logger;
            _context = context;
            View = View ?? new ViewModel();
        }

        public void OnGet(int? pageIndex = 1, int? pageSize = 10, string? sortBy = "", SortOrder sortOrder = SortOrder.Ascending, string? keyword = "")
        {
            var skip = (int)((pageIndex - 1) * pageSize);

            var query = (IQueryable<StreamingService>)_context.StreamingServices;

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(a =>
                            a.Title != null && a.Title.ToLower().Contains(keyword.ToLower())
                        || a.Description != null && a.Description.ToLower().Contains(keyword.ToLower())
                        || a.Abbreviation != null && a.Abbreviation.ToLower().Contains(keyword.ToLower())
                );
            }

            var totalRows = query.Count();

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.ToLower() == "Title" && sortOrder == SortOrder.Ascending)
                {
                    query = query.OrderBy(a => a.Title);
                }
                else if (sortBy.ToLower() == "Title" && sortOrder == SortOrder.Descending)
                {
                    query = query.OrderByDescending(a => a.Title);
                }
                else if (sortBy.ToLower() == "Description" && sortOrder == SortOrder.Ascending)
                {
                    query = query.OrderBy(a => a.Description);
                }
                else if (sortBy.ToLower() == "Description" && sortOrder == SortOrder.Descending)
                {
                    query = query.OrderByDescending(a => a.Description);
                }
                else if (sortBy.ToLower() == "Abbreviation" && sortOrder == SortOrder.Ascending)
                {
                    query = _context.StreamingServices.OrderBy(a => a.Abbreviation);
                }
                else if (sortBy.ToLower() == "Abbreviation" && sortOrder == SortOrder.Descending)
                {
                    query = query.OrderByDescending(a => a.Abbreviation);
                }
            }

            var StreamingServices = query
                            .Skip(skip)
                            .Take((int)pageSize)
                             .ToList();



            View.StreamingServices = new Paged<StreamingService>()
            {
                Items = StreamingServices,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalRows = totalRows,
                SortBy = sortBy,
                SortOrder = sortOrder
            };

        }

        public class ViewModel
        {
            public Paged<StreamingService>? StreamingServices { get; set; }
        }
    }
}

