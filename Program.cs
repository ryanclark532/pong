using Raylib_cs;
namespace pong;

public enum Screen
{
    Menu,
    Game
}

internal class Program
{

    private static Game game;

    public static void Main(string[] args)
    {
        Raylib.InitWindow(900, 480, "Pong!");
        Raylib.SetTargetFPS(60);

	    var length = args.Length;
        if(length == 0)
        {
            Console.WriteLine("Please provide a game mode (single or multiplayer)");
            return;
        }

        var type = args[0];
        var host = args[1];

        switch (type){
            case "single":
                game = new Game();
                break;
            case "multiplayer":
                var target = DateTime.Now;
                target.AddSeconds(5);
                var port = args[2];
                var hosting = host == "host";
                if(hosting)
                {
                    Console.WriteLine("Waiting for a connection");
                    var multiGame = new MultiGameHost();
                    multiGame.host(int.Parse(port));
                    game = multiGame;

                } else if(!hosting)
                {
                    Console.WriteLine("Joining Game");
                    var multiGame = new MultiGameClient();
                    multiGame.join(int.Parse(port));
                    game = multiGame; }
                while (DateTime.Now < target)
                {
                    Thread.Sleep(100); 
                }

                break;
        }
	

        while (!Raylib.WindowShouldClose())
        {

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            Raylib.DrawFPS(20, 20);
            Raylib.DrawText(host, Raylib.GetScreenWidth() - 200, 20, 20, Color.Gray);

            game.paintFrame();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}
