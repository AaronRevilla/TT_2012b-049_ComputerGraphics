using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerGraphics.Algoritmos.GraficarLineas
{
    class Naive
    {
        /*
        int naive(int* raster, int c, int r, int xi, int yi, int xf, int yf)
        {
            int len = 0;
            int longx = 0;
            int longy = 0;
            int aux_x = 0;
            int aux_y = 0;
            int i = 0;
            int y = 0;


            float x = 0;
            float b = 0;
            float y1 = 0;
            float m = 0;

            m = (((float)yf - (float)yi) / ((float)xf - (float)xi));

            longx = xf - xi;
            longy = yf - yi;

            /////////////////////////////////////////////////////////////obtener su magnitud
            if (longx < 0)
            {
                longx = longx * -1;
            }
            if (longy < 0)
            {
                longy = longy * -1;
            }
            ////////////////////////////////////////////////////////////////////////////////

            if (longx >= longy)
            {
                //////////////////////////////////////////en caso de que P2 sea mas chico que P1
                if (xf < xi)
                {
                    aux_x = xf;
                    aux_y = yf;
                    xf = xi;
                    yf = yi;
                    xi = aux_x;
                    yi = aux_y;
                    len = longx;
                    if (len < 0)
                    {
                        len = len * (-1);
                    }
                }
                ////////////////////////////////////////////////////////////////////////////////

                x = (float)xi;
                b = yi - (m * x);
                len = longx;

                for (i = 0; i < len; i++)
                {
                    y1 = (m * x) + b;
                    *(raster + ((rounding(y1)) * c) + ((int)x)) = 1;
                    x = x + 1.0;
                }
            }
            else////////////////////////////////////////////////////////swap the plane
            {
                //////////////////////////////////////////en caso de que P2 sea mas chico que P1
                if (yf < yi)
                {
                    aux_x = xf;
                    aux_y = yf;
                    xf = xi;
                    yf = yi;
                    xi = aux_x;
                    yi = aux_y;
                    len = longx;
                    if (len < 0)
                    {
                        len = len * (-1);
                    }
                }
                ////////////////////////////////////////////////////////////////////////////////

                m = (((float)xf - (float)xi) / ((float)yf - (float)yi));
                b = (float)((float)xi - (m * (float)yi));
                y = (float)yi;
                len = longy;

                /////////////////////////////////////////////////////////algoritmo naive swaping

                for (i = 0; i < len; i++)
                {
                    y1 = (m * y) + b;
                    *(raster + ((((int)y) * c) + (rounding(y1)))) = 1;
                    y = y + 1.0;
                }
            }

            return 0;
        }*/
    }
}
