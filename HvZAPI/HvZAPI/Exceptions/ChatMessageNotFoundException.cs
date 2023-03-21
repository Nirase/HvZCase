namespace HvZAPI.Exceptions
{
    public class ChatMessageNotFoundException : Exception
    {
        public ChatMessageNotFoundException() { }
        public ChatMessageNotFoundException(string message) : base(message) { }
    }
}
