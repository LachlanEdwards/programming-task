using System.Net;
using System.Text.RegularExpressions;
using Mantel.Http.Analyser.Exception;
using Mantel.Http.Analyser.Extension;

namespace Mantel.Http.Analyser.Model;

public record Log (
    string IpAddress,
    string Rfc931,
    string AuthUser,
    DateTimeOffset DateTime,
    string Method,
    string Uri,
    string Protocol,
    HttpStatusCode StatusCode,
    int Size,
    string UserAgent)
{
    /// <summary>
    /// Parses a log entry in Common Log Format and creates a Log object.
    /// </summary>
    /// <param name="input">The log entry string to be parsed.</param>
    /// <returns>A Log object representing the parsed log entry.</returns>
    /// <remarks>
    /// The input log entry should conform to the Common Log Format (CLF):
    /// "{ip} {rfc931} {authuser} [{datetime}] "{method} {uri} {protocol}" {status} {size} "{userAgent}"
    /// </remarks>
    public static Log Parse(string input)
    {
        Match match = new Regex(_pattern, RegexOptions.Compiled).Match(input);
        return new Log(
            IpAddress: match.GetValue(nameof(IpAddress)),
            Rfc931: match.GetValue(nameof(Rfc931)),
            AuthUser: match.GetValue(nameof(AuthUser)),
            DateTime: match.GetDateTimeOffsetValue(nameof(DateTime)),
            Method: match.GetValue(nameof(Method)),
            Uri: match.GetValue(nameof(Uri)),
            Protocol: match.GetValue(nameof(Protocol)),
            StatusCode: match.GetEnumValue<HttpStatusCode>(nameof(StatusCode)),
            Size: match.GetInt32Value(nameof(Size)),
            UserAgent: match.GetValue(nameof(UserAgent)));
    }

    /// <summary>
    /// Regular Expression Pattern to retrieve named Capture Group/s from a CLF string.
    /// </summary>
    private const string _pattern =
        @"(?<IpAddress>[\d\.]+)\s(?<Rfc931>[^\s]+)\s(?<AuthUser>[^\s]+)\s\[(?<DateTime>[\d\w\/\s\:\+]+)\]\s\""(?<Method>[\w]+)\s(?<Uri>[^\s]+)\s(?<Protocol>[\d\w\/\.]+)\""\s(?<StatusCode>\d{3})\s(?<Size>\d+)[\s\""\-]+(?<UserAgent>.+)\""";
}