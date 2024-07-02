namespace RecordClique_BusinessLogic.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string message)
            : base(message)
        {
        }
    }
}
