namespace RecordClique_BusinessLogic.Exceptions
{
    public class NotMatchingPasswordsException : Exception
    {
        public NotMatchingPasswordsException(string message)
            : base(message)
        {
        }
    }
}
