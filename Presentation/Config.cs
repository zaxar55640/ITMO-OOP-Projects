using ClassLibrary1.Business;
using ClassLibrary1.DataAccess;


        Service service = new Service(new DataBase(), "C:\\Users\\Захар\\Documents\\GitHub\\zaxar55640\\Presentation\\data.json"); //.Restore();
        service.AddSource(new Source("whatsapp"));
        service.AddSource(new Source("mail"));
        service.SourceStartSession(service.GetSources().FirstOrDefault(s => s._name == "whatsapp"));
        service.SourceStartSession(service.GetSources().FirstOrDefault(s => s._name == "mail"));
        service.HandleMessage("123", service.GetSources().First(s => s._name == "whatsapp"));
        service.HandleMessage("123", service.GetSources().First(s => s._name == "mail"));
        Console.WriteLine("Do you want to signup(1) or login(2)?");
        string ans = Console.ReadLine();
        Console.WriteLine("Write username:");
        string name = Console.ReadLine();
        Console.WriteLine("Write password:");
        string psw = Console.ReadLine();
        Employee employee = null;
        Director director = null;
        Client client = null;
        if (ans == "1")
        {
            Console.WriteLine("What kind of account you want to create?(Employee/Director/Client)");
            string a = Console.ReadLine();
            if (a == "Employee")
            {
                    service.CreateAccount(name, psw, AccountType.Employee);
                    employee = service.loginEmp(name, psw);
                    Console.WriteLine("Account created!");
            }

            if (a == "Director")
            {
                    service.CreateAccount(name, psw, AccountType.Director);
                    director = service.loginDir(name, psw);
            }
                
            if (a == "Client")
            {
                    service.CreateAccount(name, psw, AccountType.Client);
                    client = service.loginClient(name, psw);
            }
        }
        Console.WriteLine("assada");
        string type = service.LoginAuthentication(name, psw);
            switch (type)
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
                        Console.WriteLine("Choose source where you want to send message. Or 0 to exit");
                        foreach (var src in service.GetSources())
                        {
                            Console.WriteLine($"{count} {src._name}");
                            count++;
                        }
                        int b = Convert.ToInt32(Console.ReadLine());
                        if (b == 0)
                            break;
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
                        string number = Console.ReadLine();
                        switch (number)
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
                                string source = Console.ReadLine();
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
                                int numbber = Convert.ToInt32(Console.ReadLine());
                                EmployeeReport er = service.GetEmployeeReports().ElementAt(numbber - 1);
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
                    string check;
                    Console.WriteLine(@"Welcome! Do you want to start session?(Y/N)");
                    check = Console.ReadLine();

                        while (true)
                    {
                        Console.WriteLine("If you want to exit press E, C to continue"); 
                        if (Console.ReadLine() == "E")
                            break;
                        service.EmployeeStartSession(employee);
                        int count = 1;
                        Console.WriteLine("Choose source to work with or 0 to exit:");
                        foreach (var src in service.GetSources())
                        {
                            Console.WriteLine($"{count} {src._name}");
                            count++;
                        }
                        int aaanswer = Convert.ToInt32(Console.ReadLine());
                        if (count == 0)
                            break;
                        Source source = employee.GetSources().ElementAt(aaanswer - 1);
                        count = 1;
                        while (true)
                        {
                            Console.WriteLine("Choose message to answer or (C) to close session:");
                            service.ShowMessagesBySource(source).ForEach(m => Console.WriteLine($"{count}{m.GetMessage()}"));
                            string abc = Console.ReadLine();
                            if (abc == "C")
                                break;
                            count = Convert.ToInt32(abc);
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

        
