using MediatR;
using Notes.Application.Interfaces;
using Notes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Notes.NotesCommands
{
    public class CreateNoteCommand : IRequest<Guid> //Возвращает Id новой заметки. Содержит только данные
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
    }

    public class CreateNoteCommandHandler //Обработчик данных, переданных в запросе
        : IRequestHandler<CreateNoteCommand,Guid>
    {
        private readonly INoteDBContext dbContext;

        public CreateNoteCommandHandler(INoteDBContext _dbContext) => dbContext = _dbContext;
        public async Task<Guid> Handle(CreateNoteCommand request,
            CancellationToken cancellationToken) //Логика обработки команды создания
        {
            var note = new Note
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Title = request.Title,
                Details = request.Details,
                CreationDate = DateTime.Now,
                EditDate = null
            };

            await dbContext.Notes.AddAsync(note, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return note.Id;
        }

    }
}
