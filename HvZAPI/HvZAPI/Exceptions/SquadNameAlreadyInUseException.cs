namespace HvZAPI.Exceptions
{
    public class SquadNameAlreadyInUseException : Exception
    {
        public SquadNameAlreadyInUseException() { }
        public SquadNameAlreadyInUseException(string message) : base(message) { }
    }
}
