using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using System.Text.Json;
using App.Configuration;
using App.Services.Ip;
using App.Validators;
using Spectre.Console;
using TextCopy;

namespace App.Services.Console;

[ExcludeFromCodeCoverage]
public class ConsoleService : IConsoleService
{
    public ConsoleService()
    {
        System.Console.OutputEncoding = Encoding.UTF8;
    }

    public void RenderTitle(string text)
    {
        AnsiConsole.WriteLine();
        AnsiConsole.Write(new FigletText(text));
        AnsiConsole.WriteLine();
    }

    public void RenderVersion(string version)
    {
        var text = $"{Settings.Cli.FriendlyName} V{version}";
        RenderText(text, Color.White);
    }

    public void RenderText(string text, Color color)
    {
        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Markup($"[bold {color}]{text}[/]"));
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine();
    }

    public void RenderSettingsFile(string filePath)
    {
        var name = Path.GetFileName(filePath);
        var json = File.ReadAllText(filePath);
        var formattedJson = GetFormattedJson(json);
        var header = new Rule($"[yellow]({name})[/]");
        header.Centered();
        var footer = new Rule($"[yellow]({filePath})[/]");
        footer.Centered();

        AnsiConsole.WriteLine();
        AnsiConsole.Write(header);
        AnsiConsole.WriteLine(formattedJson);
        AnsiConsole.Write(footer);
        AnsiConsole.WriteLine();
    }

    public void RenderUserSecretsFile(string filepath)
    {
        if (!OperatingSystem.IsWindows()) return;
        if (!File.Exists(filepath)) return;
        if (!GetYesOrNoAnswer("display user secrets", false)) return;
        RenderSettingsFile(filepath);
    }
    
    public void RenderException(Exception exception) => RenderAnyException(exception);

    public static void RenderAnyException<T>(T exception) where T : Exception
    {
        const ExceptionFormats formats = ExceptionFormats.ShortenTypes
                                         | ExceptionFormats.ShortenPaths
                                         | ExceptionFormats.ShortenMethods;

        AnsiConsole.WriteLine();
        AnsiConsole.WriteException(exception, formats);
        AnsiConsole.WriteLine();
    }

    public async Task RenderStatusAsync(Func<Task> action)
    {
        var spinner = RandomSpinner();

        await AnsiConsole.Status()
            .StartAsync("Work is in progress ...", async ctx =>
            {
                ctx.Spinner(spinner);
                await action.Invoke();
            });
    }

    public async Task<T> RenderStatusAsync<T>(Func<Task<T>> func)
    {
        var spinner = RandomSpinner();

        return await AnsiConsole.Status()
            .StartAsync("Work is in progress ...", async ctx =>
            {
                ctx.Spinner(spinner);
                return await func.Invoke();
            });
    }
    
    public bool GetYesOrNoAnswer(string text, bool defaultAnswer)
    {
        if (AnsiConsole.Confirm($"Do you want to [u]{text}[/] ?", defaultAnswer)) return true;
        AnsiConsole.WriteLine();
        return false;
    }

    public void RenderValidationErrors(ValidationErrors validationErrors)
    {
        var count = validationErrors.Count;

        var table = new Table()
            .BorderColor(Color.White)
            .Border(TableBorder.Square)
            .Title($"[red][bold]{count} error(s)[/][/]")
            .AddColumn(new TableColumn("[u]Name[/]").Centered())
            .AddColumn(new TableColumn("[u]Message[/]").Centered())
            .Caption("[grey][bold]Invalid options/arguments[/][/]");

        foreach (var error in validationErrors)
        {
            var failure = error.Failure;
            var name = $"[bold]{error.OptionName()}[/]";
            var reason = $"[tan]{failure.ErrorMessage}[/]";
            table.AddRow(ToMarkup(name), ToMarkup(reason));
        }

        AnsiConsole.WriteLine();
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
    }

    public async Task CopyTextToClipboardAsync(string text, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(text))
        {
            await ClipboardService.SetTextAsync(text, cancellationToken);            
        }
    }

    public void RenderPrivateIps(ICollection<PrivateIp> privateIps)
    {
        var table = new Table()
            .BorderColor(Color.White)
            .Border(TableBorder.Square)
            .Title( "[yellow][bold]Private ip(s)[/][/]")
            .AddColumn(new TableColumn("[u]NetworkName[/]").Centered())
            .AddColumn(new TableColumn("[u]NetworkType[/]").Centered())
            .AddColumn(new TableColumn("[u]NetworkStatus[/]").Centered())
            .AddColumn(new TableColumn("[u]IpV4[/]").Centered())
            .AddColumn(new TableColumn("[u]IpV6[/]").Centered());

        foreach (var privateIp in privateIps.OrderBy(x => x.NetworkName))
        {
            table.AddRow(
                ToMarkup(privateIp.NetworkName),
                ToMarkup(privateIp.NetworkType),
                ToMarkup(privateIp.NetworkStatus),
                ToMarkup(privateIp.IpV4 ?? Emoji.Known.CrossMark),
                ToMarkup(privateIp.IpV6 ?? Emoji.Known.CrossMark));
        }

        AnsiConsole.WriteLine();
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
    }

    public void RenderPublicIps(ICollection<PublicIp> publicIps)
    {
        var table = new Table()
            .BorderColor(Color.White)
            .Border(TableBorder.Square)
            .Title( "[yellow][bold]Public ip(s)[/][/]")
            .AddColumn(new TableColumn("[u]SourceUrl[/]").Centered())
            .AddColumn(new TableColumn("[u]IpV4[/]").Centered());

        foreach (var publicIp in publicIps)
        {
            table.AddRow(
                ToMarkup(publicIp.SourceUrl),
                ToMarkup(publicIp.IpV4 ?? Emoji.Known.CrossMark));
        }

        AnsiConsole.WriteLine();
        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
    }

    private static string GetFormattedJson(string json)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        using var document = JsonDocument.Parse(json);
        return JsonSerializer.Serialize(document, options);
    }

    private static Spinner RandomSpinner()
    {
        var values = typeof(Spinner.Known)
            .GetProperties(BindingFlags.Public | BindingFlags.Static)
            .Where(x => x.PropertyType == typeof(Spinner))
            .Select(x => (Spinner)x.GetValue(null))
            .ToArray();

        var index = Random.Shared.Next(values.Length);
        var value = (Spinner)values.GetValue(index);
        return value;
    }

    private static Markup ToMarkup(string text)
    {
        try
        {
            return new Markup(text ?? string.Empty);
        }
        catch
        {
            return ErrorMarkup;
        }
    }

    private static readonly Markup ErrorMarkup = new(Emoji.Known.CrossMark);
}