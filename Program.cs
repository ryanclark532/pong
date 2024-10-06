using Raylib_cs;

namespace pong;

public enum Screen
{
    Menu,
    Game
}

internal class Program
{

    public static void Main()
    {
        Raylib.InitWindow(800, 480, "Pong!");
        Raylib.SetTargetFPS(60);

        var menu = new Menu();

        while (!Raylib.WindowShouldClose())
        {

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            Raylib.DrawFPS(20, 20);

            menu.paintFrame();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}
