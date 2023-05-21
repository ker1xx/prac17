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
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string json = "";
            if (File.Exists(desktop + "\\" + FileName))
                json = File.ReadAllText(desktop + "\\" + FileName);
            else
            {
                FileStream stream = File.Create(desktop + "\\" + FileName + ".json");
                stream.Close();
                json = File.ReadAllText(desktop + "\\" + FileName);
            }
            T workers = JsonConvert.DeserializeObject<T>(json);
            return workers;
        }		

        public static List<Word> Word = Deserialize.deserialize<List<Word>>("words.json");
    }
}
