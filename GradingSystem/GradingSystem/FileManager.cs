using GradingSystem;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

internal class FileManager
{
    private const string FilePath = "classes.json";

    public void Save(List<Class> classes)
    {
        string json = JsonSerializer.Serialize(classes,
            new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }

    public List<Class> Load()
    {
        if (!File.Exists(FilePath))
        {
            return new List<Class>();
        }
        string json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<Class>>(json) ?? new List<Class>();
    }
}
