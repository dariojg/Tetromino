using System;
using Tao.Sdl;


namespace Tetromino
{

    class Program
    {
        
      
        static Font font;
        const int windowWidht = 640;
        const int windowHeight = 600;
        
        static Grid grid;
        static Shape shape;
        static ShapeRot currentShape;
        
        static Sound sound = new Sound();
         struct GameTime
        {
            public DateTime startTime;
            public float deltaTime;
            public float lastTime;
            public float acumulatedTime;
            public float speed;
            public float acumulatedTimeToRelease;

        }
        static GameTime gameTime = new GameTime { };

        // Variables de estado
        static bool running = true;
        static bool menu = true;
        static int scoring = 0;
        static bool keyPressed = false;
        const float releaseKey = 0.1f;

        static void Main(string[] args)
        {
            Engine.Initialize(windowWidht, windowHeight);

            font = Engine.LoadFont("assets/font.ttf", 30);
            
            shape = new Shape();
            grid = shape.grid;

            gameTime.startTime = DateTime.Now;
            gameTime.speed = 0.7f;  // Velocidad de caída de la pieza


            while (true)
            {
                CheckInputs();
                Update();
                Render();
                Sdl.SDL_Delay(20);  
            }
        }

        static void CheckInputs()
        {
            if (Engine.KeyPress(Engine.KEY_UP) && running && !keyPressed)
            {
                shape.Rotate();
                keyPressed = true;
            }

            if (Engine.KeyPress(Engine.KEY_DOWN) && running && !keyPressed)
            {
                shape.MoveDown(0,1);
                keyPressed = true;
            }

            if (Engine.KeyPress(Engine.KEY_LEFT) && running && !keyPressed)
            {
                shape.MoveLeft(-1, 0);
                keyPressed = true;
            }

            if (Engine.KeyPress(Engine.KEY_RIGHT) && running && !keyPressed)
            {
                shape.MoveRight(1, 0);
                keyPressed = true;
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
                sound.StopBackgroundMusic();
                sound.StartBackgroundMusic();
            }

            if (Engine.KeyPress(Engine.KEY_ESC) && running)
            {
                Environment.Exit(0);
            }

        }
        static void Update()
        {
            UpdateTime();
            currentShape = shape.current;


            if (gameTime.acumulatedTime > gameTime.speed && running)
            {
                shape.MoveDown(0, 1);
                gameTime.acumulatedTime = 0;
           
            }

            if (gameTime.acumulatedTimeToRelease > releaseKey)
            {
                keyPressed = false;
                gameTime.acumulatedTimeToRelease = 0;
            }

            if (shape.fullBoard) {
                running = false;
            }

            if(scoring > 500 && gameTime.speed > 0.2f)  
            {
                gameTime.speed = 0.2f; // aumenta dificultad, la pieza cae mas rápido
            }

            if (!running)
            {
                sound.StopBackgroundMusic();
            }

            scoring = shape.scoring;
        }

        static void Render()
        {
            Engine.Clear();


            // Pantalla Menu
            if (menu)
            {
                Engine.DrawText("Tetromino", 200, 100, 123, 255, 255, font);
                Engine.DrawText("Press space to start", 120, 450, 255, 255, 255, font);
                Engine.Show();
                return;
            }


            // Pantalla Juego terminado
            if (!running && !menu)
            {
                Engine.DrawText("Game Over", windowWidht / 2 - 100, 100, 123, 255, 255, font);
                Engine.DrawText("Press space to restart", 120, 450, 255, 255, 255, font);
                Engine.Show();
                return;
            }


            // Puntos y próxima pieza
            Engine.DrawText("Score:", windowWidht - 300, 10, 200, 200, 200, font);
            Engine.DrawText(scoring.ToString(), windowWidht - 70, 10, 200, 200, 200, font);

            Engine.DrawText("Next:", windowWidht - 300, 150, 200, 200, 200, font);
            shape.DrawNextShape(Engine.screen);


            // Tablero y Piezas
            shape.grid.Draw(Engine.screen);
            shape.Draw(Engine.screen);
            
            Engine.Show();
        }


        static void UpdateTime()
        {
            float currenTime = (float)(DateTime.Now - gameTime.startTime).TotalSeconds;
            gameTime.deltaTime = currenTime - gameTime.lastTime;
            gameTime.lastTime = currenTime;
            gameTime.acumulatedTime += gameTime.deltaTime;
            gameTime.acumulatedTimeToRelease += gameTime.deltaTime;
        }

  
    }
}