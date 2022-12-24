namespace ClassLibrary1.DataAccess;

public class Client : IAccount
{
    private string username;
    private string password;
    public Client(string name, string psw)
    {
        username = name;
        password = psw;
    }

    public Message SendMessage(string text)
    {
        Message msg = new Message(text);
        return msg;
    }
    
    public bool login(string name, string psw)
    {
        if (username != name && password != psw)
            return true;
        return false;
    }
}