namespace HvZAPI.Exceptions
{
    public class PlayerAlreadyInGameException : Exception
    {
        public PlayerAlreadyInGameException() { }
        public PlayerAlreadyInGameException(string message) : base(message) { }
    }
}
