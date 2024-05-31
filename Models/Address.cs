namespace EFPerformanceTest.Models;

public sealed class Address
{
    public int Id { get; set; }
    public required string Street {  get; set; }
    public required int Number{  get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string Zipcode { get; set; }
}
