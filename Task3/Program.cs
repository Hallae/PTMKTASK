class Program
{
    static void Main(string[] args)
    {
        List<Employee> employees = GetEmployees(); // Fetching employees
        foreach (var employee in employees)
        {
            // Constructing full name
            string fullName = $"{employee.FirstName} {employee.Patronymic} {employee.LastName}";

            // Handling null DateOfBirth
            int age = 0;
            if (employee.DateOfBirth.HasValue)
            {
                age = employee.CalculateAge(employee.DateOfBirth.Value);
            }

            Console.WriteLine($"{fullName}, {employee.Gender}, Age: {age}");
        }
    }

    static List<Employee> GetEmployees()
    {
   
        return new List<Employee>
        {
            new Employee { FirstName = "John", Patronymic = "Doestovicha", LastName = "Smith", Gender = "Male", DateOfBirth = new DateTime(1980, 1, 1) },
            new Employee { FirstName = "Jane", Patronymic = "Avanavich.", LastName = "Doe", Gender = "Female", DateOfBirth = new DateTime(1975, 12, 31) }
        };
    }
}



