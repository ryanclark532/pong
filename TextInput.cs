using System.Numerics;
using System.Text;
using Raylib_cs;

public class TextInput
{
    private Rectangle rectangle;
    private StringBuilder text;
    private Vector2 pos;
    private Action onEnter;
    private bool active;

    public TextInput(float x, float y, Action onEnter)
    {
        this.pos.X = x;
        this.pos.Y = y;
        this.rectangle = new Rectangle(x, y, 450, 70);
        this.onEnter = onEnter;
        this.active = false;
        this.text = new StringBuilder();
    }

    public string getText()
    {
        return this.text.ToString();
    }

    public void draw()
    {
        Raylib.DrawRectangleRec(rectangle, Color.Black);
        if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), this.rectangle) && Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            this.active = true;
        }
        else if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            this.active = false;
        }

        if (this.active)
        {
            Raylib.DrawRectangleLines((int)this.pos.X, (int)this.pos.Y, 450, 70, Color.White);
            if (Raylib.IsKeyPressed(KeyboardKey.Backspace))
            {
                this.text.Remove(this.text.Length - 1, 1);
            }
            else
            {

                int key = Raylib.GetCharPressed();

                while (key > 0)
                {
                    if ((key >= 48 && key <= 57) || key == (int)':' || key == (int)'.')
                    {
                        this.text.Append((char)key);
                    }
                    key = Raylib.GetCharPressed();
                }
            }

        }
        else
        {
            Raylib.DrawRectangleLines((int)this.pos.X, (int)this.pos.Y, 450, 70, Color.Gray);
        }
        Raylib.DrawText(this.text.ToString(), (int)this.pos.X + 15, (int)this.pos.Y + 20, 30, Color.White);
    }
}

