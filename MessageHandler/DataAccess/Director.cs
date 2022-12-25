namespace ClassLibrary1.DataAccess;

public class Director : IAccount
{
    private string username;
    private string password;
    private List<Employee> _employees;
    public Director(string name, string psw)
    {
        username = name;
        password = psw;
        _employees = new List<Employee>();
    }

    public void GetReportEmployee(Employee employee)
    {
        
    }
    
    public void GetReportSource(Source source)
    {
        
    }
    
    public bool login(string name, string psw)
    {
        if (username == name && password == psw)
            return true;
        return false;
    }

    public void AddEmployee(Employee empl)
    {
        _employees.Add(empl);
    }
}