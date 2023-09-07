using Microsoft.EntityFrameworkCore;
using Notes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Intefaces
{
    public interface INoteDBContext //Реализуется в Notes.Persistance. Но для слабой связи создаем интерфейс тут
    {
        DbSet<Note> Notes { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken); //дублируем для удобства из DbContext. Сохраняет контекст в БД
    }
}
