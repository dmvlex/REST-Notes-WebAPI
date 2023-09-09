using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notes.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Persistance
{
    public static class DependecyInjection
    {
        /// <summary>
        /// Добавляет Persistance в IoC-контейнер Web Api
        /// </summary>
        public static IServiceCollection AddPersistance(this IServiceCollection services,
            IConfiguration config)
        {
            var connectionString = config["DBConnection"];
            services.AddDbContext<INoteDBContext, NotesDBContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            return services; //возвращаем обратно полученные сервисы с нашим сервисом
        }

    }
}
