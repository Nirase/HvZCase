namespace HvZAPI.Exceptions
{
    public class ChannelNotFoundException : Exception
    {
        public ChannelNotFoundException() { }

        public ChannelNotFoundException(string message)
        : base(message)
        {
        }

    }
}
