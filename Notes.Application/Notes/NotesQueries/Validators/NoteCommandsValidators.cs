using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Notes.NotesQueries.Validators
{
    public class GetNoteListValidator : AbstractValidator<GetNoteListQuery>
    {
        public GetNoteListValidator()
        {
            RuleFor(getlistquery => getlistquery.UserId).NotEqual(Guid.Empty);
        }
    }

    public class GetNoteDetailsValidator : AbstractValidator<GetNoteDetailsQuery>
    {
        public GetNoteDetailsValidator()
        {
            RuleFor(getdetailsquery => getdetailsquery.UserId).NotEqual(Guid.Empty);
            RuleFor(getdetailsquery => getdetailsquery.Id).NotEqual(Guid.Empty);
        }
    }
}
