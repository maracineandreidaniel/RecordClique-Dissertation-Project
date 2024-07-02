namespace RecordClique_BusinessLogic.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string message)
            : base(message)
        {
        }
    }
}
