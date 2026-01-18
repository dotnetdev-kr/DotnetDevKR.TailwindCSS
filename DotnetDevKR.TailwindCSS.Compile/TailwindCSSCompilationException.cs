using System;

namespace DotnetDevKR.TailwindCSS;

/// <summary>
/// Exception thrown when TailwindCSS compilation fails.
/// </summary>
public class TailwindCSSCompilationException : Exception
{
    /// <summary>
    /// Gets the compiler output (stderr) if available.
    /// </summary>
    public string? CompilerOutput { get; }

    /// <summary>
    /// Gets the exit code of the TailwindCSS process if available.
    /// </summary>
    public int? ExitCode { get; }

    /// <summary>
    /// Creates a new TailwindCSSCompilationException.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="compilerOutput">The compiler stderr output.</param>
    /// <param name="exitCode">The process exit code.</param>
    public TailwindCSSCompilationException(string message, string? compilerOutput = null, int? exitCode = null)
        : base(message)
    {
        CompilerOutput = compilerOutput;
        ExitCode = exitCode;
    }

    /// <summary>
    /// Creates a new TailwindCSSCompilationException with an inner exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception.</param>
    public TailwindCSSCompilationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
