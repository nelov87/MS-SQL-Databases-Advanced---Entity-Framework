using System;
using System.Linq;
using System.Text;
using SoftUni.Data;
using SoftUni.Models;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
           
            using (SoftUniContext context = new SoftUniContext())
            {
                string result = RemoveTown(context);
                Console.WriteLine(result);
            }

        }

        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var employees = context.Employees.OrderBy(x => x.EmployeeId);

            StringBuilder sb = new StringBuilder();

            foreach (var emp in employees)
            {
                sb.AppendLine($"{emp.FirstName} {emp.LastName} {emp.MiddleName} {emp.JobTitle} {emp.Salary:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var employees = context.Employees
                .Select(e => new {
                    e.FirstName,
                    e.Salary
                })
                .Where(e => e.Salary > 50000)
                .OrderBy(e => e.FirstName);

            StringBuilder sb = new StringBuilder();

            foreach (var emp in employees)
            {
                sb.AppendLine($"{emp.FirstName} - {emp.Salary:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var employees = context.Employees
                .Where(e => e.Department.Name == "Research and Development")
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    e.Department,
                    e.Salary
                })
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .ToList();

            foreach (var emp in employees)
            {
                sb.AppendLine($"{emp.FirstName} {emp.LastName} from {emp.Department.Name} - ${emp.Salary:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            Address address = new Address
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            context.Addresses.Add(address);

            var nakov = context.Employees.FirstOrDefault(e => e.LastName == "Nakov");
            nakov.Address = address;

            context.SaveChanges();

            var empAddr = context.Employees
                .OrderByDescending(e => e.AddressId)
                .Select(a => a.Address.AddressText)
                .Take(10)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (string addr in empAddr)
            {
                sb.AppendLine($"{addr}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            var employees = context.Employees
                .Where(p => p.EmployeesProjects.Any(s => s.Project.StartDate.Year >= 2001 && s.Project.StartDate.Year <= 2003))
                .Select(x => new
                {
                    EmployeeFullName = x.FirstName + " " + x.LastName,
                    ManagerFullName = x.Manager.FirstName + " " + x.Manager.LastName,
                    Projects = x.EmployeesProjects.Select(p => new
                    {
                        ProjectName = p.Project.Name,
                        StartDate = p.Project.StartDate,
                        EndDate = p.Project.EndDate
                    }).ToList()
                })
                .Take(10)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var emp in employees)
            {
                sb.AppendLine($"{emp.EmployeeFullName} - Manager: {emp.ManagerFullName}");
                foreach (var proj in emp.Projects)
                {
                    var startDate = proj.StartDate.ToString("M/d/yyyy h:mm:ss tt");
                    var endDate = proj.EndDate.HasValue 
                        ? proj.EndDate.Value.ToString("M/d/yyyy h:mm:ss tt")
                        : "not finished";
                    

                    sb.AppendLine($"--{proj.ProjectName} - {startDate} - {endDate}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetAddressesByTown(SoftUniContext context)
        {
            var addresses = context.Addresses
                .OrderByDescending(x => x.Employees.Count)
                .ThenBy(t => t.Town.Name)
                .ThenBy(at => at.AddressText)
                .Select(adr => new
                {
                    AddressText = adr.AddressText,
                    TownName = adr.Town.Name,
                    EmployeesCount = adr.Employees.Count.ToString()
                })
                .Take(10)
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var adr in addresses)
            {
                sb.AppendLine($"{adr.AddressText}, {adr.TownName} - {adr.EmployeesCount} employees");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployee147(SoftUniContext context)
        {
            var employee = context.Employees
                .Where(e => e.EmployeeId == 147)
                .Select(p => new
                {
                    FullName = p.FirstName + " " + p.LastName,
                    JobTitle = p.JobTitle,
                    Projects = p.EmployeesProjects.OrderBy(ep => ep.Project.Name).Select(pr => new
                    {
                        ProjectName = pr.Project.Name
                    }).ToList()
                })
                .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var emp in employee)
            {
                sb.AppendLine($"{emp.FullName} - {emp.JobTitle}");

                foreach (var proj in emp.Projects)
                {
                    sb.AppendLine($"{proj.ProjectName}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            var departments = context.Departments
                .Where(d => d.Employees.Count > 5)
                .OrderBy(d => d.Employees.Count)
                .ThenBy(d => d.Name)
                .Select(d => new
                {
                    DepartmentName = d.Name,
                    MenagerFullName = d.Manager.FirstName + " " + d.Manager.LastName,
                    Employees = d.Employees.Select(e => new
                    {
                        EmployeeFullName = e.FirstName + " " + e.LastName,
                        JobTitle = e.JobTitle
                    })
                    .OrderBy(e => e.EmployeeFullName)
                    .ToList()
                }).ToList();

            foreach (var department in departments)
            {
                sb.AppendLine($"{department.DepartmentName} - {department.MenagerFullName}");

                foreach (var emp in department.Employees)
                {
                    sb.AppendLine($"{emp.EmployeeFullName} - {emp.JobTitle}");
                }
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetLatestProjects(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var projects = context.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10)
                .Select(p => new
                {
                    ProjectName = p.Name,
                    ProjectDescription = p.Description,
                    p.StartDate
                })
                .OrderBy(p => p.ProjectName)
                .ToList();

            foreach (var project in projects)
            {
                sb.AppendLine($"{project.ProjectName}");
                sb.AppendLine($"{project.ProjectDescription}");
                string date = project.StartDate.ToString("M/d/yyyy h:mm:ss tt");
                sb.AppendLine($"{date}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string IncreaseSalaries(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            context.Employees
                .Where(e => e.Department.Name == "Engineering"
                || e.Department.Name == "Tool Design" 
                || e.Department.Name == "Marketing"
                || e.Department.Name == "Information Services")
                .ToList()
                .ForEach(e => e.Salary *= 1.12m);
            context.SaveChanges();

            var employees = context.Employees
                .Where(e => e.Department.Name == "Engineering"
                || e.Department.Name == "Tool Design"
                || e.Department.Name == "Marketing"
                || e.Department.Name == "Information Services")
                .Select(e => new
                {
                    FullName = e.FirstName + " " + e.LastName,
                    e.Salary
                })
                .OrderBy(e => e.FullName)
                .ToList();

            foreach (var emp in employees)
            {
                sb.AppendLine($"{emp.FullName} (${emp.Salary:F2})");
            }

            return sb.ToString().TrimEnd();

        }

        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Where(e => e.FirstName.IndexOf("Sa") == 0)
                .Select(e => new
                {
                    FullName = e.FirstName + " " + e.LastName,
                    JobTitle = e.JobTitle,
                    Salary = e.Salary
                })
                .OrderBy(e => e.FullName)
                .ToList();

            foreach (var emp in employees)
            {
                sb.AppendLine($"{emp.FullName} - {emp.JobTitle} - (${emp.Salary:F2})");
            }

            return sb.ToString().TrimEnd();
        }

        public static string DeleteProjectById(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var project = context.Projects.FirstOrDefault(p => p.ProjectId == 2);

            var employeesProjects = context.EmployeesProjects
                .Where(ep => ep.ProjectId == 2).ToList();

            context.EmployeesProjects.RemoveRange(employeesProjects);

            context.Projects.Remove(project);
            context.SaveChanges();

            var projects = context.Projects
                .Select(p => new
                {
                    p.Name
                }).Take(10)
                .ToList();

            foreach (var proj in projects)
            {
                sb.AppendLine(proj.Name);
            }

            return sb.ToString().TrimEnd();
        }

        public static string RemoveTown(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();
            int addresessCount = 0;

            context.Employees.Where(e => e.Address.Town.Name == "Seattle")
                .ToList()
                .ForEach(e => e.AddressId = null);

            var addresess = context.Addresses.Where(a => a.Town.Name == "Seattle").ToList();

            var town = context.Towns.FirstOrDefault(t => t.Name == "Seattle");

            addresessCount = addresess.Count();

            context.RemoveRange(addresess);

            context.Towns.Remove(town);

            context.SaveChanges();

            return $"{addresessCount} addresses in Seattle were deleted";
        }
    }
}
