using System;
using System.IO;
using ConsoleWeb.ConsoleWeb;
using Microsoft.AspNetCore.Hosting;

namespace LS_BackGroundTaskCode
{
    class Program
    {
        /// <summary>
        ///  模拟后台任务
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
            Console.ReadKey();
        }
    }
}
