using Microsoft.EntityFrameworkCore;
using Notes.Domain;
using Notes.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Tests.Common
{
    public static class NoteContextFactory
    {
        public static Guid UserAid = Guid.NewGuid();
        public static Guid UserBid = Guid.NewGuid();

        public static Guid NotesIdForDelete = Guid.NewGuid();
        public static Guid NotesIdForUpdate = Guid.NewGuid();

        public static NotesDBContext Create()
        {
            var options = new DbContextOptionsBuilder<NotesDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new NotesDBContext(options);
            context.Database.EnsureCreated();

            context.AddRange(new Note
            {
                CreationDate = DateTime.Now,
                Details = "Details",
                EditDate = DateTime.Now,
                Title = "Title",
                Id = Guid.Parse("{a5d63427-3f9d-495f-b88c-e9642c1756db}"),
                UserId = UserAid
            },
            new Note
            {
                CreationDate = DateTime.Now,
                Details = "SecondDetails",
                EditDate = DateTime.Now,
                Title = "SecondTitle",
                Id = Guid.Parse("{b83c4562-e6ff-4ec0-aaa3-972755933561}"),
                UserId = UserBid
            }
            );
            context.SaveChanges();
            return context;
        }

        public static void Destroy(NotesDBContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
