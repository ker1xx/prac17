using prac17.Model;
using System.Collections.Generic;

namespace prac17.ViewModel.Helpers
{
    internal class Deserialize
    {
        public static List<Word> Word = Serializer.Serializer.deserialize<List<Word>>("words.json");
    }
}
