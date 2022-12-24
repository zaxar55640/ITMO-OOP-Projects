using ClassLibrary1.Business;
using ClassLibrary1.DataAccess;


internal static class EmployeeAppa
{
    public static void Main(string[] args)
    {
        Console.Read();
        Service service = new Service(new DataBase(), "Presentation/data.json"); //.Restore();
        Console.WriteLine("Do you want to signup(1) or login(2)?");
        string a = Console.ReadLine();
        Console.WriteLine("Write username:");
        string name = Console.ReadLine();
        Console.WriteLine("Write password:");
        string psw = Console.ReadLine();
        Employee employee = null;
        Director director = null;
        Client client = null;
        if (a == "1")
        {
            Console.WriteLine("What kind of account you want to create?(Employee/Director/Client)");
            a = Console.ReadLine();
            switch (a)
            {
                case "Employee":
                {
                    service.CreateAccount(name, psw, AccountType.Employee);
                    employee = service.loginEmp(name, psw);
                    break;
                }

                case "Director":
                {
                    service.CreateAccount(name, psw, AccountType.Director);
                    director = service.loginDir(name, psw);
                    break;
                }
                
                case "Client":
                {
                    service.CreateAccount(name, psw, AccountType.Client);
                    client = service.loginClient(name, psw);
                    break;
                }
            }
        }
        a = service.LoginAuthentication(name, psw);
            switch (a)
            {
                case "client":
                {
                    while (true)
                    {
                        Console.WriteLine("If you want to exit press E, C to continue");
                        if (Console.ReadLine() == "E")
                            break;
                        int count = 1;
                        client = service.loginClient(name, psw);
                        Console.WriteLine("Choose source where you want to send message.");
                        service.GetSources().ForEach(sr =>
                        {
                            Console.WriteLine($"{count}. {sr.GetSourceName()}");
                            count++;
                        });
                        int b = Convert.ToInt32(Console.ReadLine());
                        Source sr = service.GetSources().ElementAt(b);
                        Console.WriteLine("Write your message:");
                        service.ClientSendMessage(client, Console.ReadLine(), sr);
                    }
                    break;
                }
                    
                case "director":
                {
                    while (true)
                    { 
                        Console.WriteLine("If you want to exit press E, C to continue"); 
                        if (Console.ReadLine() == "E")
                            break; 
                        director = service.loginDir(name, psw);
                        Console.WriteLine(@"Welcome. What you want to see?
                        1. Summary Report
                        2. Source Report
                        3. Employee Report");
                        a = Console.ReadLine();
                        switch (a)
                        {
                            case "1":
                            {
                                SummaryReport sr = service.GetSummaryReport();
                                Console.WriteLine(@$"Total amount of messages: {sr.GetAmountMessage()};
                                Amount of completed messages: {sr.GetAmountDoneMessage()}.");
                                break;
                            }

                            case "2":
                            {
                                int count = 1;
                                Console.WriteLine("Which source?");
                                service.GetSourceReports()
                                    .ForEach(sr =>
                                    {
                                        Console.WriteLine($"{count}. {sr.GetSource()._name}");
                                        count += 1;
                                    });
                                a = Console.ReadLine();
                                SourceReport sr = service.GetSourceReports().ElementAt(count - 1);
                                Console.WriteLine(@$"Total amount of messages: {sr.GetReceivedMessageAmount()}
                                Amount completed messages: {sr.GetDoneMessageAmount()}.");
                                break;
                            }
                            
                            case "3":
                            {
                                int count = 1;
                                service.GetEmployeeReports().ForEach(er =>
                                {
                                    Console.WriteLine($"{count}. {er.GetEmployee().GetUserName()}");
                                    count++;
                                });
                                a = Console.ReadLine();
                                EmployeeReport er = service.GetEmployeeReports().ElementAt(count - 1);
                                Employee emp = er.GetEmployee();
                                int totalAmount = emp.GetSources().Sum(s => s.GetMessages().Count);
                                Console.WriteLine(@$"Total amount of messages: {totalAmount}
                                Amount completed messages: {er.GetAmountServed()}.");
                                break;
                        }
                    }
                    }
                    break;
                }
                    
                case "employee":
                {
                    employee = service.loginEmp(name, psw);
                    while (a != "Y")
                    {
                        Console.WriteLine(@"Welcome! Do you want to start session?(Y/N)");
                        a = Console.ReadLine();
                    }

                    while (true)
                    {
                        Console.WriteLine("If you want to exit press E, C to continue"); 
                        if (Console.ReadLine() == "E")
                            break;
                        service.EmployeeStartSession(employee);
                        int count = 1;
                        Console.WriteLine("Choose source to work with:");
                        employee.GetSources().ForEach(s => Console.WriteLine($"{count}. {s._name}"));
                        count = Convert.ToInt32(Console.ReadLine());
                        Source source = employee.GetSources().ElementAt(count - 1);
                        count = 1;
                        while (true)
                        {
                            Console.WriteLine("Choose message to answer or (C) to close session:");
                            service.ShowMessagesBySource(source).ForEach(m => Console.WriteLine($"{count}{m.GetMessage()}"));
                            a = Console.ReadLine();
                            if (a == "C")
                                break;
                            count = Convert.ToInt32(a);
                            Message msg = service.ShowMessagesBySource(source).ElementAt(count - 1);
                            service.AnswerMessage(source, employee, Console.ReadLine(), msg);
                        }
                        service.EmployeeCloseSession(employee);
                    }
                    
                    break;
                }

                default:
                {
                    Console.WriteLine("Wrong username or password");
                    break;
                }
            }
            service.Save();
        }
        
    }