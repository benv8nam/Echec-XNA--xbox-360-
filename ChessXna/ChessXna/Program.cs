using System;

namespace ChessXna
{
    static class Program
    {
        static void Main(string[] args)
        {
            using (ChessXna game = new ChessXna())
            {
                game.Run();
            }
        }
    }
}

