
using Raylib_cs;
using System.Net;
using System.Net.Sockets;
using System.Text;
using pong;

class MultiGame : Game
{
    private string lastMsg;
    private Socket listener;
    private Socket? handler;

    public MultiGame() : base()
    {
        this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        this.lastMsg = "";
    }

    public void host(int port)
    {
        try
        {
            this.listener.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), port));
            this.listener.Listen(1);
            this.listener.Blocking = false;
            while (this.handler == null)
            {
                if (listener.Poll(1000, SelectMode.SelectRead))
                {
                    this.handler = listener.Accept();
                    this.handler.Blocking = false;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error!!");
            Console.WriteLine(e.Message);
        }
    }

    string? ReceiveData()
    {
        if (this.handler != null && this.handler.Poll(1000, SelectMode.SelectRead))
        {
            if (this.handler.Available > 0)
            {
                byte[] buffer = new byte[1024];
                int bytesReceived = this.handler.Receive(buffer);
                return Encoding.ASCII.GetString(buffer, 0, bytesReceived);
            }
        }
        return null;
    }

    protected override void MoveOpponent()
    {
        string? move = ReceiveData();
        Console.WriteLine(move);
        if (move == "i")
        {
            opponent.Y += 8f;
        }
        else if (move == "o")
        {
            opponent.Y -= 8f;
        }

        if (opponent.Y < 0)
            opponent.Y = 0;
        else if (opponent.Y + paddleHeight > Raylib.GetScreenHeight())
            opponent.Y = Raylib.GetScreenHeight() - paddleHeight;
    }
}

