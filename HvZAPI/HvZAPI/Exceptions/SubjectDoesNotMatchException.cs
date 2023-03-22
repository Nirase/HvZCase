namespace HvZAPI.Exceptions
{
    public class SubjectDoesNotMatchException : Exception
    {
        public SubjectDoesNotMatchException() { }
        public SubjectDoesNotMatchException(string message) : base(message) { }
    }
}
