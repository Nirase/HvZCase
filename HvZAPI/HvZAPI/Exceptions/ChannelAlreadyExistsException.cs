namespace HvZAPI.Exceptions
{
    public class ChannelAlreadyExistsException : Exception
    {
        public ChannelAlreadyExistsException() { }

        public ChannelAlreadyExistsException(string message)
        : base(message)
        {
        }
    }
}
