using DapperMinimalApi.Models;

namespace DapperMinimalApi.Repository
{
    public interface IGameStoreRepository
    {
        int GetGamesCount();
        dynamic GetAllGames();
        dynamic getById(int id);
        dynamic GetSummary();
        dynamic testMultiple(int IDS);
        dynamic testTransaction();
        List<Game> GameInfo();
        List<Game> gameWIthScreens();
    }
}
