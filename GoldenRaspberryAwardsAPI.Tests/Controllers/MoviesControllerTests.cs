using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using GoldenRaspberryAwards.Presentation;

namespace GoldenRaspberryAwardsAPI.Tests.Controllers
{
    public class MoviesControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public MoviesControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetProducersAwards_ReturnsCorrectData()
        {
            // Act
            var response = await _client.GetAsync("/api/movies/producers-awards");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JObject.Parse(content);

            // Assert
            result["max"].Should().NotBeNull();
            result["min"].Should().NotBeNull();
        }
    }
}