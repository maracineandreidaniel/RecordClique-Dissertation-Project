namespace RecordClique_BusinessLogic.Exceptions
{
    public class InsufficientMoneyException : Exception
    {
        public InsufficientMoneyException(string message)
            : base(message)
        {
        }
    }
}
