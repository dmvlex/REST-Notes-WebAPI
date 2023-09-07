using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Common.Mappings;
using Notes.Application.Interfaces;
using Notes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Notes.NotesQueries
{
    //Класс возвращаемых запрососм данных. ViewModel представления
    public class NoteDetailVm : IMapWith<Note>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? EditDate { get; set; }
    }
    public class GetNoteDetailsQuery : IRequest<NoteDetailVm>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }

    public class GetNoteDetailsQueryHandler
        : IRequestHandler<GetNoteDetailsQuery, NoteDetailVm>
    {
        private readonly IMapper mapper;
        private readonly INoteDBContext dbcontext;

        public GetNoteDetailsQueryHandler(INoteDBContext _dbcontext, IMapper _mapper)
        {
            dbcontext = _dbcontext;
            mapper = _mapper;
        }
        public async Task<NoteDetailVm> Handle(GetNoteDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await dbcontext.Notes
                .FirstAsync(note => note.Id == request.Id, cancellationToken);

            if (entity == null || entity.UserId != request.UserId) //если не нашли или нашли, но не то - выкидываем экспешн
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            return mapper.Map<NoteDetailVm>(entity);
        }
    }

}
