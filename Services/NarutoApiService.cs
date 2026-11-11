using System.Net.Http.Json;
using SearchNarutoAPI.Models;

namespace SearchNarutoAPI.Services;

public class NarutoApiService(HttpClient http)
{
    public async Task<CharactersResponse> GetCharactersAsync(string? name, int page, int limit)
    {
        var url = $"characters?page={page}&limit={limit}";
        if (!string.IsNullOrWhiteSpace(name))
            url += $"&name={Uri.EscapeDataString(name.Trim())}";

        return await http.GetFromJsonAsync<CharactersResponse>(url) ?? new CharactersResponse();
    }

    public Task<Character?> GetCharacterAsync(int id) => http.GetFromJsonAsync<Character>($"characters/{id}");
}
