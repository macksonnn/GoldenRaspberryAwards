using CsvHelper;
using CsvHelper.Configuration;
using GoldenRaspberryAwards.Application.DTOs;
using GoldenRaspberryAwards.Domain.Entities;
using GoldenRaspberryAwards.Domain.Interfaces;
using System.Globalization;

namespace GoldenRaspberryAwards.Application.Services
{
    public class CsvService
    {
        private readonly IMovieRepository _movieRepository;

        public CsvService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task ImportMoviesAsync(string csvFilePath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ";",
                HeaderValidated = null,
                PrepareHeaderForMatch = args => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(args.Header.ToLower()).Replace(" ", string.Empty)
            };

            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<MovieDTO>().ToList();
                var entities = ParseToEntity(records);
                await _movieRepository.AddRangeAsync(entities);
            }
        }

        //public async Task ImportMoviesAsync(string csvFilePath)
        //{
        //    using (var stream = new FileStream(csvFilePath, FileMode.Open, FileAccess.Read))
        //    {
        //        await ImportMoviesFromStreamAsync(stream);
        //    }
        //}
        //public async Task ImportMoviesFromStreamAsync(Stream csvStream)
        //{
        //    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        //    {
        //        HasHeaderRecord = true,
        //        Delimiter = ";",
        //        HeaderValidated = null,
        //        PrepareHeaderForMatch = args => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(args.Header.ToLower()).Replace(" ", string.Empty)
        //    };

        //    using (var reader = new StreamReader(csvStream))
        //    using (var csv = new CsvReader(reader, config))
        //    {
        //        var records = csv.GetRecords<MovieDTO>().ToList();
        //        var entities = ParseToEntity(records);
        //        await _movieRepository.AddRangeAsync(entities); // Await the async call
        //    }


        //}

        private List<MovieEntity> ParseToEntity(List<MovieDTO> dtos)
        {
            return dtos.Select(dto => new MovieEntity
            {
                Year = dto.Year,
                Producers = dto.Producers,
                Studios = dto.Studios,
                Title = dto.Title,
                Winner = dto.Winner.ToUpper() == "YES"
            }).ToList();
        }

    }
}
