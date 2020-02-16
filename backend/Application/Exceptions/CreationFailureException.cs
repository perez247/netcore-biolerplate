using System;

namespace Application.Exceptions
{
    public class CreationFailureException : Exception
    {
        public CreationFailureException(string name, string message)
            : base($"Failed to create \"{name}\": {message}")
        {
        }
    }
}