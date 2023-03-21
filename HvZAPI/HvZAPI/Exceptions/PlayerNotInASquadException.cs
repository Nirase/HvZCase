namespace HvZAPI.Exceptions
{
    public class PlayerNotInASquadException :Exception 
    {
        public PlayerNotInASquadException() { }
        public PlayerNotInASquadException(string message):base(message) { }
    }
}
