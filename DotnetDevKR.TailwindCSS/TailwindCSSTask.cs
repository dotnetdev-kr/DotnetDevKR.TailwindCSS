using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.IO;


namespace DotnetDevKR.TailwindCSS;

public class TailwindCSSTask : Microsoft.Build.Utilities.Task
{
    public string? InputFilename { get; set; }
    [Required]
    public string? OutputFilename { get; set; }
    public string? ProjectDir { get; set; }
    [Required]
    public string MSBuildThisFileDirectory { get; set; }
    public bool IsMinify { get; set; } = false;
    public bool DebugMode { get; set; } = false;
    public bool DotNetWatch { get; set; } = false;

    public override bool Execute()
    {
        Log.LogWarning($"Compiling Tailwind CSS from '{InputFilename}' to '{OutputFilename}' in project directory '{ProjectDir}'.");
        if (OutputFilename is not null && !Directory.Exists(Path.GetDirectoryName(this.OutputFilename)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(this.OutputFilename)!);
        }

        var compiler = new TailwindCSSCompiler();
        compiler.CompileAsync(
            MSBuildThisFileDirectory,
            InputFilename,
            OutputFilename,
            ProjectDir,
            IsMinify,
            DebugMode,
            DotNetWatch
        ).GetAwaiter().GetResult();

        return !Log.HasLoggedErrors;
    }
}