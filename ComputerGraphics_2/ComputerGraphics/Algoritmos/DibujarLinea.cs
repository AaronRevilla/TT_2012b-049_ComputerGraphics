using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows.Controls;

namespace ComputerGraphics.Algoritmos
{
    class DibujarLinea
    {
        public static LinkedList<Point3D> bresenham(Point3D p0, Point3D p1, TextBlock areaResumen, TextBlock areaInformacion)
        {
            /*
             *  Constantes para el algoritmo Bresenham
             * 
             *  dx
             *  dy
             *  m = dy/dx
             *  2dy
             *  2(dy - dx)
             *  k=0
             *  p[k] = (x[k],y[k])
             *  
             */
            int x, y, dx, dy, p, incE, incNE, x0, y0, x1, y1, stepx, stepy, indicePuntos;
            LinkedList<Point3D> puntos = new LinkedList<Point3D>();
            x0 = (int)(p0.X);
            x1 = (int)(p1.X);
            y0 = (int)(p0.Y);
            y1 = (int)(p1.Y);

            dy = (y1 - y0);             //constante
            dx = (x1 - x0);             //constante

            areaInformacion.Text = "dx = " + dx + "\n";
            areaInformacion.Text = areaInformacion.Text + "dy = " + dy + "\n";
            areaInformacion.Text = areaInformacion.Text + "m = " + (dy/dx) + "\n";

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
            indicePuntos = 1;
            if (dx > dy)
            {
                p = 2 * dy - dx;        //constantes P[0]
                incE = 2 * dy;          //constante 
                incNE = 2 * (dy - dx);  //constante

                areaInformacion.Text = areaInformacion.Text + "2dy = " + incE + "\n";
                areaInformacion.Text = areaInformacion.Text + "2(dy - dx) = " + incNE + "\n";

                areaResumen.Text = indicePuntos + ".-   ( " + x + ", " + y + ") p= " + p + "\n";
                while (x != x1)
                {
                    x = x + stepx;
                    if (p < 0)
                    {
                        p = p + incE;   //P[k+1]
                    }
                    else
                    {
                        y = y + stepy;
                        p = p + incNE; //P[k+1]
                    }
                    puntos.AddLast(new Point3D(x, y, 0));
                    areaResumen.Text = areaResumen.Text + (++indicePuntos) + ".-   ( " + x + ", " + y + ") p = " + p + "\n";
                }
            }
            else
            {
                p = 2 * dx - dy;        //constante P0
                incE = 2 * dx;          //constante
                incNE = 2 * (dx - dy);  //constante

                areaInformacion.Text = areaInformacion.Text + "2dy = " + incE + "\n";
                areaInformacion.Text = areaInformacion.Text + "2(dy - dx) = " + incNE + "\n";

                areaResumen.Text = indicePuntos + ".-   ( " + x + ", " + y + ") p = " + p + "\n";
                while (y != y1)
                {
                    y = y + stepy;
                    if (p < 0)
                    {
                        p = p + incE;   //P[k+1]
                    }
                    else
                    {
                        x = x + stepx;  
                        p = p + incNE;  //P[k+1]
                    }
                    puntos.AddLast(new Point3D(x, y, 0));
                    areaResumen.Text = areaResumen.Text + (++indicePuntos) + ".-   ( " + x + ", " + y + ") p= " + p + "\n";
                }
            }
            return puntos;
        }

        public static LinkedList<Point3D> naiveLine(Point3D p0, Point3D p1, TextBlock areaResumen, TextBlock areaInformacion)
        {
            /*
             *  Constantes para el algoritmo Naive
             *
             *  dx
             *  dy
             *  m = dy/dx
             *  b 
             *  y(real)
             *  k=0
             *  p[k] = (x[k],y[k])
             *  
             */
            LinkedList<Point3D> points = new LinkedList<Point3D>();
            int x0 = (int)p0.X;
            int y0 = (int)p0.Y;
            int x1 = (int)p1.X;
            int y1 = (int)p1.Y;

            int dx = x1 - x0;       //constante
            int dy = y1 - y0;       //constante

            int indicePuntos = 1;

            areaInformacion.Text = "dx = " + dx + "\n";
            areaInformacion.Text = areaInformacion.Text + "dy = " + dy + "\n";

            points.AddLast(p0);
            if (Math.Abs(dx) > Math.Abs(dy))
            {
                float m = (float)(dy) / (float)(dx);    //constante
                float b = y0 - m * x0;                  //constante

                areaInformacion.Text = areaInformacion.Text + "m = " + m + "\n";
                areaInformacion.Text = areaInformacion.Text + "b = " + b + "\n";
                areaResumen.Text = indicePuntos + ".-   ( " + x0 + ", " + y0 + ") y(real) = " + y0 + "\n";

                if (dx < 0)
                    dx = -1;
                else
                    dx = 1;
                while (x0 != x1)
                {
                    x0 += dx;
                    y0 = round(m * x0 + b);             //constante
                    points.AddLast(new Point3D(x0, y0, 0)); //add new 'x' and 'y'
                    areaResumen.Text = areaResumen.Text + (++indicePuntos) + ".-   ( " + x0 + ", " + y0 + ") y(real) = " + y0 + "\n";
                }
            }
            else
            {
                if (dy != 0)
                {
                    float m = (float)(dx) / (float)(dy);    //constante
                    float b = x0 - m * y0;                  //constante

                    areaInformacion.Text = areaInformacion.Text + "m = " + m + "\n";
                    areaInformacion.Text = areaInformacion.Text + "b = " + b + "\n";
                    areaResumen.Text = indicePuntos + ".-   ( " + x0 + ", " + y0 + ") y(real) = " + y0 + "\n";

                    if (dy < 0)
                        dy = -1;
                    else
                        dy = 1;
                    while (y0 != y1)
                    {
                        y0 += dy;
                        x0 = round(m * y0 + b);             //constante
                        points.AddLast(new Point3D(x0, y0, 0)); //add new 'x' and 'y'
                        areaResumen.Text = areaResumen.Text + (++indicePuntos) + ".-   ( " + x0 + ", " + y0 + ") y(real) = " + y0 + "\n";
                    }
                }
            }

            return points;
        }

        public static LinkedList<Point3D> dda(Point3D p0, Point3D p1, TextBlock areaResumen, TextBlock areaInformacion)
        {
            /*
             *  Constantes para el algoritmo DDA
             *  dx
             *  dy
             *  m= dx/dy
             *  k=0
             *  x[k] =  x[k-1] + (1/m)
             *  y[k] =  y[k-1] + m
             */
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
            int indicePuntos = 0;
            int y = 0;

            float x = 0;
            float b = 0;
            float y1 = 0;
            float aux_y1 = 0;
            float m = 0;

            longx = xf - xi;
            longy = yf - yi;

            m = (((float)yf - (float)yi) / ((float)xf - (float)xi));                     //constante

            areaInformacion.Text = "dx = " + longx + "\n";
            areaInformacion.Text = areaInformacion.Text + "dy = " + longy + "\n";
            areaInformacion.Text = areaInformacion.Text + "m = " + m + "\n";
            areaResumen.Text = (++indicePuntos) + ".-   ( " + xi + ", " + yi + ") y(real) = " + aux_y1 + "\n";

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
                    areaResumen.Text = areaResumen.Text + (++indicePuntos) + ".-   ( " + indicePunto + ", " + yi + ") y(real) = " + aux_y1 + "\n";
                }
                points.AddLast(p1);
                areaResumen.Text = areaResumen.Text + (++indicePuntos) + ".-   ( " + xf + ", " + yf + ") y(real) = " + aux_y1 + "\n";
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
                    areaResumen.Text = areaResumen.Text + (++indicePuntos) + ".-   ( " + xi + ", " + indicePunto + ") y(real) = " + aux_y1 + "\n";
                }
                points.AddLast(p1);
                areaResumen.Text = areaResumen.Text + (++indicePuntos) + ".-   ( " + xf + ", " + yf + ") y(real) = " + aux_y1 + "\n";
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
                        areaResumen.Text = areaResumen.Text + (++indicePuntos) + ".-   ( " + xi + ", " + yi + ") y(real) = " + aux_y1 + "\n";
                    }
                }
                else
                {
                    for (indicePunto = xi; indicePunto < xf; indicePunto++)
                    {
                        points.AddLast(new Point3D(xi, yi, 0));
                        xi++;
                        yi--;
                        areaResumen.Text = areaResumen.Text + (++indicePuntos) + ".-   ( " + xi + ", " + yi + ") y(real) = " + aux_y1 + "\n";
                    }
                }
                points.AddLast(p1);
                areaResumen.Text = areaResumen.Text + (++indicePuntos) + ".-   ( " + xf + ", " + yf + ") y(real) = " + aux_y1 + "\n";
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

                areaInformacion.Text = areaInformacion.Text + "b = " + b + "\n";
                ///////////////////////////////////////////////////////////////////algoritmo dda

                y1 = (m * x) + b;
                points.AddLast(new Point3D(x, round(y1), 0));
                aux_y1 = y1;
                areaResumen.Text = areaResumen.Text + (++indicePuntos) + ".-   ( " + x + ", " + round(y1) + ") y(real) = "+ aux_y1 +"\n";

                for (i = 0; i < len; i++)
                {
                    x = x + 1;
                    y1 = aux_y1 + m;    //incremento en y
                    points.AddLast(new Point3D(x, round(y1) + 1, 0));
                    areaResumen.Text = areaResumen.Text + (++indicePuntos) + ".-   ( " + x + ", " + round(y1) + ") y(real) = " + aux_y1 + "\n";
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

                areaInformacion.Text = areaInformacion.Text + "b = " + b + "\n";
                ///////////////////////////////////////////////////////////algoritmo dda swaping

                y1 = (m * y) + b;// y1 = x
                points.AddLast(new Point3D(round(y1), round(y), 0));
                aux_y1 = y1;
                areaResumen.Text = areaResumen.Text + (++indicePuntos) + ".-   ( " + round(y1) + ", " + round(y) +") y(real) = " + aux_y1 + "\n";

                for (i = 0; i < len; i++)
                {
                    y = y + 1;
                    y1 = aux_y1 + m;  //incremento en y
                    points.AddLast(new Point3D(round(y1), round(y), 0));
                    areaResumen.Text = areaResumen.Text + (++indicePuntos) + ".-   ( " + round(y1) + ", " + round(y) + ") y(real) = " + aux_y1 + "\n";
                    aux_y1 = y1;
                }
            }
            points.AddLast(p1);
            areaResumen.Text = areaResumen.Text + (++indicePuntos) + ".-   ( " + xf + ", " + yf + ") y(real) = " + aux_y1 + "\n";
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
