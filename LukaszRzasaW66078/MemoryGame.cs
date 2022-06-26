using System;
using System.Linq;
namespace LukaszRzasaW66078
{
    public class MemoryGame
    {
        protected const int size = 4;
        protected int[,] selected = new int[2, 2] {{-1, -1},{-1, -1} };
        protected int move = 0;
        protected char[] letters = new char[size] { 'A', 'B', 'C', 'D' };

        protected char[] charMap = new char[16];

        protected Ceil[,] board = new Ceil[size, size];

        private bool isGameFinished = false;
        private bool isGameValid = false;


        public MemoryGame(char[] charMap)
        {
            if(charMap.Length<=0)
            {
                Console.Write(":(");
                return;
            }
            for(int i=0; i<8; i++)
            {
                this.charMap[i] = charMap[i];
            }
            this.isGameValid = true;
        }

        public void Start()
        {
            if(this.isGameValid)
            {
                this.CreateBoard();
                this.Draw(true);
                System.Threading.Thread.Sleep(2000);
                this.Loop();
            }
        }

        private void CreateBoard()
        {
            int[] order = { 0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7 };
            Random random = new Random();
            order = order.OrderBy(x => random.Next()).ToArray();
            //
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    this.board[row, col] = new Ceil(this.charMap[order[(row*4) + col]]);
                }
            }

        }

        private void Loop()
        {
            do
            {
                this.Draw(false);
            } while (!this.MakeMove());
            //
            this.Draw(false);
            this.CompareSelected();
            //
            if(!this.ChackGameIsFinished())
            {
                this.Loop();
            }
        }

        private bool MakeMove()
        {
            Console.Write("Podaj wspolzedne (np A1): ");
            string moveStr = Console.ReadLine();
            if(moveStr == "q")
            {
                this.isGameFinished = true;
                return false;
            }
            if (moveStr.Length >= 2 && this.letters.Contains(moveStr[0]) && moveStr[1]-'0' > 0 && moveStr[1] - '0' <= size)
            {
                int row = Array.IndexOf(this.letters, moveStr[0]);
                int col = moveStr[1] - '0' - 1;
                if (row>=0 && col>=0 && row<size && col<size)
                {
                    if(this.board[row, col].IsShown)
                    {
                        return false;
                    }
                    if(this.selected[0, 0] == -1)
                    {
                        this.selected[0, 0] = row;
                        this.selected[0, 1] = col;
                    }
                    else
                    {
                        this.selected[1, 0] = row;
                        this.selected[1, 1] = col;
                        return true;
                    }
                }
            }
            return false;
        }

        private void CompareSelected()
        {
            if(this.board[this.selected[0, 0], this.selected[0, 1]].Sign(true) == this.board[this.selected[1, 0], this.selected[1, 1]].Sign(true))
            {
                this.board[this.selected[0, 0], this.selected[0, 1]].IsShown = true;
                this.board[this.selected[1, 0], this.selected[1, 1]].IsShown = true;
            } else
            {
                System.Threading.Thread.Sleep(1200);
            }
            this.selected[0, 0] = -1;
            this.selected[0, 1] = -1;
            this.selected[1, 0] = -1;
            this.selected[1, 1] = -1;
        }

        private void Draw(bool forceShow = false)
        {
            Console.Clear();
            for (int row = -1; row < size; row++)
            {
                for (int col = -1; col < size; col++)
                {
                    if (row == -1)
                    {
                        if (col == -1) Console.Write("   ");
                        else
                        {
                            Console.Write(col + 1);
                        }
                    }
                    else if (col == -1)
                    {
                        Console.Write(this.GetRowPrefix(row));
                    }
                    else
                    {
                        this.DrawCeilSign(row, col, forceShow);
                    }
                    Console.Write(' ');
                }
                Console.WriteLine("");
            }
            Console.WriteLine("-----");
            Console.Write("Wybrana para: ");
            if(this.selected[0, 0] != -1)
            {
                Console.Write(this.letters[this.selected[0, 0]]);
                Console.Write(this.selected[0, 1]+1);
            } else
            {
                Console.Write("__");
            }
            Console.Write(", ");
            if (this.selected[1, 0] != -1)
            {
                Console.Write(this.letters[this.selected[1, 0]]);
                Console.Write(this.selected[1, 1]+1);
            }
            else
            {
                Console.Write("__");
            }
            Console.WriteLine("");
            Console.WriteLine("-----");
        }

        private void DrawCeilSign(int row, int col, bool forceShow)
        {
            bool forceShowSign = (row == selected[0, 0] && col == selected[0, 1])
                || (row == selected[1, 0] && col == selected[1, 1]);
            char sign = this.board[row, col].Sign(forceShowSign || forceShow);
            Console.Write(sign);
        }

        private string GetRowPrefix(int row)
        {
            if (row == -1) return "  ";
            return letters[row] + ": ";
        }

        private bool ChackGameIsFinished()
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if(!this.board[row, col].IsShown)
                    {
                        return false;
                    }
                }
            }
            Console.WriteLine("Game finished");
            this.isGameFinished = true;
            return true;
        }
    }
}
