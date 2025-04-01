using Dapper_VS_EFcore.DTOs;
using Dapper_VS_EFcore.Models;

namespace Dapper_VS_EFcore.Repository
{
    public interface IBaseRepository
    {
        GameDto GetById(int id);
    }
}
