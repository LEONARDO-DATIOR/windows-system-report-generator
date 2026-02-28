using System;
using System.Management;



HelpDev hpdev = new HelpDev();


hpdev.GetCampoTabela("Win32_PhysicalMemory");
Console.WriteLine("----------------------------------------------------");
hpdev.GetValorCampoTabela("Win32_PhysicalMemory", "TypeDetail");
Console.WriteLine("----------------------------------------------------");
Console.WriteLine("----------------------------------------------------");
Console.WriteLine("----------------------------------------------------");


var memoryService = new MemoryInfoService();
memoryService.ExibirInformacoes();
Console.WriteLine("\n");
var cpuService = new CpuInfoService();
cpuService.ExibirInformacoes();