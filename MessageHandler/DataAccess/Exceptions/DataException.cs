namespace ClassLibrary1.DataAccess.Exceptions;

public class DataException : Exception
{
    public DataException(string message)
        : base(message) { }
}