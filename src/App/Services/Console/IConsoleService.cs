using App.Services.Ip;
using App.Validators;
using Spectre.Console;

namespace App.Services.Console;

public interface IConsoleService
{
    void RenderTitle(string text);
    void RenderVersion(string version);
    void RenderText(string text, Color color);
    void RenderSettingsFile(string filePath);
    void RenderException(Exception exception);
    Task RenderStatusAsync(Func<Task> action);
    Task<T> RenderStatusAsync<T>(Func<Task<T>> func);
    void RenderValidationErrors(ValidationErrors validationErrors);
    Task CopyTextToClipboardAsync(string text, CancellationToken cancellationToken);
    void RenderPrivateIps(ICollection<PrivateIp> privateIps);
    void RenderPublicIps(ICollection<PublicIp> publicIps);
}