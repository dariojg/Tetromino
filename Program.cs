using System;
using Tao.Sdl;

namespace Tetromino
{

    class Program
    {
        static Font font;
  

        static Grid grid;
        static Shape shape;
        static ShapeRot currentShape;
        static ShapeRot nextShape;

        static int windowWidht = 640;
        static int windowHeight = 600;

        static int intervalTicker = 20000;
        static int currentTime = 0;
        static int lastTime = 0;

        static bool running = true;
        static bool menu = true;
        static int scoring = 0;
        static void Main(string[] args)
        {
            Engine.Initialize(windowWidht, windowHeight);

            font = Engine.LoadFont("assets/font.ttf", 30);

            shape = new Shape();
            grid = shape.grid;

            while (true)
            {
                currentTime += Sdl.SDL_GetTicks();
                CheckInputs();
                Update();
                Render();
                Sdl.SDL_Delay(20);  
            }
        }

        static void CheckInputs()
        {
            if (Engine.KeyPress(Engine.KEY_UP) && running)
            {
                shape.Rotate();
            }

            if (Engine.KeyPress(Engine.KEY_DOWN) && running)
            {
                shape.MoveDown(0,1);
            }

            if (Engine.KeyPress(Engine.KEY_LEFT) && running)
            {
                shape.MoveLeft(-1, 0);
            }

            if (Engine.KeyPress(Engine.KEY_RIGHT) && running)
            {
                shape.MoveRight(1, 0);
            }

            if (Engine.KeyPress(Engine.KEY_ESP) && !running)
            {
                grid.CleanBoard();
                running = true;
                shape.fullBoard = false;
            }

            if (Engine.KeyPress(Engine.KEY_ESP) && menu)
            {
                grid.CleanBoard();
                running = true;
                menu = false;
                shape.fullBoard = false;
            }


            if (Engine.KeyPress(Engine.KEY_ESC) && running)
            {
                Environment.Exit(0);
                SdlMixer.Mix_CloseAudio();
            }

        }

        static void Update()
        {
            currentShape = shape.current; 
            if (currentTime >= intervalTicker)
            {
               // shape.MoveDown(0, 1);
                currentTime = 0;
            }

            if (shape.fullBoard) {
                running = false;
            }

            scoring = shape.scoring;
        }

        static void Render()
        {
            Engine.Clear();

            if (menu)
            {
                Engine.DrawText("Tetromino", 200, 100, 123, 255, 255, font);
                Engine.DrawText("Press space to start", 120, 450, 255, 255, 255, font);
                Engine.Show();
                return;
            }

            if (!running && !menu)
            {
                Engine.DrawText("Game Over", windowWidht / 2 - 100, 100, 123, 255, 255, font);
                Engine.DrawText("Press space to restart", 120, 450, 255, 255, 255, font);
                Engine.Show();
                return;
            }

            Engine.DrawText("Score:", windowWidht - 300, 10, 200, 200, 200, font);
            Engine.DrawText(scoring.ToString(), windowWidht - 70, 10, 200, 200, 200, font);


            shape.grid.Draw(Engine.screen);
            shape.Draw(Engine.screen);

           // shape.grid.DrawBoardNextShape(Engine.screen);
            shape.DrawNextShape(Engine.screen);

            Engine.Show();
        }

  
    }
}