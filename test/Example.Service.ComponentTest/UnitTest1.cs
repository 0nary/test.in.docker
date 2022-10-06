using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Text;

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

    //[Test]
    //public async Task TestInternet()
    //{
    
    //    // Arrange
    //    var httpClient = new HttpClient();
    //    string responseJson = "";
    //    // Act

    //    using (httpClient)
    //    {
    //        using (var response = await httpClient.GetAsync("https://httpbin.org/get"))
    //        {
    //            if (response.IsSuccessStatusCode)
    //            {
    //                responseJson = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
    //            }
    //        }
    //    }



    //    // Assert
    //    Assert.IsNotEmpty(responseJson);
    //}

    [Test]
    public async Task ShouldReturnAListOfStaticValuesOnGetRequest()
    {
        // Arrange
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("http://localhost");
        string responseJson = "";
        // Act

        using (httpClient)
        {
            using (var response = await httpClient.GetAsync("/api/Values"))
            {
                if (response.IsSuccessStatusCode)
                {
                    responseJson = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();
                }
            }
        }



        // Assert
        Assert.That(responseJson, Is.EqualTo("[\"Value1\",\"Value2\",\"Value3\"]"));
    }

}