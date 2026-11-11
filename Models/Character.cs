using System.Text.Json;
using System.Text.Json.Serialization;

namespace SearchNarutoAPI.Models;

public class CharactersResponse
{
    [JsonPropertyName("characters")] public List<Character> Characters { get; set; } = new();
    [JsonPropertyName("currentPage")] public int CurrentPage { get; set; }
    [JsonPropertyName("pageSize")] public int PageSize { get; set; }
    [JsonPropertyName("total")] public int Total { get; set; }
}

public class Character
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("images")] public List<string> Images { get; set; } = new();
    [JsonPropertyName("jutsu")] public List<string> Jutsu { get; set; } = new();
    [JsonPropertyName("personal")] public JsonElement? Personal { get; set; }
    [JsonPropertyName("rank")] public JsonElement? Rank { get; set; }

    public string? FirstImage => Images.FirstOrDefault();

    // personal.clan ou personal.affiliation
    public string Clan => Describe(Personal, "clan") is { Length: > 0 } c
        ? c
        : Describe(Personal, "affiliation") is { Length: > 0 } a ? a : "Não informado";

    // rank.ninjaRank (pode ser string ou objeto por parte: "Part I": "Genin")
    public string NinjaRank => Describe(Rank, "ninjaRank") is { Length: > 0 } r ? r : "Não informado";

    private static string Describe(JsonElement? parent, string property)
    {
        if (parent is not { ValueKind: JsonValueKind.Object } obj
            || !obj.TryGetProperty(property, out var value))
            return "";
        return Flatten(value);
    }

    private static string Flatten(JsonElement e) => e.ValueKind switch
    {
        JsonValueKind.String => e.GetString() ?? "",
        JsonValueKind.Array => string.Join(", ", e.EnumerateArray().Select(Flatten).Where(s => s.Length > 0)),
        JsonValueKind.Object => string.Join("; ", e.EnumerateObject().Select(p => $"{p.Name}: {Flatten(p.Value)}")),
        JsonValueKind.Number or JsonValueKind.True or JsonValueKind.False => e.ToString(),
        _ => ""
    };
}
