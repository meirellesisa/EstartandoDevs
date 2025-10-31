using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EstartandoDevs.Infrastructure.Context;

namespace EstartandoDevs.API.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HealthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                // Verifica se consegue conectar ao banco de dados
                var canConnect = await _context.Database.CanConnectAsync();
                
                if (!canConnect)
                {
                    return StatusCode(503, new { status = "unhealthy", message = "Database connection failed" });
                }

                return Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
            }
            catch (Exception ex)
            {
                return StatusCode(503, new { status = "unhealthy", message = ex.Message });
            }
        }
    }
}

