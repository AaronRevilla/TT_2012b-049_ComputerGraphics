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

        private Model3DGroup CreateTriangleModel(Point3D p0, Point3D p1, Point3D p2)
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


    }
}
