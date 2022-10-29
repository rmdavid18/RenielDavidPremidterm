using RenielDavidPremidActivity.Infrastructure.Domain.Models;
using Microsoft.EntityFrameworkCore;

using System.Data;
using System.Data.Entity;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace RenielDavidPremidActivity.Infrastructure.Domain
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
          : base(options)
        {
        }
        public Microsoft.EntityFrameworkCore.DbSet<Video> Videos { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<StreamingService> StreamingServices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Video> Videos = new List<Video>();

            Videos.Add(new Video()
            {
                Id = Guid.Parse("00965ecf-acae-46fe-8775-d7834b07fd96"),
                Title = "SpiderMan",
                Description = "People who work in school but not teaching",
                DateOfPublished = DateTime.Parse("1984-3-13"),
                Type = Enums.Type.Movie,
            });

            Videos.Add(new Video()
            {
                Id = Guid.Parse("00965ecf-acae-46fe-8775-d7834b07fd97"),
                Title = "Batman",
                Description = "People who work in school but not teaching",
                DateOfPublished = DateTime.Parse("1984-3-12"),
                Type = Enums.Type.Series,
            });




            List<StreamingService> StreamingServices = new List<StreamingService>();

            StreamingServices.Add(new StreamingService()
            {
                Id = Guid.Parse("00965ecf-acae-46fe-8775-d7834b07fd98"),
                Title = "NetFlix",
                Description = "People who work in school but not teaching",
                Abbreviation = "NFLX"

             
              
            });


            modelBuilder.Entity<Video>().HasData(Videos);
            modelBuilder.Entity<StreamingService>().HasData(StreamingServices);
        }

    }
}




