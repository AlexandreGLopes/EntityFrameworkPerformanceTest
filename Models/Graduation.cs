namespace EFPerformanceTest.Models;

public sealed class Graduation
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required DateOnly AccomplishDate { get; set; }

    public List<Person> Persons { get; set; }
}
