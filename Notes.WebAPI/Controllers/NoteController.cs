using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Notes.Application.Notes.NotesCommands;
using Notes.Application.Notes.NotesQueries;
using Notes.WebAPI.Models;

namespace Notes.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class NoteController : BaseController
    {
        private readonly IMapper mapper;

        public NoteController(IMapper _mapper)
        {
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<ActionResult<NoteListVm>> GetAll()
        {
            var query = new GetNoteListQuery
            {
                UserId = UserId
            }; //Формируем запрос. 

            var vm = await Mediator.Send(query); //Отправляем в медиатор, там он уже разберется

            return Ok(vm); //возвращаем View Model
            //ахуенно, да?
        }

        [HttpGet("{id}")] //таким образом к предыдущему роуту добавляется еще один элемент
        public async Task<ActionResult<NoteDetailVm>> Get(Guid id)
        {
            var query = new GetNoteDetailsQuery
            {
                Id = id,
                UserId = UserId
            };

            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDto noteData) // [FromBody] - вытаскивать из тела запроса
        {
            var command = mapper.Map<CreateNoteCommand>(noteData);

            command.UserId = UserId;

            var noteId = await Mediator.Send(command);


            return Ok(noteId);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateNoteDto newNoteData)
        {
            var command = mapper.Map<UpdateNoteCommand>(newNoteData);
            command.UserId = UserId;

            await Mediator.Send(command);

            return NoContent(); //204. Запрос удачный, но без контента
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteNoteCommand()
            {
                Id = id,
                UserId = UserId
            };

            await Mediator.Send(command);
            return NoContent();
        }
    }
}
