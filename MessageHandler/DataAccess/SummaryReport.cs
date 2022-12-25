namespace ClassLibrary1.DataAccess;

public class SummaryReport
{
    private int _amountMessage { get; }
    private int _amountDoneMessage { get; }

    public SummaryReport(int am, int adm)
    {
        _amountMessage = am;
        _amountDoneMessage = adm;
    }

    public int GetAmountMessage()
    {
        return _amountMessage;
    }
    
    public int GetAmountDoneMessage()
    {
        return _amountDoneMessage;
    }
}