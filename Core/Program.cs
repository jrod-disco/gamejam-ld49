using System;

namespace gamejam_ld49
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new GameLD49())
                game.Run();
        }
    }
}
