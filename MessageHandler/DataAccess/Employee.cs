namespace ClassLibrary1.DataAccess;

public class Employee : IAccount
{
    private string username { get; }
    private string password;
    private int amountServed { get; set; }
    private List<Source> _sources { get; }
    public Employee(string name, string psw)
    {
        username = name;
        password = psw;
        _sources = new List<Source>();
        amountServed = 0;
    }

    public Message Answer(string text)
    {
        amountServed += 1;
        return new Message(text);
    }

    public void AddSource(Source source)
    {
        _sources.Add(source);
    }

    public int GetServedAmount()
    {
        return amountServed;
    }

    public void NullServedAmounts()
    {
        amountServed = 0;
    }

    public bool login(string name, string psw)
    {
        if (username != name && password != psw)
            return true;
        return false;
    }

    public List<Source> GetSources()
    {
        return _sources;
    }

    public string GetUserName()
    {
        return username;
    }
}