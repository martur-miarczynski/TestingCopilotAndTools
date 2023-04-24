using Common.Http.JsonConverters;
using FluentAssertions;
using Xunit;

namespace Common.Http.Tests.JsonConverters
{
    public class UpperSnakeCaseNamingStrategyTests
    {
        [Fact]
        public void GetPropertyName_ConvertNameToUpperSnakeCase()
        {
            // Arrange
            var expectedValue = "SOME_VALUE";
            var target = new UpperSnakeCaseNamingStrategy();

            // Act
            var actual = target.GetPropertyName("someValue", false);

            // Assert
            actual.Should().Be(expectedValue);
        }
    }
}
