using Microsoft.Extensions.DependencyInjection;

namespace ConsoleWeb
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Text;

    namespace ConsoleWeb
    {
        public class Startup
        {
            public void Configure(IApplicationBuilder app)
            {
                //app.Run(context =>
                //{
                //    return context.Response.WriteAsync("Hello world");
                //});
            }
            public void ConfigureServices(IServiceCollection services)
            {
                //services.AddMvc();
                services.AddSingleton<Microsoft.Extensions.Hosting.IHostedService, BackManagerService>(factory =>
                {
                    BackManagerService.OrderManagerService order = new BackManagerService.OrderManagerService();
                    return new BackManagerService(options =>
                    {
                        options.Name = "订单超时检查";
                        options.CheckTime = 5 * 100;
                        options.Callback = order.CheckOrder;
                        options.Handler = order.OnBackHandler;
                    });
                });
            }
        }
    }
}
