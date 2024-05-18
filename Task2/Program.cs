using PTMKTASK.Models;

class Program
{
    static void Main(string[] args)
    {
        using (var context = new EmployeeContext())
        {
            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Patronymic: ");
            string patronymic = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Enter Date of Birth (YYYY/MM/DD): ");
            DateTime dob = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Gender (Male/Female): ");
            string gender = Console.ReadLine();

            var employee = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                Patronymic = patronymic,
                DateOfBirth = dob,
                Gender = gender
            };

            // Calculate age
            int age = employee.CalculateAge(dob);

            Console.WriteLine($"Age: {age}");

            // Save to database
            context.Employees.Add(employee);
            context.SaveChanges();

            Console.WriteLine("Employee saved successfully!");
        }
    }
}
