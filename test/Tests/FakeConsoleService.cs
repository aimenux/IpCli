using System.Diagnostics;
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

    public void RenderUserSecretsFile(string filepath)
    {
    }

    public void RenderException(Exception exception)
    {
        Debug.WriteLine(exception);
    }

    public async Task RenderStatusAsync(Func<Task> action)
    {
        await action.Invoke();
    }

    public async Task<T> RenderStatusAsync<T>(Func<Task<T>> func)
    {
        return await func.Invoke();
    }

    public bool GetYesOrNoAnswer(string text, bool defaultAnswer)
    {
        return true;
    }

    public void RenderValidationErrors(ValidationErrors validationErrors)
    {
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