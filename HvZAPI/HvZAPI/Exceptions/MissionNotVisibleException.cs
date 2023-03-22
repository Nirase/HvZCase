namespace HvZAPI.Exceptions
{
    public class MissionNotVisibleException : Exception
    { 
        public MissionNotVisibleException() { }
        public MissionNotVisibleException(string message) : base(message) { }
    }
}
