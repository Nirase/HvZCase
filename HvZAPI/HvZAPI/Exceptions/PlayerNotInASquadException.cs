namespace HvZAPI.Exceptions
{
    public class PlayerNotInASquadException :Exception 
    {
        public PlayerNotInASquadException() { }
        public PlayerAlreadyInSquadException(string message):base(message) { }
    }
}
