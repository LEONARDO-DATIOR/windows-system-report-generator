using System;
using System.Management;

public class DiskInfoService
{
    public void ExibirInformacoes()
    {
        var searcher = new ManagementObjectSearcher(
            "SELECT Model, InterfaceType, Size, SerialNumber FROM Win32_DiskDrive");

        Console.WriteLine("===== INFORMAÇÕES DOS DISCOS =====");

        foreach (ManagementObject obj in searcher.Get())
        {
            string model = obj["Model"]?.ToString() ?? "Desconhecido";

            ulong sizeBytes = obj["Size"] != null 
                ? Convert.ToUInt64(obj["Size"]) 
                : 0;

            double sizeGB = sizeBytes / 1024.0 / 1024.0 / 1024.0;

            string diskType = DetectDiskType(model);

            Console.WriteLine($"Modelo: {model}");
            Console.WriteLine($"Tipo: {diskType}");
            Console.WriteLine($"Tamanho: {sizeGB:F2} GB");
            Console.WriteLine("----------------------------------");
        }

        Console.WriteLine();
    }

    private string DetectDiskType(string model)
    {
        if (string.IsNullOrWhiteSpace(model))
            return "Desconhecido";

        string lowerModel = model.ToLower();

        if (lowerModel.Contains("ssd"))
            return "SSD";

        if (lowerModel.Contains("nvme"))
            return "NVMe (SSD)";

        return "HDD ou Não identificado";
    }
}