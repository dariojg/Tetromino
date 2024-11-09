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
        static Tetromino tetromino;
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
        static bool pause = false;

        static void Main(string[] args)
        {
            Engine.Initialize(windowWidht, windowHeight);

            font = Engine.LoadFont("assets/font.ttf", 30);
            
            tetromino = new Tetromino();
            grid = tetromino.grid;

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
            if (Engine.KeyPress(Engine.KEY_UP) && running && !keyPressed && !pause)
            {
                tetromino.Rotate();
                keyPressed = true;
            }

            if (Engine.KeyPress(Engine.KEY_DOWN) && running && !keyPressed && !pause)
            {
                tetromino.MoveDown(0,1);
                keyPressed = true;
            }

            if (Engine.KeyPress(Engine.KEY_LEFT) && running && !keyPressed && !pause)
            {
                tetromino.MoveLeft(-1, 0);
                keyPressed = true;
            }

            if (Engine.KeyPress(Engine.KEY_RIGHT) && running && !keyPressed && !pause)
            {
                tetromino.MoveRight(1, 0);
                keyPressed = true;
            }

            if (Engine.KeyPress(Engine.KEY_ESP) && menu)
            {
                RestartGame();
            }

            if (Engine.KeyPress(Engine.KEY_P) && !keyPressed && !menu)
            {
                pause = !pause;
                keyPressed = true;
            }


            if (Engine.KeyPress(Engine.KEY_ESP) && !menu && !running)  // Reinciar despues de game over
            {
                RestartGame();
            }

            if (Engine.KeyPress(Engine.KEY_ESC) && running)
            {
                Environment.Exit(0);
            }

        }
        static void Update()
        {
            UpdateTime();
            currentShape = tetromino.current;


            if (gameTime.acumulatedTime > gameTime.speed && running && !pause)
            {
                tetromino.MoveDown(0, 1);
                gameTime.acumulatedTime = 0;
           
            }

            if (gameTime.acumulatedTimeToRelease > releaseKey)
            {
                keyPressed = false;
                gameTime.acumulatedTimeToRelease = 0;
            }

            if (tetromino.fullBoard) {
                running = false;
            }

            if(scoring > 500 && gameTime.speed > 0.2f)  
            {
                gameTime.speed = 0.2f; // aumenta dificultad, la pieza cae mas rápido
            }

            sound.CheckIfPlayFinishIt();

            scoring = tetromino.scoring;
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
            tetromino.DrawNextShape(Engine.screen);

            // Tablero y Piezas
            tetromino.grid.Draw(Engine.screen);
            tetromino.Draw(Engine.screen);

            // Juego en pausa 
            if (pause)
            {
                Engine.DrawText("Pause", windowWidht / 2 - 100, 200, 180, 180, 180, font);
            }

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

        static void RestartGame()
        {
            grid.CleanBoard();
            running = true;
            menu = !menu;
            tetromino.fullBoard = false;
            tetromino.scoring = 0;
        }
  
    }
}