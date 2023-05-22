using Newtonsoft.Json;
using prac17.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace prac17.ViewModel.Helpers
{
    internal class Deserialize
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

        public static List<Word> Word = Deserialize.deserialize<List<Word>>("words.json");
    }
}
