using GoldenRaspberryAwards.Domain.Entities;
using GoldenRaspberryAwards.Domain.Interfaces;
using GoldenRaspberryAwards.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GoldenRaspberryAwards.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly DataContext _context;
        private readonly DbContextOptions<DataContext> _options;

        public MovieRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovieEntity>> GetAllAsync()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task AddRangeAsync(IEnumerable<MovieEntity> movies)
        {
            _context.Movies.AddRange(movies);
            _context.SaveChanges();
        }
    }

}
