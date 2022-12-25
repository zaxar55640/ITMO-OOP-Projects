using System.Text.Json;
using ClassLibrary1.DataAccess;
using ClassLibrary1.DataAccess.Exceptions;

namespace ClassLibrary1.Business;

public class Service
{
    private DataBase db;
    private string path;

    public Service(DataBase dataB, string ppath)
    {
        db = dataB;
        path = ppath;
    }
    
    public void CreateAccount(string name, string password, AccountType acc)
    {
        switch(acc)

        {
            case AccountType.Client:
            {
                db.AddClient(new Client(name, password));
                break;
            }
            case AccountType.Director:
            {
                db.AddDirector(new Director(name, password));
                break;
            }
            case AccountType.Employee:
            {
                db.AddEmployee(new Employee(name, password));
                break;
            }

        }
    }

    public string LoginAuthentication(string name, string password)
    {
        if (name == string.Empty || password == String.Empty)
            throw new DataException("Empty lines of password or login");
        return db.loginAuthentication(name, password);
    }
    
    public Employee loginEmp(string name, string password)
    {
        if (name == string.Empty || password == String.Empty)
            throw new DataException("Empty lines of password or login");
        return db.loginEmp(name, password);
    }
    
    public Client loginClient(string name, string password)
    {
        if (name == string.Empty || password == String.Empty)
            throw new DataException("Empty lines of password or login");
        return db.loginClient(name, password);
    }
    
    public Director loginDir(string name, string password)
    {
        if (name == string.Empty || password == String.Empty)
            throw new DataException("Empty lines of password or login");
        return db.loginDir(name, password);
    }

    public void HandleMessage(string text, Source source)
    {
        if (source == null) throw new DataException("Can't pass null source to handle message.");
        Message msg = new Message(text);
        msg.status = MessageStatus.Received;
        db.GetCurrentSourceReport(source).AddReceivedMessage();
        source.AddMessage(msg);
    }

    public Message AnswerMessage(Source source, Employee emp,string answer, Message msg)
    {
        if (source == null) throw new DataException("source");
        if (emp == null) throw new DataException("emp");
        if (msg == null) throw new DataException("msg");
        source.GetMessage(msg);//.SetStatus(MessageStatus.Done);
        db.GetCurrentSourceReport(source).AddDoneMessage();
        Message ans = emp.Answer(answer);
        return ans;
    }

    public List<Message> ShowMessagesBySource(Source source)
    {
        if (!db.GetSources().Any(s => s == source)) throw new DataException("There is no such a source to get messages/");
        return source.GetMessages();
    }

    public List<Source> ShowEmployeesSources(Employee emp)
    {
        return emp.GetSources();
    }

    public void EmployeeStartSession(Employee emp)
    {
        if (emp == null) throw new DataException("Employee wasn't give, can't start sessiom.");
        EmployeeReport er = new EmployeeReport(emp);
        db.AddEmployeeReport(er);
    }

    public void EmployeeCloseSession(Employee emp)
    {
        if (emp == null) throw new DataException("Can't close session/ user not exists");
        db.GetCurrentEmployeeReport(emp).CloseReport(emp.GetServedAmount());
    }
    
    public void SourceStartSession(Source source)
    {
        if (source == null) throw new DataException("Can't close session/ source not exists");
        db.AddSourceReport(source);
    }

    public void SourceCloseSession(Source source)
    {
        if (source == null) throw new DataException("Can't close session/ source not exists");
        db.GetCurrentSourceReport(source).CloseReport();
    }

    public List<EmployeeReport> GetEmployeeReports()
    {
        return db.GetEmployeeReports();
    }
    
    public List<SourceReport> GetSourceReports()
    {
        return db.GetSourceReports();
    }

    public SummaryReport GetSummaryReport()
    {
        List<SourceReport> reports = db.GetSourceReports();
        db.GetSourceReports().ForEach(p => this.SourceCloseSession(p.GetSource()));
        int amountMessage = reports.Sum(r => r.GetReceivedMessageAmount());
        int amountServedMessage = reports.Sum(r => r.GetDoneMessageAmount());
        return new SummaryReport(amountMessage, amountServedMessage);
    }

    public void AddSourcesToEmployee(Employee empl, Source src)
    {
        empl.AddSource(src);
    }   

    public void AddEmployeesToDirector(Director dir, Employee empl)
    {
        dir.AddEmployee(empl);
    }
    
    public Service Restore()
    {
        Service sr = JsonSerializer.Deserialize<Service>(File.ReadAllText(path)) ?? throw new InvalidOperationException();
        return sr;
    }

    public void Save()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        File.AppendAllText(path, JsonSerializer.Serialize(this));
    }

    public Message ClientSendMessage(Client client, string text, Source source)
    {
        if (client == null) throw new DataException("Couldn't send message client - not exists");
        if (source == null) throw new DataException("Couldn't send message source - not exists");
        if (text == string.Empty) throw new DataException("Couldn't send message text - not exists");
        Message msg = client.SendMessage(text);
        this.HandleMessage(text, source);
        return msg;
    }

    public List<Source> GetSources()
    {
        return db.GetSources();
    }

    public void AddEmployee(Employee emp)
    {
        if (emp == null) throw new DataException("Couldn't add employee - not exists");
        db.AddEmployee(emp);
    }

    public void AddSource(Source src)
    {
        if (src == null) throw new DataException("Couldn't add source - not exists");
        db.AddSource(src);
    }

    public void AddClient(Client client)
    {
        if (client == null) throw new DataException("Couldn't add client - not exists");
        db.AddClient(client);
    }

    public void AddDirector(Director dir)
    {
        db.AddDirector(dir);
    }
}