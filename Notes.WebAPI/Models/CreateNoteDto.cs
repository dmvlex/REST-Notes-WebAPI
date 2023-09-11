using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Application.Notes.NotesCommands;

namespace Notes.WebAPI.Models
{
    //Дата трансфер объект. Нужен исключительно для получения инфы из вью
    public class CreateNoteDto : IMapWith<CreateNoteCommand> 
    {
        public string Title { get; set; }
        public string Details { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateNoteCommand, CreateNoteDto>();
        }
    }
}
