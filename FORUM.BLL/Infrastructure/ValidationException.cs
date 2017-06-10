using System;

namespace FORUM.BLL.Infrastructure
{
    public class ValidationException : ApplicationException
    {
        public ValidationException() : base() { }
        public ValidationException(string str, string property) : base(str)
        {
            Property = property;
        }

        public string Property { get; protected set; }

        public override string ToString()
        {
            return Message;
        }
    }
}
