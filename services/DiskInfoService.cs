using System;
using System.Management;

public class DiskInfoService
{
    public void ExibirInformacoes()
    {
        Console.WriteLine("===== INFORMAÇÕES DOS DISCOS =====");

        foreach (DriveInfo drive in DriveInfo.GetDrives())
        {
            if (!drive.IsReady) continue;

            double totalGB = drive.TotalSize / 1024.0 / 1024.0 / 1024.0;
            double freeGB = drive.TotalFreeSpace / 1024.0 / 1024.0 / 1024.0;
            double usedGB = totalGB - freeGB;

            Console.WriteLine($"Nome do Volume: {drive.Name}");
            Console.WriteLine($"Espaço Total: {totalGB:F2} GB");
            Console.WriteLine($"Espaço Usado: {usedGB:F2} GB");
            Console.WriteLine($"Espaço Livre: {freeGB:F2} GB");
            Console.WriteLine("----------------------------------");
        }
       
    }

}