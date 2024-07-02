namespace RecordClique_BusinessLogic.Exceptions
{
    public class ConcurrencyException : Exception
    {
        public ConcurrencyException(string message)
            : base(message)
        {
        }
    }
}
