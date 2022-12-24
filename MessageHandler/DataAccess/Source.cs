namespace ClassLibrary1.DataAccess;

public class Source
{
    private List<Message> _messages { get; set; }
    public string _name { get; set; }

    public Source(string name)
    {
        _name = name;
        _messages = new List<Message>();
    }

    public void AddMessage(Message msg)
    {
        _messages.Add(msg);
    }

    public Message GetMessage(Message msg)
    {
        return _messages.FirstOrDefault(m => m == msg);
    }

    public List<Message> GetMessages()
    {
        return _messages;
    }
    
    public void SetMessageStatusAsDone(Message msg)
    {
        _messages.FirstOrDefault(m => m == msg).status = MessageStatus.Done;
    }

    public string GetSourceName()
    {
        return _name;
    }
}