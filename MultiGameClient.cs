using System.Net;
using System.Net.Sockets;
using System.Text;
using pong;

class MultiGameClient : Game
{
    private Socket listener;
    private Socket? handler;

    public MultiGameClient() : base()
    {
        this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    public void join(int port)
    {
        try
        {
            this.handler = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        
            this.handler.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), port));

            this.handler.Blocking = false;

            Console.WriteLine("Connected to server!");

    }
    catch (Exception e)
    {
        Console.WriteLine("Error connecting to the server!");
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
        var pos = this.ReceiveData();
        if(pos.Length == 0)
        {
          return; 
        }
        var x = pos[0].Split(":");
        if(x[0] != "b")
        {
          return;
        }
        var split = x[1].Split(".");

        this.ball.X = float.Parse(split[0]);
        this.ball.Y = float.Parse(split[1]);
        this.velocity.X = float.Parse(split[2]);
        this.velocity.Y = float.Parse(split[3]);
    }


    protected override void MoveOpponent()
    {
        var pos = this.ReceiveData();
        if(pos.Length == 0)
        {
          return; 
        }
        var x = pos[0].Split(":");
        Console.WriteLine(x);
        if(x[0] != "p")
        {
          return;
        }
        var split = x[1];
        Console.WriteLine(split);
        this.opponent.Y = float.Parse(split);
    }

    protected override void MovePlayer()
    {
        base.MovePlayer();
        var pos = $"o:{this.player.Y};";
        this.WriteData(pos);
    }

}
