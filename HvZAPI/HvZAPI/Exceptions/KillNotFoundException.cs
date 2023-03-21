namespace HvZAPI.Exceptions
{
    public class KillNotFoundException : Exception
    {
        public KillNotFoundException() { }
        public KillNotFoundException(string message) : base(message) { }
    }
}
