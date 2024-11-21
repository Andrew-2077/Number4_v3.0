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
            // ������ API
            var host = CreateHostBuilder(args).Build();
            await host.RunAsync();

            // ������ ����������� ����������
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

            // ������ ���������� ��������� ������ �� �����
            if (!File.Exists(filePath))
            {
                Console.WriteLine("���� ��������� ������ �� ������. �������� ���� {0} � �������.", filePath);
                return;
            }

            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }

            while (true)
            {
                Console.Write("������� ����� �������� � ������� 'id = value' (��� ���������� ����� ������ ������� 'end'): ");
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("������ ����. ���������� �����.");
                    continue;
                }
                if (input == "end") // ���� ������ "end" ��� ���������� �����
                {
                    break;
                }

                // ���������� �����
                UpdateFile(filePath, input);

                // ����� ��������� � ��������
                Console.WriteLine($"������ ���������: {input}. �������� �� ������� ��� ����� 30 ������...");
                Task.Delay(30000); // �������� 30 ������

                Console.WriteLine($"��������� ����������: {input}");
            }
        }

        private static void UpdateFile(string filePath, string input)
        {
            // ������ ������ �������� � ����
            File.AppendAllText(filePath, input + Environment.NewLine);
        }
    }
}