using Dapper_VS_EFcore.Context;
using Dapper_VS_EFcore.Repository;
using Microsoft.EntityFrameworkCore;

namespace Dapper_VS_EFcore
{
    public static class DependencyInjection
    {
        public static IServiceCollection DependencyServices (this IServiceCollection Services,ConfigurationManager configuration)
        {

            // i can solve the issue of the circler reference by make the json serializer ignore the loop reference
            // but this will add $id and $ref to the json response so he can ignoire the obj
            #region Json Serializer to resolve the circler issue 
            /*
               builder.Services.AddControllers()
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    opt.JsonSerializerOptions.MaxDepth = 64;
                });
             */
            #endregion
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();
            Services.AddDbContext<EFcoreDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(EFcoreDbContext).Assembly.FullName));
            });


            #region For Ef core ->
           // Services.AddScoped<IBaseRepository, EfCoreRepository>();
            //**** here in the get Game by iD , the best time it return in 147ms.
            #endregion

            #region For Dapper->
            Services.AddScoped<IBaseRepository, DapperRepository>();
            //**** here in the get Game by iD , the best time it return in 45ms.

            #endregion
            Services.AddSingleton<DapperDBContext>();

            return Services;
        }
    }
}
