using Example.Service.Services;
using FluentAssertions;

namespace Example.Service.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void ShouldReturnAListOfStaticValues()
        {
            // Arrange
            var unitUnderTest = new ValuesService();

            // Act
            var values = unitUnderTest.GetValues();

            // Assert
            values.Should().NotBeNull();
            values.Should().ContainInOrder("Value1", "Value2", "Value3");
        }
    }
}