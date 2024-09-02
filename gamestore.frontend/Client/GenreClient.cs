using GameStore.Frontend.Models;

namespace GameStore.Frontend.Client;

public class GenreClient(HttpClient httpClient)
{
    public async Task<Genre[]> GetGenresAsync() => await httpClient.GetFromJsonAsync<Genre[]>("genres")
                                              ?? throw new Exception("Genres not found");
}