using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace ComputerGraphics
{
    class Graficos
    {
        private Point3D[] puntos;

        public Point3D[] getPoints()
        {
            return this.puntos;
        }

        public void setPoints(Point3D[] puntos)
        {
            this.puntos = puntos;
        }

        public ModelVisual3D dibujaCubo(Point3D[] puntos)
        {
            if (puntos.Length == 8)
            {
                Model3DGroup cube = new Model3DGroup();
                cube.Children.Add(CreateTriangleModel(puntos[3], puntos[2], puntos[6]));
                cube.Children.Add(CreateTriangleModel(puntos[3], puntos[6], puntos[7]));

                cube.Children.Add(CreateTriangleModel(puntos[2], puntos[1], puntos[5]));
                cube.Children.Add(CreateTriangleModel(puntos[2], puntos[5], puntos[6]));

                cube.Children.Add(CreateTriangleModel(puntos[1], puntos[0], puntos[4]));
                cube.Children.Add(CreateTriangleModel(puntos[1], puntos[4], puntos[5]));

                cube.Children.Add(CreateTriangleModel(puntos[0], puntos[3], puntos[7]));
                cube.Children.Add(CreateTriangleModel(puntos[0], puntos[7], puntos[4]));

                cube.Children.Add(CreateTriangleModel(puntos[7], puntos[6], puntos[5]));
                cube.Children.Add(CreateTriangleModel(puntos[7], puntos[5], puntos[4]));

                cube.Children.Add(CreateTriangleModel(puntos[2], puntos[3], puntos[0]));
                cube.Children.Add(CreateTriangleModel(puntos[2], puntos[0], puntos[1]));
                ModelVisual3D modelo = new ModelVisual3D();
                modelo.Content = cube;
                return modelo;
            }
            return null;
        }

        public Model3DGroup CreateTriangleModel(Point3D p0, Point3D p1, Point3D p2)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(p0);
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);

            for (int i = 0; i < 3; i++)
            {
                mesh.TriangleIndices.Add(i);
            }

            Vector3D normal = CalculateNormal(p0, p1, p2);

            for (int i = 0; i < 3; i++)
            {
                mesh.Normals.Add(normal);
            }

            Material material = new DiffuseMaterial(new SolidColorBrush(Colors.DarkCyan));
            GeometryModel3D model = new GeometryModel3D(mesh, material);
            Model3DGroup group = new Model3DGroup();
            group.Children.Add(model);
            return group;
        }

        public Model3DGroup CreatePolygonModel(Point3D[] points)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            int indexPoints = 0;
            int totalPoints = points.Count();

            for (indexPoints = 0; indexPoints < totalPoints; indexPoints++)
            {
                mesh.Positions.Add(points.ElementAt(indexPoints));
            }

            //Solo se necesitan tres puntos para poder saber la cara del triangulo
            for (int i = 0; i < 3; i++)
            {
                mesh.TriangleIndices.Add(i);
            }

            //Para obtener el vector normal solo se necesitan tres puntos del plano
            Vector3D normal = CalculateNormal(points.ElementAt(0), points.ElementAt(1), points.ElementAt(2));

            //¿Checar por que se agrega 3 veces la normal?
            //for (int i = 0; i < 3; i++)
            //{
            mesh.Normals.Add(normal);
            //}

            Material material = new DiffuseMaterial(new SolidColorBrush(Colors.DarkCyan));
            GeometryModel3D model = new GeometryModel3D(mesh, material);
            Model3DGroup group = new Model3DGroup();
            group.Children.Add(model);
            return group;
        }

        private Vector3D CalculateNormal(Point3D p0, Point3D p1, Point3D p2)
        {
            Vector3D v0 = new Vector3D(p1.X - p0.X, p1.Y - p0.Y, p1.Z - p0.Z);
            Vector3D v1 = new Vector3D(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            return Vector3D.CrossProduct(v0, v1);
        }

        public PerspectiveCamera setCamera(Point3D position, Vector3D lookDirection)
        {
            PerspectiveCamera camera = new PerspectiveCamera();
            camera.Position = position;
            camera.LookDirection = lookDirection;
            return camera;
        }

        public Point3D[] rotateModel(Point3D[] puntos, double angle, String axis)
        {
            Point3D aux = new Point3D();
            Point3D[] rotados;
            switch (axis)
            {
                case "x":
                    for (int i = 0; i < puntos.Length; i++)
                    {
                        aux.Y = (puntos[i].Y) * (Math.Cos((angle * Math.PI) / 180)) - (puntos[i].Z) * (Math.Sin((angle * Math.PI) / 180));
                        aux.Z = (puntos[i].Y) * (Math.Sin((angle * Math.PI) / 180)) + (puntos[i].Z) * (Math.Cos((angle * Math.PI) / 180));
                        aux.X = puntos[i].X;

                        puntos[i].X = aux.X;
                        puntos[i].Y = aux.Y;
                        puntos[i].Z = aux.Z;
                    }
                    break;
                case "y":
                    for (int i = 0; i < puntos.Length; i++)
                    {
                        aux.Z = (puntos[i].Z) * (Math.Cos((angle * Math.PI) / 180)) - (puntos[i].X) * (Math.Sin((angle * Math.PI) / 180));
                        aux.X = (puntos[i].Z) * (Math.Sin((angle * Math.PI) / 180)) + (puntos[i].X) * (Math.Cos((angle * Math.PI) / 180));
                        aux.Y = puntos[i].Y;

                        puntos[i].X = aux.X;
                        puntos[i].Y = aux.Y;
                        puntos[i].Z = aux.Z;
                    }
                    break;
                case "z":
                    for (int i = 0; i < puntos.Length; i++)
                    {
                        aux.X = (puntos[i].X) * (Math.Cos((angle * Math.PI) / 180)) - (puntos[i].Y) * (Math.Sin((angle * Math.PI) / 180));
                        aux.Y = (puntos[i].X) * (Math.Sin((angle * Math.PI) / 180)) + (puntos[i].Y) * (Math.Cos((angle * Math.PI) / 180));
                        aux.Z = puntos[i].Z;

                        puntos[i].X = aux.X;
                        puntos[i].Y = aux.Y;
                        puntos[i].Z = aux.Z;
                    }
                    break;
                default:
                    break;
            }
            rotados = puntos;
            return rotados;
        }

        public Point3D[] transferMode(Point3D[] puntos, double delta, String axis)
        {
            Point3D[] transfered;
            switch (axis)
            {
                case "x":
                    for (int i = 0; i < puntos.Length; i++)
                    {
                        puntos[i].X = puntos[i].X + delta;
                    }
                    break;
                case "y":
                    for (int i = 0; i < puntos.Length; i++)
                    {
                        puntos[i].Y = puntos[i].Y + delta;
                    }
                    break;
                case "z":
                    for (int i = 0; i < puntos.Length; i++)
                    {
                        puntos[i].Z = puntos[i].Z + delta;
                    }
                    break;
                default:
                    break;
            }
            transfered = puntos;
            return transfered;
        }

        public Point3D[] resizeModel(Point3D[] puntos, double scalar)
        {
            Point3D[] resized;
            for (int i = 0; i < puntos.Length; i++)
            {
                puntos[i].X = puntos[i].X * (1.1);
                puntos[i].Y = puntos[i].Y * (1.1);
                puntos[i].Z = puntos[i].Z * (1.1);
            }
            resized = puntos;
            return resized;
        }

        public LinkedList<Point3D> bresenham(Point3D p0 , Point3D p1) {
            int x, y, dx, dy, p, incE, incNE,x0,y0,x1,y1,stepx,stepy;
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

        public LinkedList<Point3D> simpleLine(Point3D p0, Point3D p1) {
            LinkedList<Point3D> points = new LinkedList<Point3D>();
            int x0 = (int)p0.X;
            int y0 = (int)p0.Y;
            int x1 = (int)p1.X;
            int y1 = (int)p1.Y;

            int dx = x1-x0;
            int dy = y1-y0;
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
                    y0 = round(m*x0 + b);
                    points.AddLast(new Point3D(x0, y0, 0));
                }
            }
            else {
                if (dy != 0) {
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

        public LinkedList<Point3D> dda(Point3D p0, Point3D p1)
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
                for (indicePunto = (xi - 1); indicePunto < xf; indicePunto++)
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
                if (m == 1)
                {
                    for (indicePunto = (xi - 1); indicePunto < xf; indicePunto++)
                    {
                        points.AddLast(new Point3D(xi, yi, 0));
                        xi++;
                        yi++;
                    }
                }
                else
                {
                    for (indicePunto = (xi - 1); indicePunto < xf; indicePunto++)
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
                x	= (float)xi;
                b	= ((float)yi) - (m * x);
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
		
        private int round(float num) {
            int entero = (int)num;
            float aux = num - entero;
            if (aux > 0.49)
            {
                return entero + 1;
            }
            else {
                return entero;
            }
        }

       
    }


   
}
