

using GoldenRaspberryAwards.Domain.Entities;

namespace GoldenRaspberryAwards.Domain.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<MovieEntity>> GetAllAsync();

        Task AddRangeAsync(IEnumerable<MovieEntity> movies);
    }
}
