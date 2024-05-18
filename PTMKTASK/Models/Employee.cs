public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string Patronymic { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }

    public int CalculateAge(DateTime DateOfBirth) //calculates the age of the employee
    {
        var today = DateTime.Today;
        var age = today.Year - DateOfBirth.Year;
        if (DateOfBirth.Date > today.AddYears(-age)) age--;
        return age;
    }
}
