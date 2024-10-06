
using Raylib_cs;
using pong;
using System.Numerics;

class MultiGame : Game
{

    private static readonly float speed = 4f;
    private static readonly int ballHeight = 15;
    private static readonly int paddleHeight = 100;


    private Rectangle player;
    private Rectangle opponent;
    private Rectangle ball;
    private Vector2 velocity;
    private int playerScore;
    private int opponentScore;

    private int middle;

    public MultiGame() : base()
    {
        //socket stuff
    }

    private void MoveOpponent()
    {

    }



}
