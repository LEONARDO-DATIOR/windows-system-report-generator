using System.Management;

public class SystemInfoService
{
    public double GetTotalRamInGb()
    {
        var moSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");

        double totalMemory = 0;

        foreach (ManagementObject obj in moSearcher.Get())
        {
            totalMemory += Convert.ToDouble(obj["Capacity"]);
        }

        return totalMemory / 1024 / 1024 / 1024;
    }


}