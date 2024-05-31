using BenchmarkDotNet.Attributes;
using EFPerformanceTest.Data;
using EFPerformanceTest.Models;
using Microsoft.EntityFrameworkCore;

namespace EFPerformanceTest.Benchmarks;

[MemoryDiagnoser]
public class EFPerformanceBenchmarks
{
    private readonly AppDbContext _context;

    public EFPerformanceBenchmarks()
    {
        _context = new AppDbContext();
    }

    [GlobalSetup]
    public void Setup()
    {
        _context.Database.Migrate();
    }

    [Benchmark]
    public void AddPerson()
    {
        var person = new Person
        {
            Name = "John Doe",
            Age = 30,
            Graduations =
            [
                new()
                {
                    Name = "Psychology",
                    AccomplishDate = new DateOnly(2010, 05, 21)
                }
            ]
        };
        _context.Person.Add(person);
        _context.SaveChanges();

        var person2 = new Person
        {
            Name = "Alexandre Lopes",
            Age = 35,
            Graduations =
            [
                new()
                {
                    Name = "Psychology",
                    AccomplishDate = new DateOnly(2010, 05, 21)
                },
                new()
                {
                    Name = "Systems Development",
                    AccomplishDate = new DateOnly(2020, 01, 31)
                }
            ]
        };
        _context.Person.Add(person2);
        _context.SaveChanges();
    }

    [Benchmark]
    public void QueryPersonOverTwentyYearsOld()
    {
        var personList = _context.Person.Where(p => p.Age > 20).ToList();

        ConsoleWriteResultsOverTwenty(personList);
    }

    [Benchmark]
    public void QueryPersonsIncludeWithFilterInside()
    {
        var personList =
            _context.Person
                .AsNoTracking()
                .Include(p => p.Graduations
                    .Where(g => g.Name == "Systems Development"))
                .ToList();

        ConsoleWriteresults(personList);
    }

    [Benchmark]
    public void QueryPersonsWithoutIncludeAndAddGraduationAfter()
    {
        var personList =
            _context.Person
                .AsNoTracking()
                .ToList();

        foreach (var person in personList)
        {
            var filteredGraduations = _context.Entry(person)
                .Collection(p => p.Graduations)
                .Query()
                .Where(g => g.Name == "Systems Development")
                .ToList();

            person.Graduations = filteredGraduations;

            ConsoleWritePersonResults(person);
        }
    }

    [Benchmark]
    public void QueryPersonsProjectIncludesInsideNewObject()
    {
        var personList =
            _context.Person
                .AsNoTracking()
                .Select(p => new Person
                {
                    Id = p.Id,
                    Name = p.Name,
                    Age = p.Age,
                    AddressId = p.AddressId,
                    Address = p.Address,
                    Graduations = p.Graduations
                        .Where(g => g.Name == "Systems Development")
                        .ToList()
                })
                .ToList();

        ConsoleWriteresults(personList);
    }

    private static void ConsoleWriteResultsOverTwenty(List<Person> personList)
    {
        //Descomente o método abaixo quando for verificar os dados dos registros retornados no console ou no log

        //Console.WriteLine($"Pessoas com mais de 20 anos: {personList.Count}");
        //foreach (var person in personList)
        //{
        //    Console.WriteLine($"Id: {person.Id}, Nome: {person.Name}, Idade: {person.Age}");
        //}
    }

    private static void ConsoleWriteresults(List<Person> personList)
    {
        //Descomente o método abaixo quando for verificar os dados dos registros retornados no console ou no log

        //foreach (var person in personList)
        //{
        //    ConsoleWritePersonResults(person);
        //}
    }

    private static void ConsoleWritePersonResults(Person? person)
    {
        //Descomente o método abaixo quando for verificar os dados dos registros retornados no console ou no log

        //Console.WriteLine($"Id: {person.Id}, Nome: {person.Name}, Idade: {person.Age}");
        //foreach (var graduation in person.Graduations)
        //{
        //    Console.WriteLine($"Graduation: {graduation.Name}, Date: {graduation.AccomplishDate}");
        //}
    }
}
