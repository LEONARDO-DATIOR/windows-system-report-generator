using System;
using System.Management;
using Microsoft.Win32;



HelpDev hpdev = new HelpDev();


hpdev.GetInfoCaminho(Registry.LocalMachine, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
Console.WriteLine("----------------------------------------------------");
hpdev.GetValorCampoTabela("Win32_PhysicalMemory", "TypeDetail");
Console.WriteLine("----------------------------------------------------");
Console.WriteLine("----------------------------------------------------");
Console.WriteLine("----------------------------------------------------");


// var memoryService = new MemoryInfoService();
// memoryService.ExibirInformacoes();
// Console.WriteLine("\n");
// var cpuService = new CpuInfoService();
// cpuService.ExibirInformacoes();
// Console.WriteLine("\n");
// var diskService = new DiskInfoService();
// diskService.ExibirInformacoes();
// Console.WriteLine("\n");
// Console.WriteLine("\n");
// var softwareService = new SoftwareInfoService();
// softwareService.ExibirSoftwaresInstalados();