using System;

namespace Moonshot
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new MoonshotGame())
                game.Run();
        }
    }
}
