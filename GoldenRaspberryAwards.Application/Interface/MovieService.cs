

using GoldenRaspberryAwards.Application.DTOs;
using GoldenRaspberryAwards.Domain.Entities;

namespace GoldenRaspberryAwards.Application.Interface
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieEntity>> GetAllAsync();
        Task<(List<IntervalDTO> min, List<IntervalDTO> max)> GetMinMaxIntervals();
    }
}
