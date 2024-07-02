namespace RecordClique_BusinessLogic.Exceptions
{
    public class IncorrectPasswordException : Exception
    {
        public IncorrectPasswordException(string message)
            : base(message)
        {
        }
    }
}