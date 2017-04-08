using System;

namespace DemoApp.Services.CustomExceptions
{
    public class PersonExistsException : Exception
    {
        public PersonExistsException()
        {

        }

        public PersonExistsException(string message) : base(message) {}

        public PersonExistsException(string message, Exception inner) :base(message,inner) { }
    }
}
