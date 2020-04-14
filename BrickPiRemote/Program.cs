using System;

namespace BrickPiRemote
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new BrickPiRemoteGame())
                game.Run();
        }
    }
}
