using Microsoft.EntityFrameworkCore;
using Notes.Application.Intefaces;
using Notes.Domain;
using Notes.Persistance.EntityTypeConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Persistance
{
    sealed public class NotesDBContext : DbContext, INoteDBContext
    {
        public DbSet<Note> Notes { get; set; }

        //Переопределение конструктора, что бы можно было передать строку подключения позже
        public NotesDBContext(DbContextOptions<NotesDBContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new NoteConfiguration()); //Добавляем конфигурации Note сущности
            base.OnModelCreating(builder);
        }
    }
}
