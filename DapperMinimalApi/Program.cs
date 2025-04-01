using DapperMinimalApi.Repository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DapperMinimalApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<DapperContext>();
            builder.Services.AddScoped<IGameStoreRepository, GameStoreRepository>();
            // Configure MemoryCache with global expiration policy
            builder.Services.AddMemoryCache(options =>
            {
                options.ExpirationScanFrequency = TimeSpan.FromSeconds(1);
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            // Games count
            app.MapGet("/games-count", (IGameStoreRepository gameStoreRepo) =>
            {
                return gameStoreRepo.GetGamesCount();
            });

            // All Games Basic info
            app.MapGet("/gmaes", (IGameStoreRepository gameStore) =>
            {
                return gameStore.GetAllGames();
            });


            // All Games Basic info
            app.MapGet("/get-by-id/{id}", (int id,IGameStoreRepository gameStore) =>
            {
                return gameStore.getById(id);
            });

            // Get Summary 

            app.MapGet("get-summary", (IGameStoreRepository gameStore) =>
            {
                return gameStore.GetSummary();
            });

            // test multiple 

            app.MapGet("get-multiplee/{ID}", (int ID,IGameStoreRepository gameStore) =>
            {
                return gameStore.testMultiple(ID);
            });

            // test Transaction

            app.MapGet("testTransaction", (IGameStoreRepository gameStore) =>
            {
                var result =  gameStore.testTransaction();

                if(result.Success == true)
                {
                    return Results.Ok(result);
                }
                else
                {
                    return Results.BadRequest(result);
                }
            });

            app.MapGet("game-info", (IGameStoreRepository gameStore) =>
            {
                var result = gameStore.GameInfo();
                return result;
            });

            // test one to many 
            app.MapGet("game/screens", (IGameStoreRepository gameStore) =>
            {
                var result = gameStore.gameWIthScreens();
                return result;
            });

            app.Run();
        }
    }
}
