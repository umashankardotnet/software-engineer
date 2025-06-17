namespace csharp_practice
{
    // https://medium.com/@alexvc/net-c-fundamentals-for-senior-devs-a4c46cec2d93
    internal class ObjectAssignmenta
    {
       static void Main()
        {
            Employee employee = new Employee() { Id = 1, Name = "bhanu" };
            Employee employee2 = employee;
            employee.Name = "Priya";
            employee = null; // its just removing the refence of this variable from actual object employee
            Console.WriteLine(employee2);
            Console.WriteLine(employee2.Name);
        }
    }
    class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
