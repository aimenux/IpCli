[![.NET](https://github.com/aimenux/IpCli/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/aimenux/IpCli/actions/workflows/ci.yml)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=IpCli-Key&metric=coverage)](https://sonarcloud.io/summary/new_code?id=IpCli-Key)
![Nuget](https://img.shields.io/nuget/v/IpCli)

# IpCli
```
A net global tool helping to retrieve ip infos
```

> In this repo, i m building a global tool that allows to retrieve ip infos.
>
> The tool is based on multiple sub commmands :
> - Use sub command `Public` to get public ip infos
> - Use sub command `Private` to get private ip infos

>
> To run the tool, type commands :
> - `IpCli -h` to show help
> - `IpCli -s` to show settings
> - `IpCli Public` to get public ip infos
> - `IpCli Private` to get private ip infos
>
>
> To install global tool from a local source path, type commands :
> - `dotnet tool install -g --configfile .\nugets\local.config IpCli --version "*-*" --ignore-failed-sources`
>
> To install global tool from [nuget source](https://www.nuget.org/packages/IpCli), type these command :
> - For stable version : `dotnet tool install -g IpCli --ignore-failed-sources`
> - For prerelease version : `dotnet tool install -g IpCli --version "*-*" --ignore-failed-sources`
>
> To uninstall global tool, type these command :
> - `dotnet tool uninstall -g IpCli`
>
>

**`Tools`** : vs22, net 6.0/7.0, command-line, spectre-console, polly, fluent-validation, fluent-assertions, xunit
