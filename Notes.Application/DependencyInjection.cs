using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application
{
    //содержит методы регистрация групп сервисов с помощью методов расширения
    public static class DependencyInjection 
    {
        public static IServiceCollection AddApplication (this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            }); //регистрируем медиатр
            return services;
        }
    }
}
