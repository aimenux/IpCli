using App.Extensions;
using FluentAssertions;

namespace Tests.Extensions;

public class StringExtensionsTests
{
    [Theory]
    [InlineData("", "")]
    [InlineData(null, null)]
    [InlineData("abc", "abc")]
    [InlineData("abc", "Abc")]
    [InlineData("abc", "aBc")]
    [InlineData("abc", "abC")]
    [InlineData("abc", "ABC")]
    public void Should_Be_Equals(string left, string right)
    {
        // arrange
        // act
        var areEquals = left.IgnoreEquals(right);

        // assert
        areEquals.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("", null)]
    [InlineData(null, "")]
    [InlineData("abc", "âbc")]
    [InlineData("edf", "èdf")]
    [InlineData("uvw", "ùvw")]
    public void Should_Not_Be_Equals(string left, string right)
    {
        // arrange
        // act
        var areEquals = left.IgnoreEquals(right);

        // assert
        areEquals.Should().BeFalse();
    }
    
    [Theory]
    [InlineData("127.0.0.1")]
    [InlineData("102.25.511.52")]
    [InlineData("202.22.452.561")]
    public void Should_Be_Valid_IpV4(string ipV4)
    {
        // arrange
        // act
        var ok = ipV4.IsValidIpV4();

        // assert
        ok.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("127")]
    [InlineData("127.0.0")]
    public void Should_Not_Be_Valid_IpV4(string ipV4)
    {
        // arrange
        // act
        var ok = ipV4.IsValidIpV4();

        // assert
        ok.Should().BeFalse();
    }
}
