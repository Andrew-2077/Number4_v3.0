using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace WebNumber4_2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DataController(AppDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated(); // Создает БД, если она не существует
        }

        [HttpPost]
        public async Task<IActionResult> UpdateData([FromBody] DataEntry entry)
        {
            if (entry == null || string.IsNullOrWhiteSpace(entry.Value))
            {
                return BadRequest("Некорректные данные.");
            }

            // Сохранение в БД
            await _context.DataEntries.AddAsync(entry);
            await _context.SaveChangesAsync();

            // Задержка перед отправкой сообщения
            await Task.Delay(30000);

            // Здесь можно добавить логику отправки сообщения во внешний мир
            Console.WriteLine($"Сообщение отправлено: {entry.Value}");

            return Ok($"Данные обновлены: {entry.Value}");
        }
    }
}
