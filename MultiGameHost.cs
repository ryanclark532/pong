using Raylib_cs;
using System.Net;
using System.Net.Sockets;
using System.Text;
using pong;

class MultiGameHost: Game
{
    private Socket listener;
    private Socket? handler;


    public MultiGameHost() : base()
    {
        this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
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

    string[] ReceiveData()
    {
        if (this.handler != null && this.handler.Poll(10, SelectMode.SelectRead))
        {
            if (this.handler.Available > 0)
            {
                byte[] buffer = new byte[128];
                int bytesReceived = this.handler.Receive(buffer);
                var data = Encoding.UTF8.GetString(buffer, 0, bytesReceived);

                return data.Split(";");   
            }
        }
        return [];
    }

void WriteData(string data)
{
    try
    {
        if (this.handler != null && this.handler.Connected)
        {
            this.handler.Send(Encoding.UTF8.GetBytes(data));
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }

}

    protected override void MoveBall()
    {
        base.MoveBall();
        var pos = $"b:{this.ball.X}.{this.ball.Y}.{this.velocity.X}.{this.velocity.Y};";
        this.WriteData(pos);
    }


    protected override void MovePlayer()
    {
        base.MovePlayer();
        var pos = $"p:{this.player.Y};";
        this.WriteData(pos);
    }


    protected override void MoveOpponent()
    {
        var pos = this.ReceiveData();
        if(pos.Length == 0)
        {
          return; 
        }

        var x = pos[0].Split(":");
        if(x[0] != "o")
        {
          return;
        }
        var split = x[1];
        Console.WriteLine(split);
        this.opponent.Y = float.Parse(split);
    }

}

