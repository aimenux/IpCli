using App.Configuration;
using FluentAssertions;

namespace Tests.Configuration;

public class SettingsTests
{
    [Fact]
    public void Should_Get_SourceUrls()
    {
        // arrange
        var settings = new Settings();
        
        // act
        var sourceUrls = settings.SourceUrls;

        // assert
        sourceUrls.Should().NotBeNullOrEmpty();
    }
    
    [Fact]
    public void Should_Get_ExitCodes()
    {
        // arrange
        // act
        const int ok = Settings.ExitCode.Ok;
        const int ko = Settings.ExitCode.Ko;

        // assert
        ok.Should().Be(0);
        ko.Should().Be(-1);
    }
    
    [Fact]
    public void Should_Get_Version()
    {
        // arrange
        // act
        var version = Settings.Cli.Version;

        // assert
        version.Should().NotBeNullOrWhiteSpace();
    }
}