using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serializer
{
    public class Serializer
    {
        public static T deserialize<T>(string FileName)
        {
            string path = Environment.CurrentDirectory;
            string json = "";
            if (File.Exists(Directory.GetParent(path).Parent.FullName + "\\" + FileName))
                json = File.ReadAllText(Directory.GetParent(path).Parent.FullName + "\\" + FileName);
            else
            {
                FileStream stream = File.Create(Directory.GetParent(path).Parent.FullName + "\\" + FileName + ".json");
                stream.Close();
                json = File.ReadAllText(Directory.GetParent(path).Parent.FullName + "\\" + FileName);
            }
            T workers = JsonConvert.DeserializeObject<T>(json);
            return workers;
        }
        public static void Serialize<T>(string FileName, T list)
        {
            string path = Environment.CurrentDirectory;
            string json = JsonConvert.SerializeObject(list);
            if (File.Exists(Directory.GetParent(path).Parent.FullName + "\\" + FileName))
                File.WriteAllText(Directory.GetParent(path).Parent.FullName + "\\" + FileName, json);
            else
            {
                FileStream stream = File.Create(Directory.GetParent(path).Parent.FullName + "\\" + FileName + ".json");
                stream.Close();
                File.WriteAllText(Directory.GetParent(path).Parent.FullName + "\\" + FileName, json);
            }

        }
    }
}
