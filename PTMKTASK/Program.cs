using PTMKTASK.Models;

namespace PTMKTASK.Models
{
    internal class Program
    {
        static void Main(string[] args)
        {

            using (var context = new EmployeeContext())
            {
                var newEmployee = new Employee
                {
                    FirstName = "",
                    Patronymic = "",
                    LastName = "",
                    DateOfBirth = null,
                    Gender = ""
                };
                context.Employees.Add(newEmployee);
                context.SaveChanges();
            }
            Console.WriteLine("Table Created succuesslly!");
        }
    }
}
