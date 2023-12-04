namespace Mantel.Http.Analyser.Exception;

using System;

/// <summary>
/// Exception to be thrown if a value in a Regex Capture Group is invalid.
/// </summary>
public class CaptureException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CaptureException"/> class with information about a malformed value during capture.
    /// </summary>
    /// <param name="type">The type of the value that is malformed.</param>
    /// <param name="captureGroup">The capture group associated with the malformed value.</param>
    public CaptureException(Type type, string captureGroup)
        : base($"Value of type {type.FullName} in {captureGroup} is malformed.")
    {
    }
}
