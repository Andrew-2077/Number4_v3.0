using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;
using WebNumber4;

namespace DataStateApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Запуск API
            var host = CreateHostBuilder(args).Build();
            await host.RunAsync();

            // Запуск консольного приложения
            StartConsoleApp();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void StartConsoleApp()
        {
            const string filePath = @"data.txt";

            // Чтение стартового состояния данных из файла
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл состояния данных не найден. Создайте файл {0} с данными.", filePath);
                return;
            }

            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }

            while (true)
            {
                Console.Write("Введите новое значение в формате 'id = value' (для завершения ввода данных введите 'end'): ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Пустой ввод. Попробуйте снова.");
                    continue;
                }
                if (input == "end") // Если вводим "end" для завершения ввода
                {
                    break;
                }

                // Обновление файла
                UpdateFile(filePath, input);

                // Вывод сообщения и задержка
                Console.WriteLine($"Данные обновлены: {input}. Отправка во внешний мир через 30 секунд...");
                Task.Delay(30000); // Задержка 30 секунд

                Console.WriteLine($"Сообщение отправлено: {input}");
            }
        }

        private static void UpdateFile(string filePath, string input)
        {
            // Запись нового значения в файл
            File.AppendAllText(filePath, input + Environment.NewLine);
        }
    }
}