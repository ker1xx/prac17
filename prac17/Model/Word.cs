using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prac17.Model
{
    internal class Word
    {
        public Word(string ThisWord, int AmountOfAttempsRemain, bool? WonOrLost)
        {
            this.ThisWord = ThisWord;
            this.AmountOfAttempsRemain = AmountOfAttempsRemain;
            this.WonOrLost = WonOrLost;
        }
        public string ThisWord;
        public int AmountOfAttempsRemain;
        public bool? WonOrLost;
    }
}
