using System;
namespace LukaszRzasaW66078
{
    public class MemoryGameSymbols : MemoryGame
    {
        static new char[] charMap = { '!', '@', '#', '$', '%',
            '^', '&', '*' };

        public MemoryGameSymbols() : base(charMap)
        {
        }
    }
}
