using Microsoft.Win32;
using System;

public class SoftwareInfoService
{
    public void ExibirSoftwaresInstalados()
    {
        Console.WriteLine("===== SOFTWARES INSTALADOS (FILTRADOS) =====");

        ListarSoftwares(Registry.LocalMachine, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
        ListarSoftwares(Registry.LocalMachine, @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall");

        Console.WriteLine();
    }

    private void ListarSoftwares(RegistryKey root, string caminho)
    {
        using (RegistryKey key = root.OpenSubKey(caminho))
        {
            if (key == null) return;

            foreach (string subKeyName in key.GetSubKeyNames())
            {
                using (RegistryKey subKey = key.OpenSubKey(subKeyName))
                {
                    string nome = subKey?.GetValue("DisplayName") as string;
                    string publisher = subKey?.GetValue("Publisher") as string;
                    string uninstall = subKey?.GetValue("UninstallString") as string;

                    object systemComponent = subKey?.GetValue("SystemComponent");

                    // FILTROS IMPORTANTES
                    if (string.IsNullOrWhiteSpace(nome)) continue;
                    if (string.IsNullOrWhiteSpace(uninstall)) continue;
                    if (systemComponent != null && (int)systemComponent == 1) continue;
                    if (nome.Contains("Update")) continue;
                    if (nome.Contains("Security")) continue;
                    if (nome.Contains("Hotfix")) continue;
                    if (nome.Contains("Driver")) continue;
                    if (nome.Contains("NVIDIA")) continue;
                    if (nome.Contains("x64")) continue;
                    if (nome.Contains("x86")) continue;

                    Console.WriteLine($"Nome: {nome}");
                    Console.WriteLine($"Fornecedor: {publisher ?? "Desconhecido"}");
                    Console.WriteLine("----------------------------------");
                }
            }
        }
    }
}