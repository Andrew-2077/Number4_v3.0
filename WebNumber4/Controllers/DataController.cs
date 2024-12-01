using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace WebNumber4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private const string FilePath = @"data.txt";

        [HttpGet]
        public async Task<IActionResult> GetData()
        {
            if (System.IO.File.Exists(FilePath))
            {
                var data = await System.IO.File.ReadAllTextAsync(FilePath);
                return Ok(data);
            }
            return NotFound("Файл данных не найден.");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateData(string newData)
        {
            if (string.IsNullOrEmpty(newData))
            {
                return BadRequest("Данные не могут быть пустыми.");
            }

            await System.IO.File.AppendAllTextAsync(FilePath, newData + "\n");

            // Задержка на 30 секунд перед отправкой сообщения
            await Task.Delay(30000);

            return Ok($"Данные обновлены: {newData}. Сообщение отправлено.");
        }
    }
}
