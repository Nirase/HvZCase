namespace HvZAPI.Exceptions
{
    public class MissionNotFoundException : Exception
    {
        public MissionNotFoundException() { }
        public MissionNotFoundException(string message) : base(message) { }
    }
}
