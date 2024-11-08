using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetromino
{
    internal class Colors
    {
        public int[] colors;
        public Colors() {
            colors = getColors();
        }

       
        //Cada bloque en la grilla será representado en distintos colores
        private int[] getColors()
        {
            int grey = 0xf1e5e2; 
            int yellow = 0xf4f967;
            int green = 0x48e54f;
            int red = 0xeb421a;
            int blue = 0x3d54e0;
            int pink = 0xe03dcb;
            int lightBlue = 0x5ce0ec;
            int other = 0x052e16;

            int[] colors = new int[8];
            colors[0] = grey; // El primer color es el que uso para pintar la grilla vacia, por eso es importante el orden
            colors[1] = yellow;
            colors[2] = green;
            colors[3] = red;
            colors[4] = blue;
            colors[5] = pink;
            colors[6] = lightBlue;
            colors[7] = other;


            return colors;
        }
    }
}
