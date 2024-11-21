using System;
using System.IO;
using System.Threading;

class Program
{
    private const string FilePath = @"data.txt";

    static void Main(string[] args)
    {
        // Чтение стартового состояния данных из файла
        if (!File.Exists(FilePath))
        {
            Console.WriteLine("Файл состояния данных не найден. Создайте файл {0} с данными.", FilePath);
            return;
        }

        string[] lines = File.ReadAllLines(FilePath);
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
            if (input == "end") //Если вводим "end" для завершения ввода
            {
                break;
            }

            // Обновление файла
            UpdateFile(input);

            // Вывод сообщения и задержка
            Console.WriteLine($"Данные обновлены: {input}. Отправка во внешний мир через 30 секунд...");
            Thread.Sleep(30000); // Задержка 30 секунд

            Console.WriteLine($"Сообщение отправлено: {input}");
        }
    }

    private static void UpdateFile(string input)
    {
        // Запись нового значения в файл
        File.AppendAllText(FilePath, input + Environment.NewLine);
    }
}