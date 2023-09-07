using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Persistance
{
    public class DBInitializer
    {
        /// <summary>
        /// Инициализуерт соединение с базой данных
        /// </summary>
        /// <param name="context"></param>
        public static void Initialize(NotesDBContext context)
        {
            context.Database.EnsureCreated();
        }
        //Этот метод нужен в первую очередь
        //для того, что бы можно было проинициализировать ее где угодно
        //в отрыве от создания контекста и уже после конфигурации

    }
}
