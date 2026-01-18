using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetDevKR.TailwindCSS;

public class TailwindCSSCompiler
{
    public async Task CompileAsync(
        string MSBuildThisFileDirectory,
        string? InputFilename,
        string? OutputFilename,
        string? ProjectDir,
        bool isMinify = false,
        bool isDebug = false,
        CancellationToken cancellationToken = default
    )
    {
        using var process = CreateTailwindCSSProcess(
            MSBuildThisFileDirectory,
            $"-i {InputFilename} -o {OutputFilename} --cwd {ProjectDir}" +
            (isMinify ? " --minify" : "") +
            (isDebug ? " --map" : "")
        );

        using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeoutCts.CancelAfter(TimeSpan.FromMinutes(5));

        var errorOutput = new MemoryStream();

        try
        {
            process.Start();
            var outputTask = process.StandardOutput.BaseStream.CopyToAsync(Stream.Null, timeoutCts.Token);
            var errorTask = process.StandardError.BaseStream.CopyToAsync(errorOutput, timeoutCts.Token);
            await Task.WhenAll(outputTask, errorTask);
            await process.WaitForExitAsync(timeoutCts.Token);
        }
        catch (OperationCanceledException) when (timeoutCts.IsCancellationRequested && !cancellationToken.IsCancellationRequested)
        {
            try { process.Kill(entireProcessTree: true); } catch { }
            throw new TailwindCSSCompilationException("TailwindCSS compilation timed out after 5 minutes.", exitCode: -1);
        }

        if (process.ExitCode != 0)
        {
            var errorOutputText = Encoding.UTF8.GetString(errorOutput.ToArray());
            throw new TailwindCSSCompilationException(
                $"TailwindCSS process exited with code {process.ExitCode}.",
                compilerOutput: errorOutputText,
                exitCode: process.ExitCode);
        }
    }

    internal static Process CreateTailwindCSSProcess(string MSBuildThisFileDirectory, string arguments)
    {
        Console.WriteLine($"Executing Tailwind CSS with arguments: {arguments}");
        var executablePath = GetExecutablePath();
        if (executablePath is null)
        {
            throw new Exception("Tailwind CSS executable not found for current OS platform and architecture.");
        }
        var process = new Process();
        process.StartInfo.FileName = Path.Combine(MSBuildThisFileDirectory, executablePath);
        process.StartInfo.Arguments = arguments;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();

        return process;
    }
    internal static string? GetExecutablePath()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return RuntimeInformation.OSArchitecture switch
            {
                Architecture.X64 => "runtime\\tailwindcss-windows-x64.exe",
                Architecture.Arm64 => "runtime\\tailwindcss-windows-arm64.exe",
                _ => null,
            };
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return RuntimeInformation.OSArchitecture switch
            {
                Architecture.X64 => "runtime/tailwindcss-linux-x64",
                Architecture.Arm64 => "runtime/tailwindcss-linux-arm64",
                _ => null,
            };
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return RuntimeInformation.OSArchitecture switch
            {
                Architecture.X64 => "runtime/tailwindcss-macos-x64",
                Architecture.Arm64 => "runtime/tailwindcss-macos-arm64",
                _ => null,
            };
        }

        return null;
    }
}