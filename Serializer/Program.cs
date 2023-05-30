using Newtonsoft.Json;

namespace Serializer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }
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
    }
}