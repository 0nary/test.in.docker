using Microsoft.Extensions.Configuration;

namespace Example.Service.ComponentTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }

    [Test]
    public async Task ShouldReturnAListOfStaticValuesOnGetRequest()
    {
        // Arrange
        var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5000") };

        // Act
        var response = await httpClient.GetAsync("/api/values");
        var responseJson = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

        // Assert
        Assert.That(responseJson, Is.EqualTo("[\"Value1\", \"Value2\", \"Value3\" ]"));
    }

}