using System;
using Tao.Sdl;

namespace Tetromino
{
    internal class Board
    {


        {
            matrix = new int[10, 20];
            matrixNextShape = new int[4, 4];
            colors = new Colors().colors;
        }

        // Solo para debug
        public void PrintGrid() {
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    Console.Write(matrix[j, i] + " ");
                }
                Console.WriteLine();
            }
        }

        public void Draw(IntPtr img)
        {
            Sdl.SDL_Rect rect;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    int cellValue = matrix[i, j];
                    rect = new Sdl.SDL_Rect {
                        x = (short)(i * tileSize),
                        y = (short)(j * tileSize),
                        w = (short)(tileSize - 1), // le resto un pixel para efecto separacion entre tiles
                        h = (short)(tileSize - 1)
                    };

                    Sdl.SDL_FillRect(img, ref rect, colors[cellValue]); //colors cellValue toma el indice de colors al principio siempre es 0
                }
            }
        }


        // fondo de la pieza siguiente
        public void DrawBoardNextShape(IntPtr img)
        {
            int initPosX = 370;
            int initPosY = 200;
            Sdl.SDL_Rect rect;
            for (int i = 0; i < matrixNextShape.GetLength(0); i++)
            {
                for (int j = 0; j < matrixNextShape.GetLength(1); j++)
                {
                    int cellValue = matrixNextShape[i, j];
                    rect = new Sdl.SDL_Rect
                    {
                        x = (short)(initPosX+i * tileSize),
                        y = (short)(initPosY+j * tileSize),
                        w = (short)(tileSize), 
                        h = (short)(tileSize)
                    };

                    Sdl.SDL_FillRect(img, ref rect, colors[0]); //colors cellValue toma el indice de colors al principio siempre es 0
                }
            }
        }


        /*
         * Collisiones
         */
        public void LockTile(int posX, int posY, int color)
        {
            matrix[posX, posY] = color; 
        }

        public bool RangeIsInside(int posX, int posY)
        {
            return posX >= 0 && posX < matrix.GetLength(0) && posY >= 0 && posY < matrix.GetLength(1);
        }

        public bool CellIsEmpty(int posX, int posY)
        {
            return matrix[posX, posY] == 0; // si la celda es igual a 0 tiene color gris, es decir está libre
        }


        /*
         * Validaciones de fila completa
         */
        public int DeleteCompletedRows()
        {
            int countRowsToDelete = 0;
            int lenCol = matrix.GetLength(1) - 1;

            for (int lastRow = lenCol; lastRow >= 0; lastRow--)  // Conviene que empezar desde abajo hacia arriba
            {
                if (CheckCompletedRow(lastRow))
                {
                    countRowsToDelete++;
                    DeleteRow(lastRow);
                }
                else if (countRowsToDelete > 0)
                {
                    UpdateRows(lastRow, countRowsToDelete);
                }
            }

            return countRowsToDelete;
        }

        public bool CheckCompletedRow(int row)
        {
            // recorro las columnas para chequear solo una fila
            for (int col = 0; col < matrix.GetLength(0); col++)
            { 
                {
                    if (matrix[col, row] == 0) // si es 0 entonces hay espacios vacios, la fila no está completa 
                    {
                        return false;
                    }
                }

            }

            return true;
        }

        public void UpdateRows(int row, int countRows)
        {
            for (int col = 0; col < matrix.GetLength(0); col++)
            {
                matrix[col, row+countRows] = matrix[col, row];
                matrix[col, row] = 0;
            }
        }

        public void DeleteRow(int row)
        {
            for (int col = 0; col < matrix.GetLength(0); col++)
            {
                matrix[col, row] = 0; // se elimina toda la fila
            }
        }


        /*
         * Reset tablero
         */
        public void CleanBoard()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = 0;
                }
            }
        }
    
    } 
}
