using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Application.Notes.NotesCommands;

namespace Notes.WebAPI.Models
{
    public class UpdateNoteDto : IMapWith<UpdateNoteDto>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateNoteCommand, UpdateNoteDto>();
        }
    }
}
