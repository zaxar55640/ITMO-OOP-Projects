namespace ClassLibrary1.DataAccess;

public class SourceReport
{
    private int _amountServed { get; set; }
    private int _amountMessages { get; set; }
    private Source _source { get; }
    private DateTime _time;
    private bool status { get; set; }

    public SourceReport(Source source, int amountMessages = 0, int amountServed = 0)
    {
        _source = source;
        _amountMessages = amountMessages;
        _amountServed = amountServed;
        _time = DateTime.Now;
        status = true;
    }

    public int GetReceivedMessageAmount()
    {
        return _amountMessages;
    }
    
    public int GetDoneMessageAmount()
    {
        return _amountServed;
    }
    
    public void AddReceivedMessage()
    {
        _amountMessages += 1;
    }
    
    public void AddDoneMessage()
    {
        _amountServed += 1;
    }
    
    public SourceReport CloseReport()
    {
        return this;
    }
    
    public bool GetReportStatus()
    {
        return status;
    }

    public Source GetSource()
    {
        return _source;
    }
}