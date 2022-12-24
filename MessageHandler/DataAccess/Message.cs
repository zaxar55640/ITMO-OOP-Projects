namespace ClassLibrary1.DataAccess;

public class Message
{
    private string _message { get; }

    private DateTime _time;

    public Message(string message)
    {
        _message = message;
        _time = DateTime.Now;
        MessageStatus status = MessageStatus.New;
    }

    public string GetMessage()
    {
        return _message;
    }
    public MessageStatus status { get; set; }

    public void SetStatus(MessageStatus ms)
    {
        status = ms;
    }
}