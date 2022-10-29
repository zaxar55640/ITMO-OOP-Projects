namespace Isu.Extra.Exceptions;

public class NotAvailableOGNPException : Exception
{
    public NotAvailableOGNPException()
        : base("Ognp has same faculty as a student.")
    {
    }
}