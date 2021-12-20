using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Module3HW5
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var resultTaks = await ResultAsunc();
            Console.WriteLine(resultTaks);
        }

        private static async Task<string> ReadFileAsync(string path)
        {
            using (var reader = new StreamReader(path))
            {
                return await reader.ReadToEndAsync();
            }
        }

        private static async Task<string> ResultAsunc()
        {
            return await Task<string>.Run(() =>
            {
                var taskList = new List<Task<string>>();

                taskList.Add(ReadFileAsync(".\\Resources\\Hello.txt"));
                taskList.Add(ReadFileAsync(".\\Resources\\World.txt"));

                var result = string.Empty;

                Task.WaitAll(taskList.ToArray());
                foreach (var item in taskList)
                {
                    result += $"{item.GetAwaiter().GetResult()} ";
                }

                return result;
            });
        }
    }
}
