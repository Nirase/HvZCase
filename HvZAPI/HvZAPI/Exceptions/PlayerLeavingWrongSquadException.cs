namespace HvZAPI.Exceptions
{
    public class PlayerLeavingWrongSquadException : Exception
    {
        public PlayerLeavingWrongSquadException() { }
        public PlayerLeavingWrongSquadException(string message) : base(message) { }
    }
}
