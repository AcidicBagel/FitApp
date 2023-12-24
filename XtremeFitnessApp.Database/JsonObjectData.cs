using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace FitApp.Database
{
    public class JsonObjectData
    {
        private readonly string[] FileNames;

        public JsonObjectData(string[] fileNames)
        {
            FileNames = fileNames;
        }

        public List<T> GetObject<T>(int fileKey)
        {
            string rawJson = File.ReadAllText(FileNames.ElementAtOrDefault(fileKey) ?? "[empty]");    
            return JsonSerializer.Deserialize<List<T>>(rawJson)
                ?? throw new Exception("Mismatched database");
        }
    }
}