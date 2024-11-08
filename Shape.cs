using System;
using System.Runtime.InteropServices.ComTypes;
using Tao.Sdl;


namespace Tetromino
{

    struct ShapeRot
    {
        public int stateRotation;
        public (int, int)[][] rotations; // tuplas con posiciones de la pieza
        public int color;
        public int offsetX;
        public int offsetY;
    };

    internal class Shape
    {

        int tileSize;
        int[] colors;
        public ShapeRot current;
        public ShapeRot next;
        public Grid grid;
        public bool fullBoard = false;
        private Random rand;
        public int scoring = 0;

        public Shape()
        {
            tileSize = 30;
            colors = new Colors().colors;
            current = GetRandom();
            next = GetRandom();
            grid = new Grid();
        }

        public ShapeRot GetRandom()
        {
            rand = new Random();
            int randomShape = rand.Next(0, 7); // 5 + default, 6 figuras posibles

            switch (randomShape)
            {
                case 0:
                    return LShape();
                case 1:
                    return TShape();
                case 2:
                    return JShape();
                case 3:
                    return IShape();
                case 4:
                    return ZShape();
                case 5:
                    return SShape();
                default:
                    return OShape();
            }
        }

        public ShapeRot JShape()
        {
            // { 0, 1, 0},
            // { 0, 1, 0},
            // { 1, 1, 0}
            // Posiciones en la matrix - LShape
            (int posX, int posY)[] rot1 = { (1, 0), (1, 1), (1, 2), (0, 2) };
            (int posX, int posY)[] rot2 = { (0, 0), (0, 1), (1, 1), (2, 1) };
            (int posX, int posY)[] rot3 = { (0, 0), (1, 0), (0, 1), (0, 2) };
            (int posX, int posY)[] rot4 = { (0, 0), (1, 0), (2, 0), (2, 1) };

            // array de rotaciones
            (int posX, int posY)[][] rotations = { rot1, rot2, rot3, rot4 };

            return new ShapeRot
            {
                stateRotation = 0,
                rotations = rotations,
                color = 1,
                offsetX = 3,
                offsetY = 0
            };
        }

        public ShapeRot TShape()
        {
            (int posX, int posY)[] rot1 = { (1, 0), (0, 1), (1, 1), (2, 1) };
            (int posX, int posY)[] rot2 = { (0, 0), (0, 1), (0, 2), (1, 1) };
            (int posX, int posY)[] rot3 = { (0, 0), (1, 0), (2, 0), (1, 1) };
            (int posX, int posY)[] rot4 = { (2, 0), (2, 1), (2, 2), (1, 1) };

            (int posX, int posY)[][] rotations = { rot1, rot2, rot3, rot4 };
            return new ShapeRot
            {
                stateRotation = 0,
                rotations = rotations,
                color = 2,
                offsetX = 3,
                offsetY = 0
            };
        }

        public ShapeRot LShape()
        {
            (int posX, int posY)[] rot1 = { (0, 0), (0, 1), (1, 1), (2, 1) };
            (int posX, int posY)[] rot2 = { (1, 0), (1, 1), (0, 2), (1, 2) };
            (int posX, int posY)[] rot3 = { (0, 0), (1, 0), (2, 0), (2, 1) };
            (int posX, int posY)[] rot4 = { (0, 0), (1, 0), (0, 1), (0, 2) };

            (int posX, int posY)[][] rotations = { rot1, rot2, rot3, rot4 };

            return new ShapeRot
            {
                stateRotation = 0,
                rotations = rotations,
                color = 3,
                offsetX = 3,
                offsetY = 0
            };
        }

        public ShapeRot IShape()
        {
            (int posX, int posY)[] rot1 = { (0, 0), (1, 0), (2, 0), (3, 0) };
            (int posX, int posY)[] rot2 = { (0, 0), (0, 1), (0, 2), (0, 3) };
            (int posX, int posY)[] rot3= { (0, 0), (1, 0), (2, 0), (3, 0) };
            (int posX, int posY)[] rot4= { (0, 0), (0, 1), (0, 2), (0, 3) };

            (int posX, int posY)[][] rotations = { rot1, rot2, rot3, rot4 };

            return new ShapeRot
            {
                stateRotation = 0,
                rotations = rotations,
                color = 4,
                offsetX = 3,
                offsetY = 0
            };
        }

        public ShapeRot ZShape()
        {
            (int posX, int posY)[] rot1 = { (0, 0), (1, 0), (1, 1), (2, 1) };
            (int posX, int posY)[] rot2 = { (1, 0), (0, 1), (1, 1), (0, 2) };
            (int posX, int posY)[] rot3 = { (0, 0), (1, 0), (1, 1), (2, 1) };
            (int posX, int posY)[] rot4 = { (1, 0), (0, 1), (1, 1), (0, 2) };

            (int posX, int posY)[][] rotations = { rot1, rot2, rot3, rot4 };

            return new ShapeRot
            {
                stateRotation = 0,
                rotations = rotations,
                color = 5,
                offsetX = 3,
                offsetY = 0
            };
        }

        public ShapeRot SShape()
        {
            (int posX, int posY)[] rot1 = { (1, 0), (2, 0), (0, 1), (1, 1) };
            (int posX, int posY)[] rot2 = { (0, 0), (0, 1), (1, 1), (1, 2) };
            (int posX, int posY)[] rot3 = { (1, 0), (2, 0), (0, 1), (1, 1) };
            (int posX, int posY)[] rot4 = { (0, 0), (0, 1), (1, 1), (1, 2) };

            (int posX, int posY)[][] rotations = { rot1, rot2, rot3, rot4 };

            return new ShapeRot
            {
                stateRotation = 0,
                rotations = rotations,
                color = 6,
                offsetX = 3,
                offsetY = 0
            };
        }

        public ShapeRot OShape()
        {
            (int posX, int posY)[] rot1 = { (0, 0), (0, 1), (1, 0), (1, 1)};
            (int posX, int posY)[] rot2 = { (0, 0), (0, 1), (1, 0), (1, 1)};
            (int posX, int posY)[] rot3 = { (0, 0), (0, 1), (1, 0), (1, 1) };
            (int posX, int posY)[] rot4 = { (0, 0), (0, 1), (1, 0), (1, 1) };

            (int posX, int posY)[][] rotations = { rot1, rot2, rot3, rot4 };

            return new ShapeRot
            {
                stateRotation = 0,
                rotations = rotations,
                color = 7,
                offsetX = 3,
                offsetY = 0
            };
        }

        public void Draw(IntPtr img)
        {

            (int posX, int posY)[] positions = GetPositions();

            Sdl.SDL_Rect rect;
            for (int i = 0; i < positions.Length; i++)
            {
                rect = new Sdl.SDL_Rect
                {
                    x = (short)((positions[i].posX) * tileSize),
                    y = (short)((positions[i].posY) * tileSize),
                    w = (short)(tileSize - 1),
                    h = (short)(tileSize - 1)
                };
                int color = colors[current.color]; //toma el indice de colors
                Sdl.SDL_FillRect(img, ref rect, color);
            }
        }

        public void DrawNextShape(IntPtr img)
        {
                (int posX, int posY)[] positions = next.rotations[0];

                int initialPosX = 400;
                int initialPosY = 210;
                Sdl.SDL_Rect rect;
                for (int i = 0; i < positions.Length; i++)
                {
                    rect = new Sdl.SDL_Rect
                    {
                        x = (short)(initialPosX + (positions[i].posX * tileSize)),
                        y = (short)(initialPosY + (positions[i].posY * tileSize)),
                        w = (short)(tileSize - 1),
                        h = (short)(tileSize - 1)
                    };
                    int color = colors[next.color]; //toma el indice de colors
                    Sdl.SDL_FillRect(img, ref rect, color);
                }
        }


        public void Move(int x, int y)
        {
            current.offsetX += x;
            current.offsetY += y;
        }

        public void MoveDown(int row, int col)
        {
            Move(row, col);
            if (!ShapeInsideBoard() || CollisionWithShapes())
            {
                Move(0, -col);
                LockShape();
            }
        }

        public void MoveRight(int row, int col)
        {
            Move(row, col);
            if (!ShapeInsideBoard() || CollisionWithShapes()) {
                Move(-row, 0);
            }

        }

        public void MoveLeft(int row, int col)
        {
            Move(row, col);
            if (!ShapeInsideBoard() || CollisionWithShapes())
            {
                Move(-row, 0);
            }
        }

        // Colision con bordes tablero
        private bool ShapeInsideBoard()
        {
             (int posX, int posY)[] currentPositions = GetPositions();

            for (int i = 0; i < currentPositions.GetLength(0) ; i++)
            {
                if (!grid.RangeIsInside(currentPositions[i].posX, currentPositions[i].posY))
                {
                    return false;
                }
            }

            return true;
        }

        private (int, int)[] GetPositions()
        {
            (int posX, int posY)[] positions = current.rotations[current.stateRotation];
            (int posX, int posY)[] newPositions = new (int, int)[positions.GetLength(0)];
            for (int i = 0; i < positions.GetLength(0); i++)
            {
                newPositions[i].posX = positions[i].posX + current.offsetX;
                newPositions[i].posY = positions[i].posY + current.offsetY;
            }

            return newPositions;
        }

   
        public void Rotate()
        {
            (int posX, int posY)[] positions = current.rotations[current.stateRotation];
            current.stateRotation++;

            // si llega a la ultima posicion, vuelve a posicion 0
            if (current.stateRotation >= positions.GetLength(0))
            {
                current.stateRotation = 0;
            }

            // si colisiona deshace la rotacion
            if (!ShapeInsideBoard() || CollisionWithShapes()) {
                current.stateRotation--;
                if (current.stateRotation < 0)
                {
                    current.stateRotation = positions.GetLength(0) - 1;
                }
            }
          
        }

        private bool insideLateralBoard(int posX, int posY)
        {
            return (posX > -1 && posX < 10) && (posY < 20 && posY > -1);
        }

        private bool CollisionWithShapes() {
            (int posX, int posY)[] positions = GetPositions();

            for (int i = 0; i < positions.GetLength(0); i++)
            {
                if (!grid.CellIsEmpty(positions[i].posX, positions[i].posY))
                {
                    return true;
                }
            }
            return false;
        }
        private void LockShape()
        {
            (int posX, int posY)[] positions = GetPositions();

            for (int i = 0; i < positions.GetLength(0); i++)
            {
                grid.LockTile(positions[i].posX, positions[i].posY, current.color);
            }

            RegenerateShapes();
            int rowsDeleted = grid.DeleteCompletedRows();
            if (CollisionWithShapes()) 
            {
                fullBoard = true;
            }

            scoring += 2 + (rowsDeleted*10); // 2 puntos por colocar pieza + 10 por linea eliminada
        }

        private void RegenerateShapes()
        {
            current = next;
            next = GetRandom();
        }
    }
}
