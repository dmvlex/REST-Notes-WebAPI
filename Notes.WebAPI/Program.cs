using Notes.Persistance;
using Notes.Application;
using Notes.Application.Common.Mappings;
using Notes.Application.Interfaces;
using Microsoft.Extensions.Configuration;

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Notes.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(INoteDBContext).Assembly));

            }); //Автомаппер конфигурируем тут, потому что нужно получить текущую сборку

            builder.Services
                .AddApplication()
                .AddPersistance(builder.Configuration)
                .AddControllers();

            builder.Services.AddCors(options =>
            {
                //Впускаем всех и всегда. Добавляем правило, потом его же юзаем в middleware
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod(); 
                    policy.AllowAnyOrigin();
                });
            });


            var app = builder.Build();

            using(var scope = app.Services.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetRequiredService<NotesDBContext>(); //получаем зависимость из контекста
                    DBInitializer.Initialize(context); //создаем базу данных
                }
                catch (Exception exception)
                {

                }
            }


            app.UseHttpsRedirection(); //редиректим на https
            app.UseRouting(); // Добавляем эндпоинты
            app.UseCors("AllowAll"); //Включаем КОРС

            app.MapControllers(); //мапим контроллеры для роутинга

            app.Run();
        }
    }
}