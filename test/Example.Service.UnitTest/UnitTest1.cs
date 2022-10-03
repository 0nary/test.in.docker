using Example.Service.Services;

namespace Example.Service.UnitTest;

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
    public void ShouldReturnAListOfStaticValues()
    {
        // Arrange
        var unitUnderTest = new ValuesService();

        // Act
        var values = unitUnderTest.GetValues();

        // Assert
        Assert.That(values, Is.EqualTo(new[] { "Value1", "Value2", "Value3" }));
    }
}