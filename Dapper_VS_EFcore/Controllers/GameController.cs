using Dapper_VS_EFcore.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Dapper_VS_EFcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IBaseRepository baseRepository;

        public GameController(IBaseRepository baseRepository)
        {
            this.baseRepository = baseRepository;
        }
        [HttpGet]
        public IActionResult GetGameById( [Required]int id=5)
        {
            var game = baseRepository.GetById(id);
            return Ok(game);
        }
    }
}
