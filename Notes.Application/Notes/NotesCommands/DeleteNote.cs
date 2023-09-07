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
    public class DeleteNoteCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }

    public class DeleteNoteCommandHandler
    : IRequestHandler<DeleteNoteCommand, Unit>
    {
        private readonly INoteDBContext dbContext;

        public DeleteNoteCommandHandler(INoteDBContext _dbContext) => dbContext = _dbContext;
        public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Notes
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null || entity.UserId != request.UserId) //если не нашли или нашли, но не то - выкидываем экспешн
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            dbContext.Notes.Remove(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
