using PTMKTASK.Models;

public class EmployeeGenerator
{
    private static Random random = new Random();

    public static List<Employee> GenerateRandomEmployees(int count)
    {
        var employees = new List<Employee>();

        for (int i = 0; i < count; i++)
        {
            var firstName = $"FirstName{i}";
            var lastName = $"LastName{i}";
            var patronymic = $"Patronymic{i}";
            var gender = $"Gender{i}";

            // Special case for 100 employees with gender "Male" and last names starting with "F"
            if (i >= count - 100)
            {
                firstName = "Male";
                lastName = $"F{random.Next(1, 26)}"; // Generates a random letter after "F"
                patronymic = "Special";
                gender = "male";
            }

            employees.Add(new Employee
            {
                FirstName = firstName,
                Patronymic = patronymic,
                LastName = lastName,
                Gender = random.Next(2) == 0 ? "Female" : "Male", // Uniform gender distribution
                DateOfBirth = new DateTime(random.Next(1980, 2024), random.Next(1, 13), random.Next(1, 28)) // Random date between 1980 and 2024
            });
        }

        return employees;
    }

    public static async Task SendEmployeesToDatabase(List<Employee> employees, EmployeeContext context)
    {
        int batchSize = 100000; 
        for (int i = 0; i < employees.Count; i += batchSize)
        {
            var batch = employees.GetRange(i, Math.Min(batchSize, employees.Count - i));
            await context.Employees.AddRangeAsync(batch);
            await context.SaveChangesAsync();
        }
        Console.WriteLine("Employees sent to the database succussfully");
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            var employees = EmployeeGenerator.GenerateRandomEmployees(1000000); // Generate employees
            using (var context = new EmployeeContext()) 
                await EmployeeGenerator.SendEmployeesToDatabase(employees, context); // Send to the database
            }
        }
    }


