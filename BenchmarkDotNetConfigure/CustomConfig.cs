using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Validators;
using BenchmarkDotNet.Diagnosers;
using Perfolizer.Horology;

namespace EFPerformanceTest.BenchmarkDotNetConfigure;

public class CustomConfig : ManualConfig
{
    public CustomConfig()
    {
        // Adiciona as colunas padrão
        AddColumnProvider(DefaultColumnProviders.Instance);

        // Usa o exportador padrão
        AddExporter(DefaultExporters.Csv);
        AddExporter(DefaultExporters.Html);
        AddExporter(DefaultExporters.Markdown);

        // Usa validadores padrão
        AddValidator(JitOptimizationsValidator.FailOnError);
        AddValidator(ExecutionValidator.FailOnError);

        // Adiciona diagnósticos padrão
        //AddDiagnoser(DefaultDiagnosers.Instance);

        // Configura o job padrão
        AddJob(Job.Default.WithIterationTime(new TimeInterval(100)).WithMinIterationCount(10));

        // Configura a unidade de tempo para milissegundos
        SummaryStyle = SummaryStyle.Default.WithTimeUnit(TimeUnit.Millisecond);
    }
}
