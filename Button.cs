using System.Numerics;
using Raylib_cs;

public class Button
{
    private Rectangle rectangle;
    private string text;
    private Vector2 pos;
    private Action onClick;
    public bool disabled;

    public Button(float x, float y, string text, Action onClick, bool disabled)
    {
        this.pos.X = x;
        this.pos.Y = y;
        this.text = text;
        this.rectangle = new Rectangle(x, y, 250, 70);
        this.onClick = onClick;
        this.disabled = disabled;
    }

    public void draw()
    {
        if (this.disabled)
        {
            Raylib.DrawRectangleRec(rectangle, Color.Gray);
            Raylib.DrawText(this.text, (int)this.pos.X + 15, (int)this.pos.Y + 20, 30, Color.Black);
        }
        else
        {
            Raylib.DrawRectangleRec(rectangle, Color.White);
            Raylib.DrawText(this.text, (int)this.pos.X + 15, (int)this.pos.Y + 20, 30, Color.Black);
            if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), this.rectangle) && Raylib.IsMouseButtonPressed(MouseButton.Left))
            {
                this.onClick();
            }
        }
    }
}
