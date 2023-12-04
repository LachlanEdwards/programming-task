using System.Globalization;
using System.Text.RegularExpressions;
using Mantel.Http.Analyser.Exception;

namespace Mantel.Http.Analyser.Extension;

/// <summary>
/// Enum Extensions.
/// </summary>
public static class EnumExtension
{
    /// <summary>
    /// Date/Time Format for strftime.
    /// </summary>
    private const string DateTimeFormat = @"dd/MMM/yyyy:HH:mm:ss zzz";

    /// <summary>
    /// Gets the enum value of type <typeparamref name="T"/> from the specified capture group in a regular expression match.
    /// </summary>
    /// <typeparam name="T">The enum type.</typeparam>
    /// <param name="match">The regular expression match containing capture groups.</param>
    /// <param name="captureGroupName">The name of the capture group containing the enum value.</param>
    /// <returns>The enum value of type <typeparamref name="T"/>.</returns>
    /// <exception cref="CaptureException">
    /// Thrown when the value in the specified capture group is malformed or cannot be converted to an enum.
    /// </exception>
    /// <remarks>
    /// This method assumes that the value in the specified capture group is a valid enum representation.
    /// </remarks>
    public static T GetEnumValue<T>(this Match match, string captureGroupName) where T : struct, Enum =>
        Enum.TryParse(match.Groups[captureGroupName].Value, out T result)
            ? result
            : throw new CaptureException(typeof(T), captureGroupName);


    /// <summary>
    /// Gets the integer value from the specified capture group in a regular expression match.
    /// </summary>
    /// <param name="match">The regular expression match containing capture groups.</param>
    /// <param name="captureGroupName">The name of the capture group containing the integer value.</param>
    /// <returns>The integer value.</returns>
    /// <exception cref="CaptureException">
    /// Thrown when the value in the specified capture group is malformed or cannot be converted to a 32-bit signed integer.
    /// </exception>
    /// <remarks>
    /// This method assumes that the value in the specified capture group is a valid integer representation.
    /// </remarks>
    public static int GetInt32Value(this Match match, string captureGroupName) =>
        Int32.TryParse(match.Groups[captureGroupName].Value, out int result)
            ? result
            : throw new CaptureException(typeof(int), captureGroupName);

    /// <summary>
    /// Gets the DateTimeOffset value from the specified capture group in a regular expression match.
    /// </summary>
    /// <param name="match">The regular expression match containing capture groups.</param>
    /// <param name="captureGroupName">The name of the capture group containing the DateTimeOffset value.</param>
    /// <returns>The DateTimeOffset value.</returns>
    /// <exception cref="CaptureException">
    /// Thrown when the value in the specified capture group is malformed or cannot be converted to a DateTimeOffset.
    /// </exception>
    /// <remarks>
    /// This method assumes that the value in the specified capture group is in the format "dd/MMM/yyyy:HH:mm:ss zzz".
    /// </remarks>
    public static DateTimeOffset GetDateTimeOffsetValue(this Match match, string captureGroupName) =>
        DateTimeOffset.TryParseExact(match.Groups[captureGroupName].Value, DateTimeFormat, CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces, out DateTimeOffset result)
            ? result
            : throw new CaptureException(typeof(DateTimeOffset), captureGroupName);

    /// <summary>
    /// Gets the string value from the specified capture group in a regular expression match.
    /// </summary>
    /// <param name="match">The regular expression match containing capture groups.</param>
    /// <param name="captureGroupName">The name of the capture group containing the string value.</param>
    /// <returns>The string value.</returns>
    /// <exception cref="CaptureException">
    /// Thrown when the value in the specified capture group is malformed or cannot be converted to a string.
    /// </exception>
    public static string GetValue(this Match match, string captureGroupName) =>
        !string.IsNullOrEmpty(match.Groups[captureGroupName].Value)
            ? match.Groups[captureGroupName].Value
            : throw new CaptureException(typeof(string), captureGroupName);
}