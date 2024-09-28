using System.Numerics;
using Raylib_cs;

namespace pong;

internal class Program
{
    private static readonly float speed = 4f;
    private static readonly int ballHeight = 15;
    private static readonly int paddleHeight = 100;

    private static Rectangle player;
    private static Rectangle opponent;
    private static Rectangle ball;
    private static Vector2 velocity;
    private static int playerScore;
    private static int opponentScore;


    private static void Reset()
    {
        var rnd = new Random();
        ball.X = Raylib.GetScreenWidth() / 2;
        ball.Y = rnd.Next(480);

        if (rnd.Next(2) == 0)
        {
            velocity.Y = speed + 2f;
            velocity.X = speed + 2f;
        }
        else
        {
            velocity.Y = (speed + 2f) * -1;
            velocity.X = (speed + 2f) * -1;
        }
    }


    private static void Move()
    {
        ball.X += velocity.X;
        ball.Y += velocity.Y;

        if (ball.X <= 0)
        {
            opponentScore++;
            Reset();
        }

        if (ball.X + ball.Width >= Raylib.GetScreenWidth())
        {
            playerScore++;
            Reset();
        }

        if (ball.Y <= 0 || ball.Y >= Raylib.GetScreenHeight() - 15) velocity.Y *= -1;
        if (ball.X <= 0 || ball.X >= Raylib.GetScreenWidth() - 15) velocity.X *= -1;


        if (Raylib.IsKeyDown(KeyboardKey.Up)) player.Y -= speed;
        if (Raylib.IsKeyDown(KeyboardKey.Down)) player.Y += speed;
        if (player.Y + player.Height >= Raylib.GetScreenHeight())
            player.Y = Raylib.GetScreenHeight() - player.Height;
        else if (player.Y <= 0) player.Y = 0;

        if (Raylib.CheckCollisionRecs(ball, player))
        {
            var relativeIntersectY = ball.Y + ball.Height / 2 - (player.Y + player.Height / 2);
            var normalizedRelativeY = relativeIntersectY / (player.Height / 2);

            velocity.Y = normalizedRelativeY * speed + 2;
            velocity.X *= -1;
        }

        if (Raylib.CheckCollisionRecs(ball, opponent))
        {
            var relativeIntersectY = ball.Y + ball.Height / 2 - (player.Y + opponent.Height / 2);
            var normalizedRelativeY = relativeIntersectY / (opponent.Height / 2);

            velocity.Y = normalizedRelativeY * speed + 2;
            velocity.X *= -1;
        }
    }

    private static void MoveOpponent()
    {
        if (velocity.X > 0) // Ball is moving towards the opponent
        {
            var timeToReachPaddle = (opponent.X - (ball.X + ballHeight)) / velocity.X;

            var predictedY = ball.Y + velocity.Y * timeToReachPaddle;

            if (predictedY < opponent.Y)
                opponent.Y -= speed - 2f;
            else if (predictedY > opponent.Y + paddleHeight)
                opponent.Y += speed - 2f;
        }

        if (opponent.Y < 0)
            opponent.Y = 0;
        else if (opponent.Y + paddleHeight > Raylib.GetScreenHeight())
            opponent.Y = Raylib.GetScreenHeight() - paddleHeight;
    }

    public static void Main()
    {
        Raylib.InitWindow(800, 480, "Hello World");
        Raylib.SetTargetFPS(60);

        var rnd = new Random();

        velocity = new Vector2(speed + 2, speed + 2);
        float starting = Raylib.GetScreenHeight() / 2 - 75;
        var middle = Raylib.GetScreenWidth() / 2 - 10;
        player = new Rectangle(20, starting, 20, paddleHeight);
        opponent = new Rectangle(Raylib.GetScreenWidth() - 40, 0, 20, paddleHeight);
        ball = new Rectangle(middle, rnd.Next(480), ballHeight, ballHeight);


        while (!Raylib.WindowShouldClose())
        {
            Move();
            MoveOpponent();


            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            Raylib.DrawFPS(20, 20);

            Raylib.DrawText(playerScore.ToString(), middle - 75, 20, 50, Color.Gray);
            Raylib.DrawText(opponentScore.ToString(), middle + 60, 20, 50, Color.Gray);

            Raylib.DrawRectangleRec(player, Color.White);
            Raylib.DrawRectangleRec(opponent, Color.White);
            Raylib.DrawRectangleRec(ball, Color.Red);


            Raylib.DrawRectangle(middle, 0, 10, Raylib.GetScreenHeight(), Color.Gray);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}