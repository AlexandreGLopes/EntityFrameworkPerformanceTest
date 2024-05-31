namespace EFPerformanceTest.Models;

public sealed class Person
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required int Age { get; set; }
    public int? AddressId { get; set; }
    public Address? Address { get; set; }

    public List<Graduation> Graduations { get; set; }
}
