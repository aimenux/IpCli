using System.Reflection;
using App.Commands;

namespace App.Configuration;

public sealed class Settings
{
    public string[] SourceUrls { get; set; } = { "https://ipinfo.io/ip" };

    public static class ExitCode 
    {
        public const int Ok = 0;
        public const int Ko = -1;
    }
    
    public static class Cli 
    {
        public const string UsageName = @"IpCli";
        public const string FriendlyName = @"IpCli";
        public const string Description = @"A net global tool helping to retrieve ip infos.";
        public static readonly string Version = GetInformationalVersion().Split("+").FirstOrDefault();
        
        private static string GetInformationalVersion() 
        {
            return typeof(ToolCommand)
                .Assembly
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
                .InformationalVersion;
        }
    }
}