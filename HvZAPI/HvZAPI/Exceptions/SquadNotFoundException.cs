namespace HvZAPI.Exceptions
{
    public class SquadNotFoundException : Exception
    {
        public SquadNotFoundException() { }
        public SquadNotFoundException(string message) : base(message) { }
    }
}
