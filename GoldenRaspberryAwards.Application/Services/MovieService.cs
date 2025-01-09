using GoldenRaspberryAwards.Application.DTOs;
using GoldenRaspberryAwards.Application.Interface;
using GoldenRaspberryAwards.Domain.Entities;
using GoldenRaspberryAwards.Domain.Interfaces;
using System.Text.RegularExpressions;

namespace GoldenRaspberryAwards.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<MovieEntity>> GetAllAsync()
        {
            return await _movieRepository.GetAllAsync();
        }

        public async Task<(List<IntervalDTO> min, List<IntervalDTO> max)> GetMinMaxIntervals()
        {
            var awards =  GetAllAsync().Result
                .Where(m => m.Winner)
                .SelectMany(m =>
                    Regex.Split(m.Producers, @"\s*(?:,|and)\s*")
                    .Select(p => new
                    {
                        Producer = p.Trim(),
                        Year = m.Year
                    }))
                .OrderBy(a => a.Producer)
                .ThenBy(a => a.Year)
                .ToList();

     
            var producers = new List<IntervalDTO>();
            var groupedByProducer = awards.GroupBy(a => a.Producer);
            foreach (var group in groupedByProducer)
            {
                var producerAwards = group.OrderBy(a => a.Year).ToList();

                for (int i = 1; i < producerAwards.Count; i++)
                {
                    var interval = producerAwards[i].Year - producerAwards[i - 1].Year;

                    if (interval >= 1)
                    {
                        producers.Add(new IntervalDTO
                        {
                            Producer = group.Key,
                            Interval = interval,
                            PreviousWin = producerAwards[i - 1].Year,
                            FollowingWin = producerAwards[i].Year
                        });
                    }
                }
            }

            var minIntervalValue = producers.Min(i => i.Interval);
            var maxIntervalValue = producers.Max(i => i.Interval);

            var minIntervals = producers
                .Where(min => min.Interval == minIntervalValue)
                .ToList();

            var maxIntervals = producers
                .Where(max => max.Interval == maxIntervalValue)
                .ToList();

            return (minIntervals, maxIntervals);
        }

    }
}
