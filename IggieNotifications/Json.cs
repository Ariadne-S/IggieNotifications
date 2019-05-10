using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace IggieNotifications
{
    public static class Json
    {
        public static void WriteJson<T>(T data)
        {
            var json = JsonConvert.SerializeObject(data);
            System.IO.File.WriteAllText(
                $@"C:\Projects\C_Sharp\IggieNotifications\{nameof(T)}.json", json);
        }

        public static void WriteJson<T>(T data, string fileName)
        {
            var json = JsonConvert.SerializeObject(data);
            System.IO.File.WriteAllText(fileName, json);
        }

        public static T ReadJson<T>()
        {
            var jsonFile = System.IO.File.ReadAllText(
                $@"C:\Projects\C_Sharp\IggieNotifications\{nameof(T)}.json");
            return JsonConvert.DeserializeObject<T>(jsonFile);
        }

        public static bool FileExists<T>()
        {
            return System.IO.File.Exists($@"C:\Projects\C_Sharp\IggieNotifications\{nameof(T)}.json");
        }

        public static bool FileExists<T>(string fileName)
        {
            return System.IO.File.Exists(fileName);
        }

    }
}
