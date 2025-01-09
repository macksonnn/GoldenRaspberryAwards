using GoldenRaspberryAwards.Application.Interface;
using GoldenRaspberryAwards.Application.Services;
using GoldenRaspberryAwards.Domain.Interfaces;
using GoldenRaspberryAwards.Infrastructure.Data;
using GoldenRaspberryAwards.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoldenRaspberryAwards.Presentation
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            if (_env.IsEnvironment("Testing"))
            {
                services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("MoviesTestDb"));
            }
            else
            {
                services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("GoldenRaspberryAwardsDb"));
            }

            services.AddScoped<CsvService>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IMovieService, MovieService>();

            services.AddControllers();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CsvService csvService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Golden Raspberry Awards API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //if (_env.IsEnvironment("Testing"))
            //{
            //    Task.Run(async () => await csvService.ImportMoviesAsync("../../Files/movies.csv")).Wait();
            //}
            //else
            //{
            //    Task.Run(async () => await csvService.ImportMoviesAsync("Files/movies.csv")).Wait();
            //}
            ImportMovies(csvService).Wait();

        }

        private async Task ImportMovies(CsvService csvService)
        {
            var filePath = _env.IsEnvironment("Testing") ? "../../../Files/movies.csv" : "Files/movies.csv";
            await csvService.ImportMoviesAsync(filePath);
        }

    }
}
