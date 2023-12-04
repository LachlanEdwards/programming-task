namespace Mantel.Http.Analyser.Model;

public record Popularity<T>(
    T Value,
    int Count);