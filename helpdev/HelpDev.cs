using System.Management;
using Microsoft.Win32;

public class HelpDev()
{
    public void GetCampoTabela(string tabelaWindows)
    {
        ManagementClass mc = new ManagementClass($"{tabelaWindows}");

        Console.WriteLine($"Propriedades da classe {tabelaWindows}:\n");

        foreach (var prop in mc.Properties)
        {
            Console.WriteLine(prop.Name);
        }

    }

    public void GetValorCampoTabela(string tabelaWindows, string campoTabela)
    {
         var searcher = new ManagementObjectSearcher(
        $"SELECT {campoTabela} FROM {tabelaWindows}");

    foreach (ManagementObject obj in searcher.Get())
    {
        var valor = obj[campoTabela];

        if (valor == null)
        {
            Console.WriteLine($"{campoTabela} ----- NULL");
            continue;
        }

        if (valor is string str)
        {
            Console.WriteLine($"{campoTabela} ----- {str}");
            Console.WriteLine($"TIPO: string");
        }
        else if (valor is UInt64 u64)
        {
            Console.WriteLine($"{campoTabela} ----- {u64}");
            Console.WriteLine($"TIPO: UInt64");

            // Exemplo extra: converter para GB se for Capacity
            Console.WriteLine($"{campoTabela} (GB) ----- {u64 / 1024.0 / 1024 / 1024:F2}");
        }
        else if (valor is UInt32 u32)
        {
            Console.WriteLine($"{campoTabela} ----- {u32}");
            Console.WriteLine($"TIPO: UInt32");
        }
        else if (valor is int i)
        {
            Console.WriteLine($"{campoTabela} ----- {i}");
            Console.WriteLine($"TIPO: int");
        }
        else if (valor is bool b)
        {
            Console.WriteLine($"{campoTabela} ----- {b}");
            Console.WriteLine($"TIPO: bool");
        }
        else if (valor is Array array)
        {
            Console.WriteLine($"{campoTabela} ----- Array:");
            Console.WriteLine($"TIPO: Array");
            foreach (var item in array)
            {
                Console.WriteLine($"   - {item}");
            }
        } else if (valor is UInt16 u16)
        {
            Console.WriteLine($"{campoTabela} ----- {u16}");
            Console.WriteLine($"TIPO: UInt16");
        }
        else
        {
            Console.WriteLine($"{campoTabela} ----- {valor} (Tipo: {valor.GetType()})");
        }
    }
    }

    public void GetInfoCaminho(RegistryKey root, string caminho)
    {
        using (RegistryKey key = root.OpenSubKey(caminho))
        {
            if (key == null)
            {
                Console.WriteLine("Chave não encontrada.");
                return;
            }

            Console.WriteLine($"===== CAMINHO: {caminho} =====");
            Console.WriteLine();

            foreach (string subKeyName in key.GetSubKeyNames())
            {
                using (RegistryKey subKey = key.OpenSubKey(subKeyName))
                {
                    if (subKey == null) continue;

                    Console.WriteLine($"--- SUBKEY: {subKeyName} ---");

                    foreach (string valueName in subKey.GetValueNames())
                    {
                        object value = subKey.GetValue(valueName);

                        Console.WriteLine($"{valueName} : {value}");
                    }

                    Console.WriteLine();
                }
            }
        }
    }





}