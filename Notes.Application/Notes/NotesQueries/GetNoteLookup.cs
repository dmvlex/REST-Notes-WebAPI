using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class NoteLookupItem : IMapWith<Note>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
    }
    public class NoteListVm
    {
        public IList<NoteLookupItem> NoteList { get; set; }
    }

    public class GetNoteListQuery : IRequest<NoteListVm>
    {
        public Guid UserId { get; set; }
    }

    public class GetNoteListQueryHandler 
        : IRequestHandler<GetNoteListQuery,NoteListVm>
    {
        protected readonly INoteDBContext dbcontext;
        protected readonly IMapper mapper;

        public GetNoteListQueryHandler(INoteDBContext _context, IMapper _mapper)
        {
            mapper = _mapper;
            dbcontext = _context;
        }

        public async Task<NoteListVm> Handle(GetNoteListQuery request, CancellationToken cancellationToken)
        {
            var userNotes = await dbcontext.Notes
                .Where(note => note.UserId == request.UserId) //получаем список по юзеру
                .ProjectTo<NoteLookupItem>(mapper.ConfigurationProvider)//мапим к айтему списка записок
                .ToListAsync(cancellationToken);// превращаем в список

            return new NoteListVm() { NoteList = userNotes };
        }
    }

}
