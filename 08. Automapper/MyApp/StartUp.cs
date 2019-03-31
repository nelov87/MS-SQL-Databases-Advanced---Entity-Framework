using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Core;
using MyApp.Core.Contracts;
using MyApp.Data;
using System;

namespace MyApp
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            IServiceProvider services = ConfigureServices();
            IEngine engine = new Engine(services);
            engine.Run();
        }

        private static IServiceProvider ConfigureServices()
        {
            var serviseColection = new ServiceCollection();

            serviseColection.AddDbContext<MyAppContext>(db =>
            db.UseSqlServer("Server=DESKTOP-533LOVH\\SQLEXPRESS;Database=Mapper;Integrated Security=True;"));

            serviseColection.AddTransient<IComandIterpreter, ComandInterpreter>();
            serviseColection.AddTransient<Mapper>();

            var serviceProvider = serviseColection.BuildServiceProvider();

            return serviceProvider;

        }
    }
}
