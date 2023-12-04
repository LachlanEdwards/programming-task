namespace Mantel.Http.Analyser.Model;

public record Ranking<T>(
    int Rank,
    Popularity<T> Popularity);