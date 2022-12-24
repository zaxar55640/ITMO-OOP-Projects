namespace ClassLibrary1.DataAccess;

public class EmployeeReport
{
    private int _amountServed { get; set; }
    private Employee _empl { get; set; }
    private DateTime _time;
    private bool status { get; set; }

    public EmployeeReport(Employee employee)
    {
        _empl = employee;
        _amountServed = 0;
        _time = DateTime.Now;
        status = true;
    }

    public EmployeeReport CloseReport(int amountServed)
    {
        _amountServed = amountServed;
        status = false;
        return this;
    }

    public bool GetReportStatus()
    {
        return status;
    }

    public Employee GetEmployee()
    {
        return _empl;
    }

    public int GetAmountServed()
    {
        return _amountServed;
    }
}