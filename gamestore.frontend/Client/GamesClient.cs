using GameStore.Frontend.Models;

namespace GameStore.Frontend.Client;

public class GamesClient(HttpClient httpClient)
{
    public async Task<GameSummary[]> GetGamesAsync() =>
        await httpClient.GetFromJsonAsync<GameSummary[]>("/games") ?? [];

    public async Task AddGameAsync(GameDetails game)
    {
        await httpClient.PostAsJsonAsync("games", game);
    }

    public async Task UpdateGameAsync(GameDetails gameDetails)
    {
        await httpClient.PutAsJsonAsync($"games/{gameDetails.Id}", gameDetails);
    }

    public async Task<GameDetails> GetGameAsync(int id)
    {
        return await httpClient.GetFromJsonAsync<GameDetails>($"games/{id}") ?? throw new Exception("Game not found");
    }

    public async Task DeleteGame(int id)
    {
        await httpClient.DeleteAsync($"games/{id}");
    }

}