using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace ComputerGraphics.Algoritmos
{
    class DibujarLinea
    {
        public static LinkedList<Point3D> bresenham(Point3D p0, Point3D p1)
        {
            int x, y, dx, dy, p, incE, incNE, x0, y0, x1, y1, stepx, stepy;
            LinkedList<Point3D> puntos = new LinkedList<Point3D>();
            x0 = (int)(p0.X);
            x1 = (int)(p1.X);
            y0 = (int)(p0.Y);
            y1 = (int)(p1.Y);

            dy = (y1 - y0);
            dx = (x1 - x0);

            if (dy < 0)
            {
                dy = -dy;
                stepy = -1;
            }
            else
            {
                stepy = 1;
            }
            if (dx < 0)
            {
                dx = -dx;
                stepx = -1;
            }
            else
            {
                stepx = 1;
            }
            x = x0;
            y = y0;
            puntos.AddLast(new Point3D(x0, y0, 0));
            if (dx > dy)
            {
                p = 2 * dy - dx;
                incE = 2 * dy;
                incNE = 2 * (dy - dx);
                while (x != x1)
                {
                    x = x + stepx;
                    if (p < 0)
                    {
                        p = p + incE;
                    }
                    else
                    {
                        y = y + stepy;
                        p = p + incNE;
                    }
                    puntos.AddLast(new Point3D(x, y, 0));
                }
            }
            else
            {
                p = 2 * dx - dy;
                incE = 2 * dx;
                incNE = 2 * (dx - dy);
                while (y != y1)
                {
                    y = y + stepy;
                    if (p < 0)
                    {
                        p = p + incE;
                    }
                    else
                    {
                        x = x + stepx;
                        p = p + incNE;
                    }
                    puntos.AddLast(new Point3D(x, y, 0));
                }
            }
            return puntos;
        }

        public static LinkedList<Point3D> naiveLine(Point3D p0, Point3D p1)
        {
            LinkedList<Point3D> points = new LinkedList<Point3D>();
            int x0 = (int)p0.X;
            int y0 = (int)p0.Y;
            int x1 = (int)p1.X;
            int y1 = (int)p1.Y;

            int dx = x1 - x0;
            int dy = y1 - y0;
            points.AddLast(p0);
            if (Math.Abs(dx) > Math.Abs(dy))
            {
                float m = (float)(dy) / (float)(dx);
                float b = y0 - m * x0;
                if (dx < 0)
                    dx = -1;
                else
                    dx = 1;
                while (x0 != x1)
                {
                    x0 += dx;
                    y0 = round(m * x0 + b);
                    points.AddLast(new Point3D(x0, y0, 0));
                }
            }
            else
            {
                if (dy != 0)
                {
                    float m = (float)(dx) / (float)(dy);
                    float b = x0 - m * y0;
                    if (dy < 0)
                        dy = -1;
                    else
                        dy = 1;
                    while (y0 != y1)
                    {
                        y0 += dy;
                        x0 = round(m * y0 + b);
                        points.AddLast(new Point3D(x0, y0, 0));
                    }
                }
            }

            return points;
        }

        public static LinkedList<Point3D> dda(Point3D p0, Point3D p1)
        {
            LinkedList<Point3D> points = new LinkedList<Point3D>();
            points.AddLast(p0);

            int xi = (int)p0.X;
            int yi = (int)p0.Y;
            int xf = (int)p1.X;
            int yf = (int)p1.Y;

            int indicePunto = 0;

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
            float aux_y1 = 0;
            float m = 0;

            longx = xf - xi;
            longy = yf - yi;

            m = (((float)yf - (float)yi) / ((float)xf - (float)xi));

            /////////////////////////////////////////////////////////////////////////////
            //////CASO BASE LINEA HORIZONTAL/////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////
            if (yi == yf)
            {
                if (xi > xf)
                {
                    aux_x = xi;
                    xi = xf;
                    xf = aux_x;
                }
                for (indicePunto = xi; indicePunto < xf; indicePunto++)
                {
                    points.AddLast(new Point3D(indicePunto, yi, 0));
                }
                points.AddLast(p1);
                return points;
            }
            /////////////////////////////////////////////////////////////////////////////
            //////CASO BASE LINEA VERTIAL////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////
            else if (xi == xf)
            {
                if (yi > yf)
                {
                    aux_y = yi;
                    yi = yf;
                    yf = aux_y;
                }
                for (indicePunto = yi; indicePunto < yf; indicePunto++)
                {
                    points.AddLast(new Point3D(xi, indicePunto, 0));
                }
                points.AddLast(p1);
                return points;
            }
            /////////////////////////////////////////////////////////////////////////////
            //////CASO BASE LINEA DIAGONAL///////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////
            else if ((m == 1) || (m == -1))
            {
                if (xi > xf)
                {
                    aux_x = xi;
                    xi = xf;
                    xf = aux_x;
                    aux_y = yi;
                    yi = yf;
                    yf = aux_y;
                }
                if (m == 1)
                {

                    for (indicePunto = xi; indicePunto < xf; indicePunto++)
                    {
                        points.AddLast(new Point3D(xi, yi, 0));
                        xi++;
                        yi++;
                    }
                }
                else
                {
                    for (indicePunto = xi; indicePunto < xf; indicePunto++)
                    {
                        points.AddLast(new Point3D(xi, yi, 0));
                        xi++;
                        yi--;
                    }
                }
                points.AddLast(p1);
                return points;
            }
            ////////////////////////////////////////////////////////
            //////////////////obtener magnitud//////////////////////
            ////////////////////////////////////////////////////////
            if (longx < 0)
            {
                longx = longx * -1;
            }
            if (longy < 0)
            {
                longy = longy * -1;
            }
            ////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////
            if (longx >= longy)
            {
                ///////////////////////////////////////////////
                /////en caso de que P1 sea mas chico que P0////
                ///////////////////////////////////////////////
                if (xf < xi)
                {
                    //Se invierten los puntos//
                    aux_x = xf;
                    aux_y = yf;
                    xf = xi;
                    yf = yi;
                    xi = aux_x;
                    yi = aux_y;
                    len = xf - xi;
                    if (len < 0)
                    {
                        len = len * (-1);
                    }
                }
                ////////////////////////////////////////////////////////////////////////////////
                x = (float)xi;
                b = ((float)yi) - (m * x);
                len = longx;
                ///////////////////////////////////////////////////////////////////algoritmo dda

                y1 = (m * x) + b;
                points.AddLast(new Point3D(x, round(y1), 0));
                //*(raster + (((rounD(y1)) * c) + ((int)x))) = 1;
                aux_y1 = y1;

                for (i = 0; i < len; i++)
                {
                    x = x + 1;
                    y1 = aux_y1 + m;//incremento en y
                    points.AddLast(new Point3D(x, round(y1) + 1, 0));
                    //*(raster + (((rounding(y1) + 1) * c) + ((int)x))) = 1;
                    aux_y1 = y1;
                }

            }
            //////////////////////////////////////////////////////////
            /////////////////////swap the plane///////////////////////
            //////////////////////////////////////////////////////////
            else
            {

                //////////////////////////////////////////en caso de que P1 sea mas chico que P0
                if (yf < yi)
                {
                    aux_x = xf;
                    aux_y = yf;
                    xf = xi;
                    yf = yi;
                    xi = aux_x;
                    yi = aux_y;
                    len = xf - xi;
                    if (len < 0)
                    {
                        len = len * (-1);
                    }
                }
                ////////////////////////////////////////////////////////////////////////////////

                m = (((float)xf - (float)xi) / ((float)yf - (float)yi));
                b = (float)((float)xi - (m * (float)yi));
                y = yi;//(float)yi
                len = yf - yi;

                ///////////////////////////////////////////////////////////algoritmo dda swaping

                y1 = (m * y) + b;// y1 = x
                points.AddLast(new Point3D(round(y1), round(y), 0));
                //*(raster+(((((int)y)* c) + (rounding(y1))))) = 1;
                aux_y1 = y1;

                for (i = 0; i < len; i++)
                {
                    y = y + 1;
                    y1 = aux_y1 + m;  //incremento en y
                    points.AddLast(new Point3D(round(y1), round(y), 0));
                    //*(raster+(((((int)y)* c) + (rounding(y1))))) = 1;
                    aux_y1 = y1;
                }
            }
            points.AddLast(p1);
            return points;
        }

        private static int round(float num)
        {
            int entero = (int)num;
            float aux = num - entero;
            if (aux > 0.49)
            {
                return entero + 1;
            }
            else
            {
                return entero;
            }
        }
    }
}
