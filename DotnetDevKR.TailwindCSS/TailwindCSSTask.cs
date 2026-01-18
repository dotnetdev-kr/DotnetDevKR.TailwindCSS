using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.IO;


namespace DotnetDevKR.TailwindCSS;

public class TailwindCSSTask : Microsoft.Build.Utilities.Task
{
    public string? InputFilename { get; set; }
    [Required]
    public string? OutputFilename { get; set; }
    public string? ProjectDir { get; set; }
    [Required]
    public string MSBuildThisFileDirectory { get; set; } = string.Empty;
    public bool IsMinify { get; set; } = false;
    public bool DebugMode { get; set; } = false;

    public override bool Execute()
    {
        // Validate required properties
        if (string.IsNullOrEmpty(OutputFilename))
        {
            Log.LogError("TAILWIND001", null, null, null, 0, 0, 0, 0,
                "OutputFilename property is required. Add <OutputFilename> to your TailwindCSS configuration.");
            return false;
        }

        // Warn if input file doesn't exist
        if (!string.IsNullOrEmpty(InputFilename) && !File.Exists(InputFilename))
        {
            Log.LogWarning("TAILWIND002", null, null, null, 0, 0, 0, 0,
                $"Input file not found: {InputFilename}. TailwindCSS will scan for utility classes only.");
        }

        // Use LogMessage instead of LogWarning for normal output
        Log.LogMessage(MessageImportance.High,
            $"TailwindCSS: Compiling '{InputFilename ?? "(auto-detect)"}' to '{OutputFilename}'");

        try
        {
            if (OutputFilename is not null && !Directory.Exists(Path.GetDirectoryName(OutputFilename)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(OutputFilename)!);
            }

            var compiler = new TailwindCSSCompiler();
            compiler.CompileAsync(
                MSBuildThisFileDirectory,
                InputFilename,
                OutputFilename,
                ProjectDir,
                IsMinify,
                DebugMode
            ).GetAwaiter().GetResult();
        }
        catch (TailwindCSSCompilationException ex)
        {
            Log.LogError("TAILWIND003", null, null, InputFilename, 0, 0, 0, 0,
                $"TailwindCSS compilation failed: {ex.Message}");
            if (!string.IsNullOrEmpty(ex.CompilerOutput))
            {
                Log.LogMessage(MessageImportance.High, $"Compiler output: {ex.CompilerOutput}");
            }
            return false;
        }
        catch (Exception ex)
        {
            Log.LogError("TAILWIND099", null, null, null, 0, 0, 0, 0,
                $"Unexpected error during TailwindCSS compilation: {ex.Message}");
            return false;
        }

        return true;
    }
}