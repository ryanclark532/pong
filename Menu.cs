
using pong;
using Raylib_cs;

public class Menu
{
   private Button single;
   private Button multi;
   private TextInput input;
   Screen currentScreen;

   private MultiGame game;

   public Menu()
   {
      this.game = new MultiGame();
      Action s = () => this.currentScreen = Screen.Game;
      Action m = () => this.currentScreen = Screen.Game;

      this.single = new Button(50f, 120f, "Single Player", s, false);
      this.multi = new Button(50f, 200f, "Multi Player", m, false);
      this.input = new TextInput(50f, 280f, m);
   }

   private void drawMenu()
   {
      Raylib.DrawText("Welcome to Pong!", 50, 80, 30, Color.White);

      if (input.getText().Length == 0)
      {
         this.multi.disabled = true;
      }
      else
      {
         this.multi.disabled = false;
      }

      this.single.draw();
      this.multi.draw();
      this.input.draw();
   }

   public void paintFrame()
   {
      switch (currentScreen)
      {
         case Screen.Menu:
            this.drawMenu();
            break;

         case Screen.Game:
            if (Raylib.IsKeyDown(KeyboardKey.Q))
            {
               this.currentScreen = Screen.Menu;
               this.game = new MultiGame();
            }
            game.paintFrame();
            break;
      }

   }

}
