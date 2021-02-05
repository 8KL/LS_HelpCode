using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using static LS_BackGroundTaskCode.Model;

namespace ConsoleWeb
{
    public class BackManagerService : BackgroundService
    {
        BackManagerOptions options = new BackManagerOptions();
        public BackManagerService(Action<BackManagerOptions> options)
        {
            options.Invoke(this.options);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // 延迟启动
            await Task.Delay(this.options.CheckTime, stoppingToken);

            options.OnHandler(0, $"正在启动托管服务 [{this.options.Name}]....");
            stoppingToken.Register(() =>
            {
                options.OnHandler(1, $"托管服务  [{this.options.Name}] 已经停止");
            });

            int count = 0;
            while (!stoppingToken.IsCancellationRequested)
            {
                count++;

                try
                {
                    options?.Callback();
                    if (count == 3)
                        throw new Exception("模拟业务报错");
                    options.OnHandler(1, $" [{this.options.Name}] 第 {count} 次执行任务....");
                }
                catch (Exception ex)
                {
                    options.OnHandler(2, $" [{this.options.Name}] 执行托管服务出错", ex);
                }
                await Task.Delay(this.options.CheckTime, stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            options.OnHandler(3, $" [{this.options.Name}] 由于进程退出，正在执行清理工作");
            return base.StopAsync(cancellationToken);
        }
 
      
        public class OrderManagerService
        {
            public void CheckOrder()
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("==业务执行完成==");
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            public void OnBackHandler(BackHandler handler)
            {
                switch (handler.Level)
                {
                    default:
                    case 0: 
                        break;
                    case 1:
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                }
                Console.WriteLine("{0} | {1} | {2} | {3}", handler.Level, handler.Message, handler.Exception, handler.State);
                Console.ForegroundColor = ConsoleColor.Gray;
                if (handler.Level == 1)
                {
                    Console.WriteLine("执行任务中");
                }
                if (handler.Level == 2)
                {
                    // 服务执行出错，进行补偿等工作
                }
                else if (handler.Level == 3)
                {
                    // 退出事件，清理你的业务
                    CleanUp();
                }
            }

            public void CleanUp()
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("==清理完成==");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }
}
