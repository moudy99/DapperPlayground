using DapperMinimalApi.Repository.Models;

namespace DapperMinimalApi.Repository
{
    public interface IGameStoreRepository
    {
        int GetGamesCount();
        dynamic GetAllGames();
        dynamic getById(int id);
        dynamic GetSummary();
    }
}
