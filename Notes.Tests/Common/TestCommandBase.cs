using Notes.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Tests.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected  readonly NotesDBContext context;

        public TestCommandBase()
        {
            context = NoteContextFactory.Create();
        }


        public void Dispose()
        {
            NoteContextFactory.Destroy(context);
        }
    }
}
