using System.Management;

public class MemoryInfoService
{
    public void ExibirInformacoes()
    {
        var total = GetTotalMemory();
        var tipo = GetMemoryType();
        var modulos = GetModuleCount();

        Console.WriteLine($"Total RAM: {total / 1024 / 1024 / 1024} GB");
        Console.WriteLine($"Tipo: {tipo}");
        Console.WriteLine($"Módulos: {modulos}");
    }

    private ulong GetTotalMemory()
    {
        ulong total = 0;

        var searcher = new ManagementObjectSearcher("SELECT Capacity FROM Win32_PhysicalMemory");

        foreach (ManagementObject obj in searcher.Get())
        {
            if (obj["Capacity"] is ulong capacity)
                total += capacity;
        }

        return total;
    }

    private int GetModuleCount()
    {
        int count = 0;

        var searcher = new ManagementObjectSearcher("SELECT Capacity FROM Win32_PhysicalMemory");

        foreach (ManagementObject obj in searcher.Get())
        {
            count++;
        }

        return count;
    }

    private string GetMemoryType()
    {
        var searcher = new ManagementObjectSearcher("SELECT SMBIOSMemoryType FROM Win32_PhysicalMemory");

        foreach (ManagementObject obj in searcher.Get())
        {
            var valor = obj["SMBIOSMemoryType"];

            if (valor != null)
            {
                ushort tipo = Convert.ToUInt16(valor);

                return tipo switch
                {
                    20 => "DDR",
                    21 => "DDR2",
                    24 => "DDR3",
                    26 => "DDR4",
                    34 => "DDR5",
                    _ => $"Desconhecido ({tipo})"
                };
            }
        }

        return "Desconhecido";
    }


}