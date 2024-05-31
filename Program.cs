using BenchmarkDotNet.Running;
using EFPerformanceTest.Benchmarks;

BenchmarkRunner.Run<EFPerformanceBenchmarks>();

// Descomente o codigo abaixo para verificar os dados retornados pelas querys usando o comando dotnet run para rodar a aplicação

//using (var writer = new StreamWriter("output.log"))
//{
//    Console.SetOut(writer);
//    Console.SetError(writer);

//    Console.WriteLine("Esta mensagem será escrita no arquivo de log.");

//    var benchmarks = new EFPerformanceBenchmarks();
//    benchmarks.Setup();

//    benchmarks.AddPerson();
//    benchmarks.QueryPersonOverTwentyYearsOld();
//    benchmarks.QueryPersonsIncludeWithFilterInside();
//    benchmarks.QueryPersonsWithoutIncludeAndAddGraduationAfter();
//    benchmarks.QueryPersonsProjectIncludesInsideNewObject();
//}