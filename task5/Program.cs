using PTMKTASK.Models;

public class EmployeeGenerator
{
    private static Random random = new Random();

    public static List<Employee> GenerateRandomEmployees(int count)
    {
        var employees = new List<Employee>();
        var startTime = DateTime.Now; // Start measuring time

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

        var endTime = DateTime.Now; // End measuring time
        var elapsedTime = endTime - startTime; // Calculate elapsed time

        Console.WriteLine($"Time taken to generate {count} employees: {elapsedTime.TotalMilliseconds} ms");

        return employees;
    }

    public static async Task SendEmployeesToDatabase(List<Employee> employees, EmployeeContext context)
    {
        int batchSize = 500000;
        var accumulatedChanges = new List<Employee>();

        foreach (var employee in employees)
        {
            accumulatedChanges.Add(employee);
            if (accumulatedChanges.Count >= batchSize || accumulatedChanges.Count == employees.Count)
            {
                context.Employees.AddRange(accumulatedChanges);
                await context.SaveChangesAsync(); // Flush changes to the database
                accumulatedChanges.Clear();
            }
        }

        if (accumulatedChanges.Any()) // Ensure any remaining changes are saved
        {
            await context.SaveChangesAsync();
        }

        Console.WriteLine("Employees sent to the database successfully");
    }

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

///<summary///
///Runtime measurement before optimization : 3900 - 47000ms average with a batch size of 10000
///Runtime measuremtn after optimization: 4000 -4000ms average with a batch size of 500000
///
