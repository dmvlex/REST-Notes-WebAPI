using Microsoft.EntityFrameworkCore;
using Notes.Application.Notes.NotesCommands;
using Notes.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Tests.Notes.Commands
{
    public class NotesCommandHandlersTest : TestCommandBase
    {
        [Fact]
        public async Task CreateNoteCommand_Success()
        {
            //Arrange
            var handler = new CreateNoteCommandHandler(context);
            var notename = "note TEST";
            var noteDetails = "Thats a test note";

            //Act
            var noteId = await handler.Handle(new CreateNoteCommand() { 
                Title = notename, 
                Details = noteDetails,
                UserId = NoteContextFactory.UserAid},CancellationToken.None);

            //Assert
            Assert.NotNull(await context.Notes.SingleOrDefaultAsync(note =>
                note.Id == noteId && note.Title == notename && note.Details == noteDetails));
        }
    }
}
