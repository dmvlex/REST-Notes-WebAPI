using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Notes.NotesCommands
{
    public class UpdateNoteCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
    }
    public class UpdateNoteCommandHandler
        : IRequestHandler<UpdateNoteCommand,Unit>
    {
        private readonly INoteDBContext dbContext;
        public UpdateNoteCommandHandler(INoteDBContext _dbContext) => dbContext = _dbContext;
        public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Notes.FirstOrDefaultAsync(x => x.Id == request.Id,cancellationToken);

            if (entity == null || entity.Id != request.Id) //если не нашли или нашли, но не то - выкидываем экспешн
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            entity.Details = request.Details; //если нет - апдейтим сущность
            entity.Title = request.Title;
            entity.EditDate = DateTime.Now;

            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value; //Обработчик должен вернуть хоть что-то, поэтому возвращаем это
        }
    }
}
