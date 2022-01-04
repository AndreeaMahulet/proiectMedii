using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using project.Models;

namespace project.Data
{
    public class DbInitializer
    {
        public static void Initialize(CompanyContext context)
        {
            context.Database.EnsureCreated();
            if (context.Employees.Any())
            {
                return; // BD a fost creata anterior
            }
            var employees = new Employee[]
            {
 new Employee{Name="Cipariu",Surname="Alexandra",Experience=2,HireDate=DateTime.Parse("2019-09-01")},
 new Employee{Name="Popa",Surname="Ion",Experience=5,HireDate=DateTime.Parse("2016-04-11")},
 new Employee{Name="Iuliu",Surname="Ivona",Experience=10,HireDate=DateTime.Parse("2011-12-03")},
 new Employee{Name="Chesovean",Surname="Marian",Experience=6,HireDate=DateTime.Parse("2015-06-05")},
 new Employee{Name="Ciocan",Surname="Maria",Experience=3,HireDate=DateTime.Parse("2019-12-03")},
 new Employee{Name="Paun",Surname="Toni",Experience=5,HireDate=DateTime.Parse("2016-02-03")},

            };
            foreach (Employee e in employees)
            {
                context.Employees.Add(e);
            }
            context.SaveChanges();
            var department = new Department[]
            {

 new Department{DepartmentID=1,Name="Testing"},
 new Department{DepartmentID=2,Name="PHP"},
 new Department{DepartmentID=3,Name="Javascript"},
 new Department{DepartmentID=4,Name="Java"},
 new Department{DepartmentID=5,Name="Testing"},
 };
            foreach (Department d in department)
            {
                context.Departments.Add(d);
            }
            context.SaveChanges();

            var manager = new Manager[]
            {
 new Manager{EmployeeID=1,DepartmentID=1,ManagerSinceDate=DateTime.Parse("2012-09-01")},
 new Manager{EmployeeID=3,DepartmentID=2,ManagerSinceDate=DateTime.Parse("2011-11-21")},
 new Manager{EmployeeID=1,DepartmentID=3,ManagerSinceDate=DateTime.Parse("2016-06-14")},
 new Manager{EmployeeID=2,DepartmentID=4,ManagerSinceDate=DateTime.Parse("2018-07-18")},
            };
            foreach (Manager m in manager)
            {
                context.Managers.Add(m);
            }
            context.SaveChanges();

            var technology = new Technology[]
            {
 new Technology{Name="Hybris"},
 new Technology{Name="Ruby on Rails"},
 new Technology{Name="Selenium"},
            };
            foreach (Manager m in manager)
            {
                context.Managers.Add(m);
            }
            context.SaveChanges();

            var client = new Client[]
 {

 new Client{ClientName="WOOD STEEL CONSTRUCT SRL",Adress="Str. Pandurii, nr.12-14, Cluj-Napoca"},
 new Client{ClientName="ABC CAR SRL",Adress="Str. Plopilor, nr. 25, Satu-Mare"},
 new Client{ClientName="GEMIX INSTAL SRL",Adress="Str. Avram Iancu, nr.12, bloc A, Oradea"},
 };
            foreach (Client c in client)
            {
                context.Clients.Add(c);
            }
            context.SaveChanges();

            var employeeForClient = new EmplyeesForClient[]
            {
 new EmplyeesForClient { EmployeeID = employees.Single(c => c.Name == "Cipariu" ).ID, ClientID = client.Single(i => i.ClientName =="WOOD STEEL CONSTRUCT SRL").ID},
 new EmplyeesForClient { EmployeeID = employees.Single(c => c.Name == "Popa" ).ID, ClientID = client.Single(i => i.ClientName =="WOOD STEEL CONSTRUCT SRL").ID},
 new EmplyeesForClient { EmployeeID = employees.Single(c => c.Name == "Chesovean" ).ID, ClientID = client.Single(i => i.ClientName =="ABC CAR SRL").ID},
 new EmplyeesForClient { EmployeeID = employees.Single(c => c.Name == "Ciocan" ).ID, ClientID = client.Single(i => i.ClientName =="GEMIX INSTAL SRL").ID},
 new EmplyeesForClient { EmployeeID = employees.Single(c => c.Name == "Iuliu" ).ID, ClientID = client.Single(i => i.ClientName =="GEMIX INSTAL SRL").ID},
 new EmplyeesForClient { EmployeeID = employees.Single(c => c.Name == "Paun" ).ID, ClientID = client.Single(i => i.ClientName =="GEMIX INSTAL SRL").ID},
            };
            foreach (EmplyeesForClient ec in employeeForClient)
            {
                context.EmplyeesForClients.Add(ec);
            }
            context.SaveChanges();
        }

    }
}
