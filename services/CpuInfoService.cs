using System;
using System.Management;
using System.Text.RegularExpressions;

public class CpuInfoService
{
    public void ExibirInformacoes()
    {
        var searcher = new ManagementObjectSearcher("SELECT Name, Manufacturer, NumberOfCores, NumberOfLogicalProcessors, MaxClockSpeed FROM Win32_Processor");

        foreach (ManagementObject obj in searcher.Get())
        {
            string name = obj["Name"]?.ToString() ?? "Desconhecido";
            string manufacturer = obj["Manufacturer"]?.ToString() ?? "Desconhecido";
            int cores = Convert.ToInt32(obj["NumberOfCores"]);
            int threads = Convert.ToInt32(obj["NumberOfLogicalProcessors"]);
            int maxClock = Convert.ToInt32(obj["MaxClockSpeed"]);

            string generation = DetectGenerationFromName(name);

            Console.WriteLine("===== INFORMAÇÕES DA CPU =====");
            Console.WriteLine($"Nome: {name}");
            Console.WriteLine($"Fabricante: {manufacturer}");
            Console.WriteLine($"Núcleos físicos: {cores}");
            Console.WriteLine($"Threads: {threads}");
            Console.WriteLine($"Frequência Máxima: {maxClock} MHz");
            Console.WriteLine($"Geração: {generation}");
            Console.WriteLine();

            return; // normalmente só existe 1 CPU
        }

        Console.WriteLine("CPU não encontrada.");
    }

    private string DetectGenerationFromName(string cpuName)
    {
        if (string.IsNullOrWhiteSpace(cpuName))
            return "Desconhecida";

        // Detecta padrão Intel tipo i7-10700K
        var match = Regex.Match(cpuName, @"-(\d{4,5})");

        if (match.Success)
        {
            var digits = match.Groups[1].Value;

            if (digits.Length >= 4)
                return $"{digits[0]}ª Geração";
        }

        return "Não identificada";
    }
}