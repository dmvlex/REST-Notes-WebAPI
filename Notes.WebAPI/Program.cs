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

            }); //���������� ������������� ���, ������ ��� ����� �������� ������� ������

            builder.Services
                .AddApplication()
                .AddPersistance(builder.Configuration)
                .AddControllers();

            builder.Services.AddCors(options =>
            {
                //�������� ���� � ������. ��������� �������, ����� ��� �� ����� � middleware
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
                    var context = scope.ServiceProvider.GetRequiredService<NotesDBContext>(); //�������� ����������� �� ���������
                    DBInitializer.Initialize(context); //������� ���� ������
                }
                catch (Exception exception)
                {

                }
            }


            app.UseHttpsRedirection(); //���������� �� https
            app.UseRouting(); // ��������� ���������
            app.UseCors("AllowAll"); //�������� ����

            app.MapControllers(); //����� ����������� ��� ��������

            app.Run();
        }
    }
}