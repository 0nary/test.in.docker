using Example.Service.LiveTests.Containers;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace Example.Service.LiveTests
{
    public class UnitTest1 : IClassFixture<ExampleServiceContainer>
    {
        private readonly ExampleServiceContainer _exampleServiceContainer;

        public UnitTest1(ExampleServiceContainer exampleServiceContainer)
        {
            _exampleServiceContainer = exampleServiceContainer;
            _exampleServiceContainer.SetBaseAddress();
        }

        [Fact]
        public async Task ShouldReturnAListOfStaticValuesOnGetRequest()
        {
            // Arrange
            string path = "api/Values";
            // Act

            var response = await _exampleServiceContainer.GetAsync(path)
                .ConfigureAwait(false);

            var weatherForecastStream = await response.Content.ReadAsStreamAsync()
              .ConfigureAwait(false);

            var responseJson = await JsonSerializer.DeserializeAsync<IEnumerable<string>>(weatherForecastStream)
              .ConfigureAwait(false);



            // Assert
            response.Should().BeSuccessful();
            responseJson.Should().NotBeNull();
            responseJson.Should().ContainInOrder("[\"Value1\",\"Value2\",\"Value3\"]");
        }
    }
}