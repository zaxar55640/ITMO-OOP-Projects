using ClassLibrary1.Business;
using ClassLibrary1.DataAccess;
namespace TestProject2;

public class UnitTest1
{
    [Fact]
    public void ClientSendMessageEmployeeAnswer_DBShowsChanges()
    {
        DataBase db = new DataBase();
        Service service = new Service(db, "123");
        Client client = new Client("andrey", "1234");
        Employee employee = new Employee("asdsa", "sad");
        Source source = new Source("whatsapp");
        service.AddClient(client);
        service.AddEmployee(employee);
        service.AddSource(source);
        service.EmployeeStartSession(employee);
        service.AddSourcesToEmployee(employee, source);
        service.SourceStartSession(source);
        Message msg = service.ClientSendMessage(client, "ну как там с деньгами", source);
        Assert.Equal(1, db.GetSources().First(p => p == source).GetMessages().Count);
        Message aMsg = service.AnswerMessage(source, employee, "какими деньгами", msg);
        Assert.Equal(1, db.GetSourceReports().Count);
        Assert.Equal(1, db.GetSourceReports().First().GetDoneMessageAmount());
        Message msg2 = service.ClientSendMessage(client, "которые я вложил в капитал", source);
        Assert.Equal(2, db.GetSourceReports().First().GetReceivedMessageAmount());
        service.EmployeeCloseSession(employee);
        Assert.Equal(1, db.GetEmployeeReports().First().GetAmountServed());
    }
    
    [Fact]
    public void SignupLogin_SustemWorks()
    {
        DataBase db = new DataBase();
        Service service = new Service(db, "123");
        string name = "andrey";
        string psw = "1234";
        Client client = new Client("andrey", "1234");
        Employee employee = new Employee("asdsa", "sad");
        Source source = new Source("whatsapp");
        service.AddClient(client);
        service.AddEmployee(employee);
        service.AddSource(source);
        Client client2 = service.loginClient(name, psw);
        Assert.True(client.login(name, psw));
    }
}