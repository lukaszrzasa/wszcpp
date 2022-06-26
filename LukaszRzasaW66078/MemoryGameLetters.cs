using System;

namespace LukaszRzasaW66078
{
    public class MemoryGameLetters : MemoryGame
    {

        static new char[] charMap = { 'A', 'B', 'C', 'D', 'E',
            'F', 'G', 'H' };

        public MemoryGameLetters() : base(charMap)
        {

        }
    }
}
