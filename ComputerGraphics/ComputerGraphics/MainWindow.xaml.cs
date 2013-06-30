using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;

namespace ComputerGraphics
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ModelVisual3D model;
        Graficos graficos;
        Point3D[] puntos = new Point3D[8];
        int x, y, z;
        public MainWindow()
        {
            InitializeComponent();
            graficos = new Graficos();
        }

        private void simpleButtonClick(object sender, RoutedEventArgs e)
        {
            MeshGeometry3D triagleMesh = new MeshGeometry3D();
            Point3D p0 = new Point3D(0, 0, 0);
            Point3D p1 = new Point3D(5, 0, 0);
            Point3D p2 = new Point3D(0, 0, 5);

            triagleMesh.Positions.Add(p0);
            triagleMesh.Positions.Add(p1);
            triagleMesh.Positions.Add(p2);

            triagleMesh.TriangleIndices.Add(0);
            triagleMesh.TriangleIndices.Add(2);
            triagleMesh.TriangleIndices.Add(1);

            Vector3D normal = new Vector3D(0, 1, 0);
            triagleMesh.Normals.Add(normal);
            triagleMesh.Normals.Add(normal);
            triagleMesh.Normals.Add(normal);

            Material material = new DiffuseMaterial(new SolidColorBrush(Colors.LawnGreen));
            GeometryModel3D triangleModel = new GeometryModel3D(triagleMesh, material);

            ModelVisual3D model = new ModelVisual3D();
            model.Content = triangleModel;
            this.mainViewport.Children.Add(model);
        }

        private void cubeButtonClick(object sender, RoutedEventArgs e)
        {
            //Model3DGroup cube = new Model3DGroup();

            puntos[0] = new Point3D(0, 0, 0);
            puntos[1] = new Point3D(5, 0, 0);
            puntos[2] = new Point3D(5, 0, 5);
            puntos[3] = new Point3D(0, 0, 5);
            puntos[4] = new Point3D(0, 5, 0);
            puntos[5] = new Point3D(5, 5, 0);
            puntos[6] = new Point3D(5, 5, 5);
            puntos[7] = new Point3D(0, 5, 5);
            model = graficos.dibujaCubo(puntos);

            if (model != null)
                this.mainViewport.Children.Add(model);

        }





        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            Transform3DGroup transform3DGroup = new Transform3DGroup();
            if (e.Key == Key.W)
            {
                for (int i = 0; i < 8; i++)
                {
                    puntos[i].Y = puntos[i].Y + 1;
                }
                this.mainViewport.Children.Remove(model);
                model = graficos.dibujaCubo(puntos);
                if (model != null)
                    this.mainViewport.Children.Add(model);
            }
            if (e.Key == Key.S)
            {
                for (int i = 0; i < 8; i++)
                {
                    puntos[i].Y = puntos[i].Y - 1;
                }
                this.mainViewport.Children.Remove(model);
                model = graficos.dibujaCubo(puntos);
                if (model != null)
                    this.mainViewport.Children.Add(model);
            }
            if (e.Key == Key.D)
            {

                for (int i = 0; i < 8; i++)
                {
                    puntos[i].X = puntos[i].X + 1;
                }
                this.mainViewport.Children.Remove(model);
                model = graficos.dibujaCubo(puntos);
                if (model != null)
                    this.mainViewport.Children.Add(model);
            }
            if (e.Key == Key.A)
            {
                for (int i = 0; i < 8; i++)
                {
                    puntos[i].X = puntos[i].X - 1;
                }
                this.mainViewport.Children.Remove(model);
                model = graficos.dibujaCubo(puntos);
                if (model != null)
                    this.mainViewport.Children.Add(model);
            }
            if (e.Key == Key.Add)
            {
                for (int i = 0; i < 8; i++)
                {
                    puntos[i].Z = puntos[i].Z + 1;
                }
                this.mainViewport.Children.Remove(model);
                model = graficos.dibujaCubo(puntos);
                if (model != null)
                    this.mainViewport.Children.Add(model);

            }
            if (e.Key == Key.Subtract)
            {
                for (int i = 0; i < 8; i++)
                {
                    puntos[i].Z = puntos[i].Z - 1;
                }
                this.mainViewport.Children.Remove(model);
                model = graficos.dibujaCubo(puntos);
                if (model != null)
                    this.mainViewport.Children.Add(model);
            }

            if (e.Key == Key.M)
            {
                for (int i = 0; i < 8; i++)
                {
                    puntos[i].X = puntos[i].X * (1.1);
                    puntos[i].Y = puntos[i].Y * (1.1);
                    puntos[i].Z = puntos[i].Z * (1.1);
                }
                this.mainViewport.Children.Remove(model);
                model = graficos.dibujaCubo(puntos);
                if (model != null)
                    this.mainViewport.Children.Add(model);
            }
            if (e.Key == Key.N)
            {
                for (int i = 0; i < 8; i++)
                {
                    puntos[i].X = puntos[i].X * (.9);
                    puntos[i].Y = puntos[i].Y * (.9);
                    puntos[i].Z = puntos[i].Z * (.9);
                }
                this.mainViewport.Children.Remove(model);
                model = graficos.dibujaCubo(puntos);
                if (model != null)
                    this.mainViewport.Children.Add(model);
            }
            if (e.Key == Key.U)
            {
                Point3D prota = new Point3D();
                for (int i = 0; i < 8; i++)
                {

                    prota.X = (puntos[i].X) * (Math.Cos((10 * Math.PI) / 180)) - (puntos[i].Y) * (Math.Sin((10 * Math.PI) / 180));
                    prota.Y = (puntos[i].X) * (Math.Sin((10 * Math.PI) / 180)) + (puntos[i].Y) * (Math.Cos((10 * Math.PI) / 180));
                    prota.Z = puntos[i].Z;

                    puntos[i].X = prota.X;
                    puntos[i].Y = prota.Y;
                    puntos[i].Z = prota.Z;
                }
                this.mainViewport.Children.Remove(model);
                model = graficos.dibujaCubo(puntos);
                if (model != null)
                    this.mainViewport.Children.Add(model);
            }
            if (e.Key == Key.O)
            {
                Point3D prota = new Point3D();
                for (int i = 0; i < 8; i++)
                {
                    prota.X = (puntos[i].X) * (Math.Cos((-10 * Math.PI) / 180)) - (puntos[i].Y) * (Math.Sin((-10 * Math.PI) / 180));
                    prota.Y = (puntos[i].X) * (Math.Sin((-10 * Math.PI) / 180)) + (puntos[i].Y) * (Math.Cos((-10 * Math.PI) / 180));
                    prota.Z = puntos[i].Z;

                    puntos[i].X = prota.X;
                    puntos[i].Y = prota.Y;
                    puntos[i].Z = prota.Z;

                }
                this.mainViewport.Children.Remove(model);
                model = graficos.dibujaCubo(puntos);
                if (model != null)
                    this.mainViewport.Children.Add(model);
            }
            if (e.Key == Key.I)
            {
                Point3D prota = new Point3D();
                for (int i = 0; i < 8; i++)
                {
                    prota.Y = (puntos[i].Y) * (Math.Cos((-10 * Math.PI) / 180)) - (puntos[i].Z) * (Math.Sin((-10 * Math.PI) / 180));
                    prota.Z = (puntos[i].Y) * (Math.Sin((-10 * Math.PI) / 180)) + (puntos[i].Z) * (Math.Cos((-10 * Math.PI) / 180));
                    prota.X = puntos[i].X;

                    puntos[i].X = prota.X;
                    puntos[i].Y = prota.Y;
                    puntos[i].Z = prota.Z;

                }
                this.mainViewport.Children.Remove(model);
                model = graficos.dibujaCubo(puntos);
                if (model != null)
                    this.mainViewport.Children.Add(model);
            }
            if (e.Key == Key.K) { 
            
            }
            if (e.Key == Key.K)
            {
                Point3D prota = new Point3D();
                for (int i = 0; i < 8; i++)
                {
                    prota.Y = (puntos[i].Y) * (Math.Cos((10 * Math.PI) / 180)) - (puntos[i].Z) * (Math.Sin((10 * Math.PI) / 180));
                    prota.Z = (puntos[i].Y) * (Math.Sin((10 * Math.PI) / 180)) + (puntos[i].Z) * (Math.Cos((10 * Math.PI) / 180));
                    prota.X = puntos[i].X;

                    puntos[i].X = prota.X;
                    puntos[i].Y = prota.Y;
                    puntos[i].Z = prota.Z;

                }
                this.mainViewport.Children.Remove(model);
                model = graficos.dibujaCubo(puntos);
                if (model != null)
                    this.mainViewport.Children.Add(model);
            }
            if (e.Key == Key.J)
            {
                Point3D prota = new Point3D();
                for (int i = 0; i < 8; i++)
                {
                    prota.Z = (puntos[i].Z) * (Math.Cos((-10 * Math.PI) / 180)) - (puntos[i].X) * (Math.Sin((-10 * Math.PI) / 180));
                    prota.X = (puntos[i].Z) * (Math.Sin((-10 * Math.PI) / 180)) + (puntos[i].X) * (Math.Cos((-10 * Math.PI) / 180));
                    prota.Y = puntos[i].Y;

                    puntos[i].X = prota.X;
                    puntos[i].Y = prota.Y;
                    puntos[i].Z = prota.Z;

                }
                this.mainViewport.Children.Remove(model);
                model = graficos.dibujaCubo(puntos);
                if (model != null)
                    this.mainViewport.Children.Add(model);
            }
            if (e.Key == Key.L)
            {
                Point3D prota = new Point3D();
                for (int i = 0; i < 8; i++)
                {
                    prota.Z = (puntos[i].Z) * (Math.Cos((10 * Math.PI) / 180)) - (puntos[i].X) * (Math.Sin((10 * Math.PI) / 180));
                    prota.X = (puntos[i].Z) * (Math.Sin((10 * Math.PI) / 180)) + (puntos[i].X) * (Math.Cos((10 * Math.PI) / 180));
                    prota.Y = puntos[i].Y;

                    puntos[i].X = prota.X;
                    puntos[i].Y = prota.Y;
                    puntos[i].Z = prota.Z;

                }
                this.mainViewport.Children.Remove(model);
                model = graficos.dibujaCubo(puntos);
                if (model != null)
                    this.mainViewport.Children.Add(model);
            }

        }

        private void mainViewport_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point3D prota = new Point3D();
            for (int i = 0; i < 8; i++)
            {
                prota.Z = (puntos[i].Z) * (Math.Cos((10 * Math.PI) / 180)) - (puntos[i].X) * (Math.Sin((10 * Math.PI) / 180));
                prota.X = (puntos[i].Z) * (Math.Sin((10 * Math.PI) / 180)) + (puntos[i].X) * (Math.Cos((10 * Math.PI) / 180));
                prota.Y = puntos[i].Y;

                puntos[i].X = prota.X;
                puntos[i].Y = prota.Y;
                puntos[i].Z = prota.Z;

            }
            this.mainViewport.Children.Remove(model);
            model = graficos.dibujaCubo(puntos);
            if (model != null)
                this.mainViewport.Children.Add(model);
        }

        private void cameraButton_Click(object sender, RoutedEventArgs e)
        {
            Point3D position = new Point3D(
            Convert.ToDouble(cameraPositionXTextBox.Text),
            Convert.ToDouble(cameraPositionYTextBox.Text),
            Convert.ToDouble(cameraPositionZTextBox.Text)
    );
            Vector3D lookDirection = new Vector3D(
                Convert.ToDouble(lookAtXTextBox.Text),
                Convert.ToDouble(lookAtYTextBox.Text),
                Convert.ToDouble(lookAtZTextBox.Text)
            );

            PerspectiveCamera camera = (PerspectiveCamera)mainViewport.Camera;
            camera = graficos.setCamera(position, lookDirection);
            mainViewport.Camera = camera;
             Model3DGroup cube = new Model3DGroup();
            DirectionalLight myDirectionalLight = new DirectionalLight();
            myDirectionalLight.Color = Colors.Blue;
            myDirectionalLight.Direction = new Vector3D(-0.61, -0.5, -0.61);
            cube.Children.Add(myDirectionalLight);
            
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Model3DGroup caras = new Model3DGroup();
            LeerArchivo archivo = null;
            int numCaras = 0;
            int numPuntos = 0;
      
            var ofd = new Microsoft.Win32.OpenFileDialog();
            var result = ofd.ShowDialog();
            if (result == false) return;
            
            archivo = new LeerArchivo(ofd.FileName);
            numCaras = archivo.numLines();

            for (int indiceLinea = 0; indiceLinea < numCaras; indiceLinea++)
            {
                string[] puntosCadena = archivo.getLine(indiceLinea + 1).Split(' ');
                numPuntos = puntosCadena.Count() / 3;
                Consola.Text = Consola.Text + numPuntos + "------" + indiceLinea + '\n';
                //Verificacion de que las coordenadas vengan en tercias
                if (puntosCadena.Count() % 3 == 0)
                {
                    Point3D[] puntosArchvivo = new Point3D[numPuntos];
                    int i = 0;

                    for (int indicePunto = 0; indicePunto < numPuntos; indicePunto++)
                    {
                        double x = Convert.ToDouble(puntosCadena.ElementAt(i++));
                        double y = Convert.ToDouble(puntosCadena.ElementAt(i++));
                        double z = Convert.ToDouble(puntosCadena.ElementAt(i++));
                        puntosArchvivo[indicePunto] = new Point3D(x, y, z);
                        Consola.Text = Consola.Text + " X=" + puntosArchvivo[indicePunto].X + " Y=" + puntosArchvivo[indicePunto].Y + " Z=" + puntosArchvivo[indicePunto].Z + '\n';
                    }
                    caras.Children.Add(graficos.CreatePolygonModel(puntosArchvivo));
                }
                else
                {
                    //no se puede hacer cara
                }
            }
            model = new ModelVisual3D();
            model.Content = caras;
            if (model != null)
            {
                this.mainViewport.Children.Add(model);
            }
       }
    }
}

