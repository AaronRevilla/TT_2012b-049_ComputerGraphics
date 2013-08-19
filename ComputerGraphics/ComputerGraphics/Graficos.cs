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
        public ModelVisual3D dibujaCubo(Point3D[] puntos) { 
            if(puntos.Length == 8){
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

        public PerspectiveCamera setCamera(Point3D position, Vector3D lookDirection) {
            PerspectiveCamera camera = new PerspectiveCamera();
            camera.Position = position;
            camera.LookDirection = lookDirection;
            return camera;
        }

        public Point3D[] rotateModel(Point3D[] puntos, double angle,String axis) {
            Point3D aux = new Point3D();
            Point3D[] rotados;
            switch(axis){
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
                        aux.Z = (puntos[i].Z) * (Math.Cos((angle* Math.PI) / 180)) - (puntos[i].X) * (Math.Sin((angle* Math.PI) / 180));
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

        public Point3D[] transferMode(Point3D[] puntos, double delta, String axis) {
            Point3D[] transfered;
            switch(axis){
                case "x":
                    for (int i = 0; i < puntos.Length; i++) {
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

        public Point3D[] resizeModel(Point3D[] puntos, double scalar) {
            Point3D[] resized;
            for (int i = 0; i < puntos.Length;i++ )
            {
                puntos[i].X = puntos[i].X * (1.1);
                puntos[i].Y = puntos[i].Y * (1.1);
                puntos[i].Z = puntos[i].Z * (1.1);
            }
            resized = puntos;
            return resized;
        }
    }
}
