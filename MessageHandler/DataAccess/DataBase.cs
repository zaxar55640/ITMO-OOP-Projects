using System.Reflection.Metadata;
using ClassLibrary1.DataAccess.Exceptions;

namespace ClassLibrary1.DataAccess;

public class DataBase
{
    private List<IAccount> users { get; }
    private List<Source> _sources { get; set; }
    private List<Client> _clients { get; set; }
    private List<Director> _directors { get; set; }
    private List<Employee> Employees { get; set; }
    private List<EmployeeReport> EmployeeReports { get; set; }
    private List<SourceReport> _sourceReports { get; set; }

    public DataBase()
    {
        _sources = new List<Source>();
        Employees = new List<Employee>();
        _directors = new List<Director>();
        _clients = new List<Client>();
        EmployeeReports = new List<EmployeeReport>();
        _sourceReports = new List<SourceReport>();
    }


    public string loginAuthentication(string name, string psw)
    {
        if (_clients.Any(c => c.login(name, psw)))
            return "client";
        if (_directors.Any(c => c.login(name, psw)))
            return "director";
        if (Employees.Any(c => c.login(name, psw)))
            return "employee";
        return "Wrong password or username.";
    }
    
    public Director loginDir(string name, string psw)
    {
        return _directors.FirstOrDefault(d => d.login(name, psw) == true);
    }
    
    public Employee loginEmp(string name, string psw)
    {
        return Employees.FirstOrDefault(d => d.login(name, psw) == true);
    }
    
    public Client loginClient(string name, string psw)
    {
        return _clients.FirstOrDefault(d => d.login(name, psw) == true);
    }

    public List<EmployeeReport> GetEmployeeReports()
    {
        return EmployeeReports;
    }
    
    public List<SourceReport> GetSourceReports()
    {
        return _sourceReports;
    }
    
    public void AddSource(Source source)
    {
        _sources.Add(source);
    }
    
    public void AddClient(Client client)
    {
        _clients.Add(client);
    }
    
    public void AddEmployee(Employee employee)
    {
        Employees.Add(employee);
    }
    
    public void AddDirector(Director director)
    {
        _directors.Add(director);
    }
    
    public void AddEmployeeReport(EmployeeReport er)
    {
        if (er == null) throw new DataException("Couldn't add employee report since report is not exists");
        EmployeeReports.Add(er);
    }

    public EmployeeReport GetCurrentEmployeeReport(Employee empl)
    {
        return EmployeeReports.FirstOrDefault(r => r.GetReportStatus() != false);
    }
    
    public void AddSourceReport(Source source)
    {
        _sourceReports.Add(new SourceReport(source));
    }
    
    public SourceReport GetCurrentSourceReport(Source source)
    {
        return _sourceReports.FirstOrDefault(r => r.GetReportStatus() != false);
    }

    public List<Source> GetSources()
    {
        return _sources;
    }
}