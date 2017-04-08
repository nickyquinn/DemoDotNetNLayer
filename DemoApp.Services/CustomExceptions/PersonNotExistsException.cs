using System;

namespace DemoApp.Services.CustomExceptions
{
    class PersonNotExistsException : Exception
    {
        public PersonNotExistsException()
        {

        }

        public PersonNotExistsException(string message) : base(message) { }

        public PersonNotExistsException(string message, Exception inner) : base(message, inner) { }
    }
}
