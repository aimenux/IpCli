using App.Services.Console;
using App.Services.Ip;
using App.Validators;
using Spectre.Console;

namespace Tests;

public class FakeConsoleService : IConsoleService
{
    public void RenderTitle(string text)
    {
    }

    public void RenderVersion(string version)
    {
    }

    public void RenderText(string text, Color color)
    {
    }

    public void RenderSettingsFile(string filePath)
    {
    }

    public void RenderException(Exception exception)
    {
    }

    public Task RenderStatusAsync(Func<Task> action)
    {
        return Task.CompletedTask;
    }

    public Task<T> RenderStatusAsync<T>(Func<Task<T>> func)
    {
        return Task.FromResult(default(T));
    }

    public void RenderValidationErrors(ValidationErrors validationErrors)
    {
        throw new NotImplementedException();
    }

    public Task CopyTextToClipboardAsync(string text, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public void RenderPrivateIps(ICollection<PrivateIp> privateIps)
    {
    }

    public void RenderPublicIps(ICollection<PublicIp> publicIps)
    {
    }
}