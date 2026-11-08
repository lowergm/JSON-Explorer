using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace JSON_Explorer;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("======== JSON Explorer ========\n\n\n");
        
        // Ler o diretorio do arquivo para extrair os dados
        Console.Write("Diretorio do arquivo JSON: ");
        string diretorio = Console.ReadLine();
        
        // Extrair o conteudo do arquivo para conversão
        string json_conteudo = File.ReadAllText(diretorio);
        
        //Console.WriteLine(json_conteudo);
        
        using (JsonDocument document = JsonDocument.Parse(json_conteudo))
        {
            JsonElement pai = document.RootElement;
            Console.WriteLine("> raiz");
            
            ler_json(pai, "");
        }
    }
    
    static void ler_json(JsonElement elemento, string identacao)
    {
        switch (elemento.ValueKind)
        {
            case JsonValueKind.Object:
                foreach (JsonProperty filho in elemento.EnumerateObject())
                {
                    Console.WriteLine(identacao + "> " + filho.Name);
                    ler_json(filho.Value, identacao + "    ");
                }
                break;
            
            case JsonValueKind.Array:
                foreach (JsonElement filho in elemento.EnumerateArray())
                {
                    ler_json(filho, identacao + "    ");
                }
                break;
                
            default:
                Console.WriteLine(identacao + "> " + elemento.ToString());
                break;
        }
    }
}