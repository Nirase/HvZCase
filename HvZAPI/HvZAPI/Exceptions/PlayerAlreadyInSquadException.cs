namespace HvZAPI.Exceptions
{
    public class PlayerAlreadyInSquadException : Exception
    {
        public PlayerAlreadyInSquadException() { }
        public PlayerAlreadyInSquadException(string message) : base(message) { }
    }
}
