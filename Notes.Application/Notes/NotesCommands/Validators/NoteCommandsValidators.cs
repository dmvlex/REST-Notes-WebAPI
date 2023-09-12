using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Notes.NotesCommands.Validators
{
    public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
    {
        public DeleteNoteCommandValidator()
        {
            RuleFor(deletecommand => deletecommand.Id).NotEqual(Guid.Empty);
            RuleFor(deletecommand => deletecommand.UserId).NotEqual(Guid.Empty);
        }
    }

    public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
    {
        //Правила валидации описываются внутри конструктора клкасса валидатора
        public CreateNoteCommandValidator()
        {
            RuleFor(createcommand => createcommand.Title).NotEmpty().MaximumLength(250);
            RuleFor(createcommand => createcommand.UserId).NotEqual(Guid.Empty);
        }
    }

    public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
    {
        public UpdateNoteCommandValidator()
        {
            RuleFor(updatecommand => updatecommand.Id).NotEqual(Guid.Empty);
            RuleFor(updatecommand => updatecommand.UserId).NotEqual(Guid.Empty);
            RuleFor(updatecommand => updatecommand.Title)
                .NotEmpty().MaximumLength(250);
        }
    }
}
